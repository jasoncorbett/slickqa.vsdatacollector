using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using SlickQA.SlickTL;

namespace mstestexample
{
    [TestClass]
    public class UnitTest1 : SlickBaseTest
    {
        public const string Feature = "TestReporter";

        [TestConfiguration("SectionName.Key")]
        public string ExampleConfig { get; set; }

        [TestMethod]
        [TestedFeature(Feature)]
        [TestName("Example Config Test")]
        [TestCategory("config")]
        [TestCategory("reporter")]
        [Description("This checks the value from the ini file")]
        public void ExampleConfigTest()
        {
            ExampleConfig.Should().Be("Value");
        }

        [TestMethod]
        [TestedFeature(Feature)]
        [TestName("Default Pass Test")]
        [TestCategory("shouldpass")]
        [TestCategory("reporter")]
        [Description("This test should always pass.")]
        public void ShouldPass()
        {
            var value = "Assertions are easy";
            value.Should().StartWith("Assertions").And.EndWith("easy");
        }

        [TestMethod]
        [TestedFeature(Feature)]
        [TestName("Default Fail Test")]
        [TestCategory("shouldfail")]
        [TestCategory("reporter")]
        [Description("This test should fail")]
        public void ShouldFail()
        {
            true.Should().Be(false);
        }

        [TestMethod]
        [TestName("Default Unexpected Exception Test")]
        [TestCategory("shouldfail")]
        [TestCategory("reporter")]
        [TestedFeature(Feature)]
        public void UnexpectedException()
        {
            throw new Exception("Not Important");
        }

        [TestMethod]
        [TestedFeature(Feature)]
        [TestName("Add a text file to result")]
        [TestCategory("shouldpass")]
        [TestCategory("reporter")]
        [Description("This test adds a file")]
        public void AddAFileTest()
        {
            TestContext.Should().NotBeNull();
            var filepath = Path.Combine(TestContext.TestRunDirectory, TestContext.FullyQualifiedTestClassName + ".txt");
            using (var testfile = new StreamWriter(filepath))
            {
                testfile.WriteLine("This is an example test file");
            }
            TestContext.AddResultFile(filepath);

        }

        [TestMethod]
        [TestedFeature(Feature)]
        [TestCategory("shouldfail")]
        [TestCategory("reporter")]
        [TestName("Generate Movie")]
        [Description("User must click on dialog box")]
        public void DialogBoxTest()
        {
            TestLog.Debug("Inside DialogBoxTest()");
            MessageBox.Show("Welcome to our test movie");
            TestLog.Info("User just dismissed the Welcome dialog box.");
            MessageBox.Show("Click on this box to end the test.");
            TestLog.Info("User just dismissed the end the test dialog box.");
            TestLog.Error("We are about to fail the test");
            "I'm only creating movies for failed tests".Should().BeEmpty();
        }
    }
}
