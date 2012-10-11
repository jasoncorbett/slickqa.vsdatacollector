using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;

namespace SlickQA.SlickTL
{
    [TestClass]
    public abstract class SlickBaseTest
    {
        public TestContext TestContext { get; set; }

        public Logger TestLog { get; set; }

        [TestInitialize]
        public void initializeFramework()
        {
            TestLog = LogManager.GetLogger("testcase." + this.GetType().Name);
            FrameworkUtility.Initialize(this, TestContext);
        }

        public virtual void Step(string stepName, string expectedResult = null)
        {
            
        }

        [TestCleanup]
        public void cleanupFramework()
        {
            FrameworkUtility.Cleanup(this, TestContext);
        }
    }
}
