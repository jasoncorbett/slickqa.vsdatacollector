using System.Collections.Generic;
using System.ComponentModel.Composition;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;

namespace SlickQA.SlickTL
{
    public interface ITestingContext
    {
        List<string> ResultFiles { get; }

        UnitTestOutcome CurrentTestOutcome { get; }

        void AddResultFile(string pathname);

        void Initialize(object instance, TestContext context);
    }

    [Export(typeof(ITestingContext))]
    class TestingContext : ITestingContext
    {
        public List<string> ResultFiles { get; set; }

        public UnitTestOutcome CurrentTestOutcome { get { return RealContext.CurrentTestOutcome; } }

        private TestContext RealContext { get; set; }

        private static Logger Log = LogManager.GetCurrentClassLogger();

        public void Initialize(object instance, TestContext context)
        {
            ResultFiles = new List<string>();
            RealContext = context;
        }

        public void AddResultFile(string pathname)
        {
            Log.Debug("Adding result file '{0}'.", pathname);
            ResultFiles.Add(pathname);
            RealContext.AddResultFile(pathname);
        }
    }
}
