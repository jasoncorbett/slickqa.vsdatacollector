﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlickQA.SlickTL;

namespace mstestexample
{
    [TestClass]
    public class SlickTLTests : SlickScreenshotEnabledBaseTest
    {

        [TestConfiguration("SectionName.Key")]
        public string ExampleConfig { get; set; }

        [TestMethod]
        [TestName("Example Slick Base Test")]
        [TestedFeature("SlickTL")]
        [Description("This test will make sure that the various services in the slick base test work.")]
        public void ExampleSlickTest()
        {
            AlwaysTakeScreenshot = true;
            ExampleConfig.Should().Be("Value");
            TestLog.Debug("This is an example logging method, ExampleConfig={0}", ExampleConfig);
        }

        [TestMethod]
        [TestName("Take Screenshot only on failure")]
        [TestAuthor("Jason Corbett")]
        [TestedFeature("SlickTL")]
        [TestCategory("slicktl")]
        [TestCategory("expectedfail")]
        [Description("This test will make sure that a screenshot will be taken on failure specifically.")]
        public void TakeFailureScreenshot()
        {
            AlwaysTakeScreenshot = false;
            "This".Should().Be("That");
        }

        [TestMethod]
        [TestName("Test Steps Included")]
        [TestAuthor("Jason Corbett")]
        [TestedFeature("SlickTL")]
        [TestCategory("slicktl")]
        [Description("This test will include steps and they should get uploaded by the TestAdapter.")]
        public void TestWithSteps()
        {
            Step("Check that ok==ok", "String 'ok' is equal to 'ok'");
            "ok".Should().Be("ok");
            Step("Check that foo!=bar", "String 'foo' is not equal to string 'bar'");
            "foo".Should().NotBe("bar");
            Step("All Done.");
        }
    }
}