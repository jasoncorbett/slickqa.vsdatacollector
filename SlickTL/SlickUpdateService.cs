using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;

namespace SlickQA.SlickTL
{
    [ServiceContract]
    public interface ISlickUpdateService
    {
        [OperationContract]
        void TestStartedSignal(string className);

        [OperationContract]
        void TestFinishedSignal(string className, string outcome, string[] files);
    }

    [Export(typeof(IFrameworkInitializePart))]
    public class TestStartedSignaler : IFrameworkInitializePart
    {
        public static Logger Log = LogManager.GetCurrentClassLogger();

        public string Name
        {
            get { return "TestStartedSignaler"; }
        }

        public void initialize(object instance)
        {
            try
            {
                var url = String.Format("net.pipe://localhost/{0}", typeof (ISlickUpdateService).FullName);
                var pipeFactory = new ChannelFactory<ISlickUpdateService>(new NetNamedPipeBinding(),
                                                                          new EndpointAddress(url));
                var slickUpdateService = pipeFactory.CreateChannel();
                slickUpdateService.TestStartedSignal(instance.GetType().FullName);
            }
            catch (Exception ex)
            {
                Log.Debug("Problem signaling the test adapter that the test has started, no worries.  For reference the exception was of type {0}, and the message was {1}.", ex.GetType().Name, ex.Message);
            }
        }
    }

    [Export]
    public class TestFinishedSignaler
    {
        public static Logger Log = LogManager.GetCurrentClassLogger();

        [Import]
        public ITestingContext Context { get; set; }

        public void FrameworkCleanupFinished(object instance)
        {
            try
            {
                var url = String.Format("net.pipe://localhost/{0}", typeof (ISlickUpdateService).FullName);
                Log.Debug("Creating factory for url: {0}", url);
                var pipeFactory = new ChannelFactory<ISlickUpdateService>(new NetNamedPipeBinding(),
                                                                          new EndpointAddress(url));
                Log.Debug("Creating Channel");
                var slickUpdateService = pipeFactory.CreateChannel();
                Log.Debug("Signaling test finished for {0}: {1}, with {2} files.", instance.GetType().FullName,
                          Context.CurrentTestOutcome.ToString(), Context.ResultFiles.Count);
                slickUpdateService.TestFinishedSignal(instance.GetType().FullName, Context.CurrentTestOutcome.ToString(),
                                                      Context.ResultFiles.ToArray());
                Log.Debug("Finished signaling test finished.");
            }
            catch (Exception)
            {
                Log.Debug("No reporting to slick happening at this time.  This is most likely because you are not using the test adapter.");
            }
        }
    }
}
