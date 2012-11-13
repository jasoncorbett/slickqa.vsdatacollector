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
        void TestFinishedSignal(string className, string outcome, string[] files);
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
