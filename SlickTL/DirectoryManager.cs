using System;
using System.ComponentModel.Composition;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;

namespace SlickQA.SlickTL
{
    [Export]
    public class DirectoryManager
    {
        public string CurrentTestOutputDirectory { get; set; }

        public string TestBaseDirectory { get; set; }

        public string ConfigurationDirectory { get; set; }

        private static int TestCount { get; set; }
        private static Logger Log = LogManager.GetCurrentClassLogger();

        public void initialize(object instance, TestContext context)
        {
            CurrentTestOutputDirectory = Path.Combine(Environment.CurrentDirectory,
                                                      (++TestCount) + "-" + context.TestName);
            if (context.Properties.Contains("OutputDirectory"))
                context.Properties["OutputDirectory"] = CurrentTestOutputDirectory;
            else
                context.Properties.Add("OutputDirectory", CurrentTestOutputDirectory);
            if (!Directory.Exists(CurrentTestOutputDirectory))
                Directory.CreateDirectory(CurrentTestOutputDirectory);

            TestBaseDirectory = Environment.CurrentDirectory;

            // stupid visual studio deploys everything to the root directory
            //ConfigurationDirectory = Path.Combine(TestBaseDirectory, "configurations");
            ConfigurationDirectory = TestBaseDirectory;
        }
    }
}
