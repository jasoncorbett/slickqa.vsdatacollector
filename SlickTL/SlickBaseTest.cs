using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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

        [Import]
        public ITestStepUtility StepUtility { get; set; }

        public int StepCounter { get; set; }

        [TestInitialize]
        public void initializeFramework()
        {
            TestLog = LogManager.GetLogger("testcase." + this.GetType().Name);
            FrameworkUtility.Initialize(this, TestContext);
            StepCounter = 0;
        }

        public virtual void Step(string stepName, string expectedResult = "")
        {
            try
            {
                if(String.IsNullOrWhiteSpace(expectedResult))
                    TestLog.Info("Step {0}: {1}", ++StepCounter, stepName);
                else
                    TestLog.Info("Step {0}: {1}, with expected result {2}", ++StepCounter, stepName, expectedResult);
                StepUtility.AddStep(stepName, expectedResult);
            }
            catch (Exception e)
            {
                if(String.IsNullOrWhiteSpace(expectedResult))
                {
                    TestLog.Warn("Unable to add step {0} with name '{1}' because of exception {2}: {3}", StepCounter, stepName, e.GetType().FullName, e.Message);
                    TestLog.Warn("Exception:", e);
                }
                else
                {
                    TestLog.Warn("Unable to add step {0} with name '{1}' and expected result '{2}' because of exception {3}: {4}", StepCounter, stepName, expectedResult, e.GetType().FullName, e.Message);
                    TestLog.Warn("Exception:", e);
                }

            }
        }

        [TestCleanup]
        public void cleanupFramework()
        {
            FrameworkUtility.Cleanup(this, TestContext);
        }
    }
}
