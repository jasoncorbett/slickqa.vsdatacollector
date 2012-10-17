// Copyright 2012 AccessData Group, LLC.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//  http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml.Serialization;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextManager.Interop;
using SlickQA.DataCollector.ConfigurationEditor.AppController;
using SlickQA.DataCollector.ConfigurationEditor.Events;
using SlickQA.DataCollector.ConfigurationEditor.View;
using SlickQA.DataCollector.EventAggregator;
using SlickQA.DataCollector.Models;

namespace SlickQA.TestFileEditor
{
	public class EditorController :
		WindowPane,
		IPersistFileFormat,
		IVsDeferredDocView,
		IVsPersistDocData,
		IEventHandler<UrlValidatedEvent>,
		IEventHandler<UrlInvalidatedEvent>,
		IEventHandler<TestPlanValidatedEvent>,
		IEventHandler<TestPlanInvalidatedEvent>,
		IEventHandler<ProjectSelectedEvent>,
		IEventHandler<ReleaseSelectedEvent>,
		IEventHandler<TestPlanSelectedEvent>,
		IEventHandler<BuildProviderSelectedEvent>,
		IEventHandler<OrderedTestSelectedEvent>
	{
		private string _filename;
		private IVsTextLines _text;
		private readonly IEditorView _view;

		private bool _saving;
		private IVsUIShell _vsUIShell;
		private bool _loading;
		private bool _isDirty;
		private readonly IApplicationController _appController;

		public EditorController(string filename, IVsTextLines text, IEditorView view, IApplicationController appController)
		{
			_filename = filename;
			_text = text;
			_view = view;
			_appController = appController;
		}

		private const uint FILE_FORMAT_INDEX = 0;
		
		#region "Window.Pane Overrides"

		public override IWin32Window Window
		{
			get { return _view.Window; }
		}

		#endregion

		#region Implementation of IPersist

		/// <param name="pClassID"/>
		int IPersist.GetClassID(out Guid pClassID)
		{
			return GetGuid(out pClassID);
		}

		public int IsDirty(out int pfIsDirty)
		{
			pfIsDirty = Convert.ToInt32(_isDirty);
			return VSConstants.S_OK;
		}

		public int InitNew(uint nFormatIndex)
		{
			if (nFormatIndex != FILE_FORMAT_INDEX)
			{
				throw new ArgumentException(Resources.InvalidFileFormatMessage);
			}

			_isDirty = false;
			return VSConstants.S_OK;
		}

		public int Load(string pszFilename, uint grfMode, int fReadOnly)
		{
			_loading = true;

			try
			{
				var newNameIsNull = string.IsNullOrWhiteSpace(pszFilename);

				if (newNameIsNull && string.IsNullOrWhiteSpace(_filename))
				{
					return VSConstants.E_INVALIDARG;
				}

				VsUIShell.SetWaitCursor();

				if (!newNameIsNull)
				{
					_filename = pszFilename;
				}

				LoadFile(_filename);
				_isDirty = false;

				NotifyDocReloaded();
			}
			finally
			{
				_loading = false;
			}
			return VSConstants.S_OK;
		}

		public int Save(string pszFilename, int fRemember, uint nFormatIndex)
		{
			_saving = true;
			try
			{
				if ((string.IsNullOrWhiteSpace(pszFilename) || pszFilename.Equals(_filename)))
				{
					SaveFile(_filename);
					_isDirty = false;
				}
				else
				{
					if (fRemember != 0) // Save As
					{
						_filename = pszFilename;
						SaveFile(_filename);
						_isDirty = false;
					}
					else // Save a Copy As
					{
						SaveFile(pszFilename);
					}
				}
			}
			finally
			{
				_saving = false;
			}
			return VSConstants.S_OK;
		}

		public int SaveCompleted(string pszFilename)
		{
			return _saving ? VSConstants.S_FALSE : VSConstants.S_OK;
		}

		public int GetCurFile(out string ppszFilename, out uint pnFormatIndex)
		{
			ppszFilename = _filename;
			pnFormatIndex = FILE_FORMAT_INDEX;
			return VSConstants.S_OK;
		}

		public int GetFormatList(out string ppszFormatList)
		{
			ppszFormatList = Resources.SaveAsFilter;
			return VSConstants.S_OK;
		}

		#endregion

		#region Implementation of IPersistFileFormat

		int IPersistFileFormat.GetClassID(out Guid pClassID)
		{
			return GetGuid(out pClassID);
		}

		#endregion

		#region Implementation of IVsDeferredDocView

		/// <summary>
		/// Provides the document view to the document window.
		/// </summary>
		/// <returns>
		/// If the method succeeds, it returns <see cref="F:Microsoft.VisualStudio.VSConstants.S_OK"/>. If it fails, it returns an error code.
		/// </returns>
		/// <param name="ppUnkDocView">[out] Pointer to the IUnknown interface of the document view. Used as an argument to <see cref="M:Microsoft.VisualStudio.Shell.Interop.IVsUIShell.CreateDocumentWindow(System.UInt32,System.String,Microsoft.VisualStudio.Shell.Interop.IVsUIHierarchy,System.UInt32,System.IntPtr,System.IntPtr,System.Guid@,System.String,System.Guid@,Microsoft.VisualStudio.OLE.Interop.IServiceProvider,System.String,System.String,System.Int32[],Microsoft.VisualStudio.Shell.Interop.IVsWindowFrame@)"/>.</param>
		public int get_DocView(out IntPtr ppUnkDocView)
		{
			ppUnkDocView = Marshal.GetIUnknownForObject(this);
			return VSConstants.S_OK;
		}

		/// <summary>
		/// Retrieves the GUID for the pane or editor factory for later use when you create the view.
		/// </summary>
		/// <returns>
		/// If the method succeeds, it returns <see cref="F:Microsoft.VisualStudio.VSConstants.S_OK"/>. If it fails, it returns an error code.
		/// </returns>
		/// <param name="pGuidCmdId">[out] Pointer to a GUID for the deferred view. Usually the GUID for the pane. Used as an argument to <see cref="M:Microsoft.VisualStudio.Shell.Interop.IVsUIShell.CreateDocumentWindow(System.UInt32,System.String,Microsoft.VisualStudio.Shell.Interop.IVsUIHierarchy,System.UInt32,System.IntPtr,System.IntPtr,System.Guid@,System.String,System.Guid@,Microsoft.VisualStudio.OLE.Interop.IServiceProvider,System.String,System.String,System.Int32[],Microsoft.VisualStudio.Shell.Interop.IVsWindowFrame@)"/> when you create the view.</param>
		public int get_CmdUIGuid(out Guid pGuidCmdId)
		{
			pGuidCmdId = GuidList.GuidTestFileEditorEditorFactory;
			return VSConstants.S_OK;
		}

		#endregion

		#region Implementation of IVsPersistDocData

		public int GetGuidEditorType(out Guid pClassID)
		{
			return GetGuid(out pClassID);
		}

		public int IsDocDataDirty(out int pfDirty)
		{
			return ((IPersistFileFormat)this).IsDirty(out pfDirty);
		}

		public int SetUntitledDocPath(string pszDocDataPath)
		{
			return ((IPersistFileFormat)this).InitNew(FILE_FORMAT_INDEX);
		}

		public int LoadDocData(string pszMkDocument)
		{
			return ((IPersistFileFormat)this).Load(pszMkDocument, 0, 0);
		}

		public int SaveDocData(VSSAVEFLAGS dwSave, out string pbstrMkDocumentNew, out int pfSaveCanceled)
		{
			pbstrMkDocumentNew = null;
			pfSaveCanceled = 0;

			int hr;

			switch (dwSave)
			{
				case VSSAVEFLAGS.VSSAVE_Save:
				case VSSAVEFLAGS.VSSAVE_SilentSave:
					var askForSave = GetService<SVsQueryEditQuerySave, IVsQueryEditQuerySave2>();

					uint result;
					hr = askForSave.QuerySaveFile(_filename, 0, null, out result);

					if (ErrorHandler.Failed(hr))
					{
						break;
					}

					hr = HandleSaveResponse(dwSave, out pbstrMkDocumentNew, out pfSaveCanceled, (tagVSQuerySaveResult)result);
					break;

				case VSSAVEFLAGS.VSSAVE_SaveAs:
				case VSSAVEFLAGS.VSSAVE_SaveCopyAs:
					FixupFileExtension();
					hr = SaveDocDataToFile(this, _filename, out pbstrMkDocumentNew, out pfSaveCanceled, dwSave);
					break;

				default:
					throw new ArgumentException(Resources.UnsupportedSaveFlag);
			}
			return hr;
		}

		public int Close()
		{
			return VSConstants.S_OK;
		}

		public int OnRegisterDocData(uint docCookie, IVsHierarchy pHierNew, uint itemidNew)
		{
			return VSConstants.S_OK;
		}

		public int RenameDocData(uint grfAttribs, IVsHierarchy pHierNew, uint itemidNew, string pszMkDocumentNew)
		{
			return VSConstants.S_OK;
		}

		public int IsDocDataReloadable(out int pfReloadable)
		{
			pfReloadable = 1;
			return VSConstants.S_OK;
		}

		public int ReloadDocData(uint grfFlags)
		{
			return ((IPersistFileFormat)this).Load(null, grfFlags, 0);
		}

		#endregion

		#region Implementation of IEventHandler<in UrlValidatedEvent>

		public void Handle(UrlValidatedEvent eventData)
		{
			
			_view.ClearUrlError();
			if (!_loading)
			{
				_isDirty = true;
				NotifyDocChanged();
			}
		}

		#endregion

		#region Implementation of IEventHandler<in UrlInvalidatedEvent>

		public void Handle(UrlInvalidatedEvent eventData)
		{
			_view.SetUrlError();
			if (!_loading)
			{
				_isDirty = true;
				NotifyDocChanged();
			}
		}

		#endregion

		#region Implementation of IEventHandler<in TestPlanValidatedEvent>

		public void Handle(TestPlanValidatedEvent eventData)
		{
			_view.ClearTestPlanError();
			if (!_loading)
			{
				_isDirty = true;
				NotifyDocChanged();
			}
		}

		#endregion

		#region Implementation of IEventHandler<in TestPlanInvalidatedEvent>

		public void Handle(TestPlanInvalidatedEvent eventData)
		{
			_view.SetTestPlanError();
			if (!_loading)
			{
				_isDirty = true;
				NotifyDocChanged();
			}
		}

		#endregion

		#region Implementation of IEventHandler<in ProjectSelectedEvent>

		public void Handle(ProjectSelectedEvent eventData)
		{
			if (!_loading)
			{
				_isDirty = true;
				NotifyDocChanged();
			}
		}

		#endregion

		#region Implementation of IEventHandler<in ReleaseSelectedEvent>

		public void Handle(ReleaseSelectedEvent eventData)
		{
			if (!_loading)
			{
				_isDirty = true;
				NotifyDocChanged();
			}
		}

		#endregion

		#region Implementation of IEventHandler<in TestPlanSelectedEvent>

		public void Handle(TestPlanSelectedEvent eventData)
		{
			if (!_loading)
			{
				_isDirty = true;
				NotifyDocChanged();
			}
		}

		#endregion

		#region Implementation of IEventHandler<in BuildProviderSelectedEvent>

		public void Handle(BuildProviderSelectedEvent eventData)
		{
			if (!_loading)
			{
				_isDirty = true;
				NotifyDocChanged();
			}
		}

		#endregion

		#region Implementation of IEventHandler<in OrderedTestSelectedEvent>

		public void Handle(OrderedTestSelectedEvent eventData)
		{
			if (!_loading)
			{
				_isDirty = true;
				NotifyDocChanged();
			}
		}

		#endregion

		private static int GetGuid(out Guid pClassID)
		{
			pClassID = GuidList.GuidTestFileEditorEditorFactory;
			return VSConstants.S_OK;
		}

		private IVsUIShell VsUIShell
		{
			get
			{
				if (_vsUIShell == null)
				{
					_vsUIShell = GetService<SVsUIShell, IVsUIShell>();
				}
				return _vsUIShell;
			}
		}

		private TInterfaceType GetService<TServiceType, TInterfaceType>()
		{
			return (TInterfaceType)base.GetService(typeof(TServiceType));
		}

		private void NotifyDocChanged()
		{
			IVsRunningDocumentTable rdt;
			uint cookie;
			if (!LockDocument(out rdt, out cookie))
			{
				return;
			}

			int hr = rdt.NotifyDocumentChanged(cookie, (uint)__VSRDTATTRIB.RDTA_DocDataIsDirty);

			UnlockDocument(rdt, cookie, hr);
		}


		private void NotifyDocReloaded()
		{
			IVsRunningDocumentTable rdt;
			uint cookie;
			if (!LockDocument(out rdt, out cookie))
			{
				return;
			}

			int hr = rdt.NotifyDocumentChanged(cookie, (uint)__VSRDTATTRIB.RDTA_DocDataReloaded);

			UnlockDocument(rdt, cookie, hr);
		}

		private static void UnlockDocument(IVsRunningDocumentTable rdt, uint cookie, int hr)
		{
			rdt.UnlockDocument((uint)_VSRDTFLAGS.RDT_ReadLock, cookie);

			ErrorHandler.ThrowOnFailure(hr);
		}

		private bool LockDocument(out IVsRunningDocumentTable rdt, out uint cookie)
		{
			if (string.IsNullOrWhiteSpace(_filename))
			{
				rdt = null;
				cookie = 0;
				return false;
			}

			rdt = GetService<SVsRunningDocumentTable, IVsRunningDocumentTable>();

			uint id;
			IVsHierarchy h;
			IntPtr data;

			int hr = rdt.FindAndLockDocument((uint)_VSRDTFLAGS.RDT_ReadLock, _filename, out h, out id, out data, out cookie);

			ErrorHandler.ThrowOnFailure(hr);
			return true;
		}

		private void LoadFile(string filename)
		{
			var serializer = new XmlSerializer(typeof(SlickTest));
			using (var stream = new StreamReader(filename))
			{
				var test = serializer.Deserialize(stream) as SlickTest;
				_appController.Raise(new FileLoadedEvent(test));
			}
		}

		private void SaveFile(string filename)
		{
			var test = new SlickTest();
			_appController.Raise(new SaveDataEvent(test));

			var serializer = new XmlSerializer(typeof(SlickTest));
			using (var stream = new StreamWriter(filename))
			{
				serializer.Serialize(stream, test);
			}

		}

		private void FixupFileExtension()
		{
			var extension = Path.GetExtension(_filename);
			if (string.IsNullOrWhiteSpace(extension) || !extension.Equals(EditorFactory.EXTENSION))
			{
				_filename += EditorFactory.EXTENSION;
			}
		}

		private int HandleSaveResponse(VSSAVEFLAGS dwSave, out string pbstrMkDocumentNew, out int pfSaveCanceled, tagVSQuerySaveResult tagVsQuerySaveResult)
		{
			pbstrMkDocumentNew = null;
			pfSaveCanceled = 0;

			int hr = VSConstants.S_OK;
			switch (tagVsQuerySaveResult)
			{
				case tagVSQuerySaveResult.QSR_NoSave_Continue:
					//Nothing to do
					break;

					// Both the NoSave_*Canceled tags have the same value.
					//case tagVSQuerySaveResult.QSR_NoSave_UserCanceled: 
				case tagVSQuerySaveResult.QSR_NoSave_Cancel:
					pfSaveCanceled = Convert.ToInt32(true);
					break;

				case tagVSQuerySaveResult.QSR_SaveOK:
					hr = SaveDocDataToFile(this, _filename, out pbstrMkDocumentNew, out pfSaveCanceled, dwSave);
					break;

				case tagVSQuerySaveResult.QSR_ForceSaveAs:
					hr = SaveDocDataToFile(this, _filename, out pbstrMkDocumentNew, out pfSaveCanceled, VSSAVEFLAGS.VSSAVE_SaveAs);
					break;

					//case tagVSQuerySaveResult.QSR_NoSave_NoisyPromptRequired:
				default:
					throw new COMException(Resources.NoNoisySave);
			}
			return hr;
		}

		private int SaveDocDataToFile(EditorController editorController, string filename, out string pbstrMkDocumentNew, out int pfSaveCanceled, VSSAVEFLAGS vssaveflags)
		{
			return VsUIShell.SaveDocDataToFile(vssaveflags, editorController, filename, out pbstrMkDocumentNew,
			                                   out pfSaveCanceled);
		}
	}
}