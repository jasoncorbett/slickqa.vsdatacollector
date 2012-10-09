using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace mstestexample
{
    [TestClass]
    public class UnitTest1
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [Description("This test should always pass.")]
        public void ShouldPass()
        {
            var value = "Assertions are easy";
            value.Should().StartWith("Assertions").And.EndWith("easy");
        }

        [TestMethod]
        [Description("This test should fail")]
        public void ShouldFail()
        {
            true.Should().Be(false);
        }

        [TestMethod]
        public void UnexpectedException()
        {
            throw new Exception("Not Important");
        }

        [TestMethod]
        [Description("This test will show off a lot of the attributes and information we can add to slick.")]
        public void DecoratedTest()
        {
            
        }

        [TestMethod]
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
        [Description("User must click on dialog box")]
        public void DialogBoxTest()
        {
            MessageBox.Show("Welcome to our test movie");
            MessageBox.Show("Click on this box to end the test.");
            "I'm only creating movies for failed tests".Should().BeEmpty();
        }
    }
}
