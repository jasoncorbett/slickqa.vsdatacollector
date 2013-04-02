using System;
using System.ComponentModel.Composition;
using System.Drawing.Imaging;

using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;

namespace SlickQA.SlickTL
{
    [CodedUITest]
    public class CodedUIScreenshotBaseTest
    {
        public bool AlwaysTakeScreenshot { get; set; }

        public TestContext TestContext { get; set; }

        public Logger TestLog { get; set; }

        [Import]
        public ITestStepUtility StepUtility { get; set; }

        public int StepCounter { get; set; }


        [Import]
        ITestingContext Context { get; set; }

        [Import]
        DirectoryManager TestDirectories { get; set; }

        [TestInitialize]
        public void InitializeFramework()
        {
            AlwaysTakeScreenshot = true;
            TestLog = LogManager.GetLogger("testcase." + GetType().Name);
            FrameworkUtility.Initialize(this, TestContext);
            StepCounter = 0;
        }

        public virtual void AttachScreenshotToResult(string filename, ImageFormat format = null)
        {
            SlickScreenshotEnabledBaseTest.DoScreenshotAndAttach(filename, Context, TestLog, TestDirectories, format);
        }

        [TestCleanup]
        public void TakeResultScreenshot()
        {
            if (Context.CurrentTestOutcome != UnitTestOutcome.Passed || AlwaysTakeScreenshot)
            {
                AttachScreenshotToResult("EndResult");
            }
            FrameworkUtility.Cleanup(this, TestContext);
        }

        public virtual void Step(string stepName, string expectedResult = "")
        {
            try
            {
                if (String.IsNullOrWhiteSpace(expectedResult))
                    TestLog.Info("Step {0}: {1}", ++StepCounter, stepName);
                else
                    TestLog.Info("Step {0}: {1}, with expected result {2}", ++StepCounter, stepName, expectedResult);
                StepUtility.AddStep(stepName, expectedResult);
            }
            catch (Exception e)
            {
                if (String.IsNullOrWhiteSpace(expectedResult))
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
    }
}
