using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using NLog;
using NLog.Config;
using NLog.Targets;
using SlickQA.DataCollector.Attributes;

namespace mstestexample
{
    [TestClass]
    public class UnitTest1
    {
        public const string Feature = "TestReporter";

        public TestContext TestContext { get; set; }

        public static Logger Log = LogManager.GetLogger("testcase.UnitTest1");

        public static int TestCounter { get; set; }

        public string LogFileName { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            // Step 1. Create configuration object 
            var config = new LoggingConfiguration();

            // Step 2. Create targets and add them to the configuration 
            var xmlLogFile = new FileTarget();
            var testpath = Path.Combine(TestContext.TestRunDirectory, String.Format("{0}-{1}", (++TestCounter), TestContext.TestName));
            Directory.CreateDirectory(testpath);
            LogFileName = Path.Combine(testpath, "testlog.xml");
            xmlLogFile.FileName = LogFileName;
            config.AddTarget("test-log-file", xmlLogFile);

            // Step 3. Set target properties 
            //xmlLogFile.Header = "<?xml version=\"1.0\" ?>" + Environment.NewLine + "<LogEntries>" + Environment.NewLine + "\t<Entries>";
            xmlLogFile.Header = "<?xml version=\"1.0\" ?>" + Environment.NewLine + "<LogEntries>";
            xmlLogFile.Layout = "\t\t<LocalLogEntry><EntryTime>${date:universalTime=true:format=ddd, dd MMM yyyy HH\\:mm\\:ss \"GMT\":xmlEncode=true}</EntryTime><Level>${level:xmlEncode=true}</Level><LoggerName>${logger:xmlEncode=true}</LoggerName><Message>${message:xmlEncode=true}</Message><ExceptionClassName>${exception:innerFormat=Type:xmlEncode=true}</ExceptionClassName><ExceptionMessage>${exception:innerFormat=Message:xmlEncode=true}</ExceptionMessage><ExceptionStackTrace>${onexception:inner=${stacktrace:format=Raw:xmlEncode=true}}</ExceptionStackTrace></LocalLogEntry>";
            xmlLogFile.Footer = "</LogEntries>";
            //xmlLogFile.Footer = "\t</Entries>" + Environment.NewLine + "</LogEntries>";

            // Step 4. Define rules
            var rule1 = new LoggingRule("*", LogLevel.Debug, xmlLogFile);
            config.LoggingRules.Clear();
            config.LoggingRules.Add(rule1);

            // Step 5. Activate the configuration
            LogManager.Configuration = config;

        }

        [TestCleanup]
        public void Cleanup()
        {
            LogManager.Configuration = null;
            TestContext.AddResultFile(LogFileName);
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
            Log.Debug("Inside DialogBoxTest()");
            MessageBox.Show("Welcome to our test movie");
            Log.Info("User just dismissed the Welcome dialog box.");
            MessageBox.Show("Click on this box to end the test.");
            Log.Info("User just dismissed the end the test dialog box.");
            Log.Error("We are about to fail the test");
            "I'm only creating movies for failed tests".Should().BeEmpty();
        }
    }
}
