using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace SlickEventListeners
{
	[Export(typeof(ISolutionEventsListener))]
    public class SolutionEventsListener : IVsSolutionEvents, ISolutionEventsListener
    {
		private readonly IVsSolution _solution;
		private uint _cookie = VSConstants.VSCOOKIE_NIL;

		[ImportingConstructor]
		public SolutionEventsListener([Import(typeof(SVsServiceProvider))] IServiceProvider serviceProvider)
		{
			ValidateArg.NotNull(serviceProvider, "serviceProvider");
			_solution = serviceProvider.GetService(typeof(SVsSolution)) as IVsSolution;
		}

	    public int OnAfterOpenProject(IVsHierarchy pHierarchy, int fAdded)
	    {
			return VSConstants.S_OK;
		}

	    public int OnQueryCloseProject(IVsHierarchy pHierarchy, int fRemoving, ref int pfCancel)
	    {
			return VSConstants.S_OK;
	    }

	    public int OnBeforeCloseProject(IVsHierarchy pHierarchy, int fRemoved)
	    {
			return VSConstants.S_OK;
		}

	    public int OnAfterLoadProject(IVsHierarchy pStubHierarchy, IVsHierarchy pRealHierarchy)
	    {
		    var project = pRealHierarchy as IVsProject;
		    OnProjectUpdate(project, SolutionChangeReason.Load);

		    return VSConstants.S_OK;
	    }

	    public int OnQueryUnloadProject(IVsHierarchy pRealHierarchy, ref int pfCancel)
	    {
			return VSConstants.S_OK;
	    }

	    public int OnBeforeUnloadProject(IVsHierarchy pRealHierarchy, IVsHierarchy pStubHierarchy)
	    {
			var project = pRealHierarchy as IVsProject;
			OnProjectUpdate(project, SolutionChangeReason.Load);

			return VSConstants.S_OK;
	    }

		public int OnAfterOpenSolution(object pUnkReserved, int fNewSolution)
	    {
			return VSConstants.S_OK;
		}

	    public int OnQueryCloseSolution(object pUnkReserved, ref int pfCancel)
	    {
			return VSConstants.S_OK;
	    }

	    public int OnBeforeCloseSolution(object pUnkReserved)
	    {
			return VSConstants.S_OK;
	    }

	    public int OnAfterCloseSolution(object pUnkReserved)
	    {
		    OnSolutionUnloaded();
			return VSConstants.S_OK;
	    }

		public void Start()
	    {
			var hresult = _solution.AdviseSolutionEvents(this, out _cookie);
			ErrorHandler.ThrowOnFailure(hresult);
	    }

	    public void Stop()
	    {
		    if (_cookie == VSConstants.VSCOOKIE_NIL || _solution == null)
		    {
			    return;
		    }

		    var hresult = _solution.UnadviseSolutionEvents(_cookie);
		    ErrorHandler.Succeeded(hresult);

		    _cookie = VSConstants.VSCOOKIE_NIL;
	    }

		private void OnProjectUpdate(IVsProject project, SolutionChangeReason reason)
		{
			if (SolutionProjectChanged != null && project != null)
			{
				SolutionProjectChanged(this, new SolutionProjectChangedEventArgs(project, reason));
			}
		}

		private void OnSolutionUnloaded()
		{
			if (SolutionUnloaded != null)
			{
				SolutionUnloaded(this, new EventArgs());
			}
		}

	    public event EventHandler<SolutionProjectChangedEventArgs> SolutionProjectChanged;
	    public event EventHandler SolutionUnloaded;
    }
}
