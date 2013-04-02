using System;
using System.ComponentModel.Composition;

using FluentAssertions;
using FluentAssertions.Assertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlickQA.SlickTL;

namespace mstestexample
{
    [TestClass]
    public class SlickTLTests : SlickScreenshotEnabledBaseTest
    {

        [TestConfiguration("SectionName.Key")]
        public string ExampleConfig { get; set; }

        [TestConfiguration("ValueTypes.Integer")]
        public int IntValue { get; set; }

        [TestConfiguration("ValueTypes.UseDefault", DefaultValue = "42")]
        public int IntUseDefault { get; set; }

        [TestConfiguration("ValueTypes.Boolean")]
        public bool BoolValue { get; set; }

        [TestConfiguration("ValueTypes.UseDefault", DefaultValue = "true")]
        public bool BoolUseDefault { get; set; }

        [Import]
        public DirectoryManager Directories { get; set; }

        [TestInitialize]
        public void Setup()
        {
            TestLog.Debug("Inside Setup");
            Directories.Should().NotBeNull();
            ExampleConfig.Should().Be("Value");
        }

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

        [TestMethod]
        [TestName("Wait.For timeout test")]
        [TestAuthor("Jason Corbett")]
        [TestedFeature("SlickTL")]
        [TestCategory("slicktl")]
        [Description("This test will check that the timeout on Wait.For happens correctly.")]
        public void WaitForTimeoutError()
        {
            Step("Run a Wait.For that always returns false with a 10 second timeout.", "Timeout Exception occurs.");
            Action act = () => Wait.For(() => false, "A method that always returns false", 10);

            act.ShouldThrow<TimeoutError>().WithMessage("always returns false", ComparisonMode.Substring);
        }

        [TestMethod]
        [TestName("Wait.For successful test")]
        [TestAuthor("Jason Corbett")]
        [TestedFeature("SlickTL")]
        [TestCategory("slicktl")]
        [Description("This test will check that Wait.For waits for a condition to be true.")]
        public void WaitForSuccessful()
        {
            Step("Run a Wait.For that returns true if 3 seconds has passed by.", "Wait.For does not throw an exception, and takes a max of 5 seconds.");
            DateTime start = DateTime.Now;
            Action act = () => Wait.For(delegate() { return (DateTime.Now - start).TotalSeconds > 2; }, "A method that checks that more than 2 seconds has passed by", 10);

            act.ShouldNotThrow<TimeoutError>();
            (DateTime.Now - start).TotalSeconds.Should()
                                  .BeLessThan(6,
                                              "The difference between the start time and now should be less than 6 seconds.");
        }

        [DoNotReport]
        [TestName("DoNotReport Test")]
        [Description("This test shouldn't be reported, but should be run.")]
        [TestMethod]
        public void DoNotReportTest()
        {
            Step("This won't be used.");
        }

        [TestName("Configuration Value Types")]
        [Description("This test makes sure that we can get integer and boolean configuration values from the ini file.")]
        [TestMethod]
        public void ConfigurationValueTest()
        {
            IntValue.Should().BeGreaterThan(0);
            IntUseDefault.Should().Be(42);
            BoolValue.Should().BeTrue();
            BoolUseDefault.Should().BeTrue();
        }
    }
}
