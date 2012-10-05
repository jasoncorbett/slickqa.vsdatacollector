using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using ValidateArg = Microsoft.VisualStudio.TestPlatform.ObjectModel.ValidateArg;

namespace SlickEventListeners
{
	[Export(typeof(IFileAddRemoveWatcher))]
	public sealed class FileAddRemoveWatcher : IFileAddRemoveWatcher, IDisposable, IVsTrackProjectDocumentsEvents2
	{
		private readonly IVsTrackProjectDocuments2 _projectDocumentTracker;
		private uint _cookie = VSConstants.VSCOOKIE_NIL;

		[ImportingConstructor]
		public FileAddRemoveWatcher([Import(typeof(SVsServiceProvider))] IServiceProvider serviceProvider)
		{
			ValidateArg.NotNull(serviceProvider, "serviceProvider");

			_projectDocumentTracker = serviceProvider.GetService(typeof(SVsTrackProjectDocuments)) as IVsTrackProjectDocuments2;
		}

		public void Start()
		{
			if (_projectDocumentTracker == null)
			{
				return;
			}
			int hresult = _projectDocumentTracker.AdviseTrackProjectDocumentsEvents(this, out _cookie);
			ErrorHandler.ThrowOnFailure(hresult);
		}

		public void Stop()
		{
			if (_cookie == VSConstants.VSCOOKIE_NIL || _projectDocumentTracker == null)
			{
				return;
			}
			int hresult = _projectDocumentTracker.UnadviseTrackProjectDocumentsEvents(_cookie);
			ErrorHandler.Succeeded(hresult);

			_cookie = VSConstants.VSCOOKIE_NIL;
		}

		public int OnQueryAddFiles(IVsProject pProject, int cFiles, string[] rgpszMkDocuments, VSQUERYADDFILEFLAGS[] rgFlags,
		                           VSQUERYADDFILERESULTS[] pSummaryResult, VSQUERYADDFILERESULTS[] rgResults)
		{
			return VSConstants.S_OK;
		}

		public int OnAfterAddFilesEx(int cProjects, int cFiles, IVsProject[] rgpProjects, int[] rgFirstIndices,
		                             string[] rgpszMkDocuments, VSADDFILEFLAGS[] rgFlags)
		{
			return OnFileAddRemove(cProjects, rgpProjects, rgpszMkDocuments, rgFirstIndices, FileChangedReason.Added);
		}

		public int OnAfterAddDirectoriesEx(int cProjects, int cDirectories, IVsProject[] rgpProjects, int[] rgFirstIndices,
		                                   string[] rgpszMkDocuments, VSADDDIRECTORYFLAGS[] rgFlags)
		{
			return VSConstants.S_OK;
		}

		public int OnAfterRemoveFiles(int cProjects, int cFiles, IVsProject[] rgpProjects, int[] rgFirstIndices,
		                              string[] rgpszMkDocuments, VSREMOVEFILEFLAGS[] rgFlags)
		{
			return OnFileAddRemove(cProjects, rgpProjects, rgpszMkDocuments, rgFirstIndices, FileChangedReason.Removed);
		}

		public int OnAfterRemoveDirectories(int cProjects, int cDirectories, IVsProject[] rgpProjects, int[] rgFirstIndices,
		                                    string[] rgpszMkDocuments, VSREMOVEDIRECTORYFLAGS[] rgFlags)
		{
			return VSConstants.S_OK;
		}

		public int OnQueryRenameFiles(IVsProject pProject, int cFiles, string[] rgszMkOldNames, string[] rgszMkNewNames,
		                              VSQUERYRENAMEFILEFLAGS[] rgFlags, VSQUERYRENAMEFILERESULTS[] pSummaryResult,
		                              VSQUERYRENAMEFILERESULTS[] rgResults)
		{
			return VSConstants.S_OK;
		}

		public int OnAfterRenameFiles(int cProjects, int cFiles, IVsProject[] rgpProjects, int[] rgFirstIndices,
		                              string[] rgszMkOldNames, string[] rgszMkNewNames, VSRENAMEFILEFLAGS[] rgFlags)
		{
			OnFileAddRemove(cProjects, rgpProjects, rgszMkOldNames, rgFirstIndices, FileChangedReason.Removed);
			return OnFileAddRemove(cProjects, rgpProjects, rgszMkNewNames, rgFirstIndices, FileChangedReason.Added);
		}

		public int OnQueryRenameDirectories(IVsProject pProject, int cDirs, string[] rgszMkOldNames, string[] rgszMkNewNames,
		                                    VSQUERYRENAMEDIRECTORYFLAGS[] rgFlags,
		                                    VSQUERYRENAMEDIRECTORYRESULTS[] pSummaryResult,
		                                    VSQUERYRENAMEDIRECTORYRESULTS[] rgResults)
		{
			return VSConstants.S_OK;
		}

		public int OnAfterRenameDirectories(int cProjects, int cDirs, IVsProject[] rgpProjects, int[] rgFirstIndices,
		                                    string[] rgszMkOldNames, string[] rgszMkNewNames, VSRENAMEDIRECTORYFLAGS[] rgFlags)
		{
			return VSConstants.S_OK;
		}

		public int OnQueryAddDirectories(IVsProject pProject, int cDirectories, string[] rgpszMkDocuments,
		                                 VSQUERYADDDIRECTORYFLAGS[] rgFlags, VSQUERYADDDIRECTORYRESULTS[] pSummaryResult,
		                                 VSQUERYADDDIRECTORYRESULTS[] rgResults)
		{
			return VSConstants.S_OK;
		}

		public int OnQueryRemoveFiles(IVsProject pProject, int cFiles, string[] rgpszMkDocuments,
		                              VSQUERYREMOVEFILEFLAGS[] rgFlags, VSQUERYREMOVEFILERESULTS[] pSummaryResult,
		                              VSQUERYREMOVEFILERESULTS[] rgResults)
		{
			return VSConstants.S_OK;
		}

		public int OnQueryRemoveDirectories(IVsProject pProject, int cDirectories, string[] rgpszMkDocuments,
		                                    VSQUERYREMOVEDIRECTORYFLAGS[] rgFlags,
		                                    VSQUERYREMOVEDIRECTORYRESULTS[] pSummaryResult,
		                                    VSQUERYREMOVEDIRECTORYRESULTS[] rgResults)
		{
			return VSConstants.S_OK;
		}

		public int OnAfterSccStatusChanged(int cProjects, int cFiles, IVsProject[] rgpProjects, int[] rgFirstIndices,
		                                   string[] rgpszMkDocuments, uint[] rgdwSccStatus)
		{
			return VSConstants.S_OK;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (!disposing)
			{
				return;
			}
			Stop();
		}

		private int OnFileAddRemove(int changedProjectCount, IVsProject[] changedProjects, string[] changedProjectItems,
		                            int[] rgFirstIndices, FileChangedReason reason)
		{
			// The way these parameters work is:
			// rgFirstIndices contains a list of the starting index into the changeProjectItems array for each project listed in the changedProjects list
			// Example: if you get two projects, then rgFirstIndices should have two elements, the first element is probably zero since rgFirstIndices would start at zero.
			// Then item two in the rgFirstIndices array is where in the changeProjectItems list that the second project's changed items reside.
			int projItemIndex = 0;
			for (int changeProjIndex = 0; changeProjIndex < changedProjectCount; changeProjIndex++)
			{
				int endProjectIndex = ((changeProjIndex + 1) == changedProjectCount)
					                      ? changedProjectItems.Length
					                      : rgFirstIndices[changeProjIndex + 1];

				for (; projItemIndex < endProjectIndex; projItemIndex++)
				{
					if (changedProjects[changeProjIndex] != null && FileChanged != null)
					{
						FileChanged(this, new FileChangedEventArgs(changedProjectItems[projItemIndex], reason));
					}
				}
			}
			return VSConstants.S_OK;
		}


		public event EventHandler<FileChangedEventArgs> FileChanged;
	}
}