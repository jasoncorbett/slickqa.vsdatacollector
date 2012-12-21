using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Assertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlickQA.SlickTL;

namespace mstestexample
{
    [TestClass]
    public class DoubleInitialize : SlickScreenshotEnabledBaseTest
    {

        [TestInitialize]
        public void Setup()
        {
            TestLog.Debug("Inside Setup");
            TestLog.Debug("Calling second initialize...");
            FrameworkUtility.Initialize(this, TestContext);
        }

        [TestMethod]
        [TestName("Double Initialize Test")]
        [TestedFeature("SlickTL")]
        [Description("This test will call initialize a second time, and it shouldn't cause a problem.")]
        public void DoubleInitializeTest()
        {
            TestLog.Debug("Hopefully you see a message about initialize already having been called.");
        }
    }
}
