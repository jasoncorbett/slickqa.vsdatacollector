using System;
using System.ComponentModel.Composition;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace SlickQA.SlickTL
{
    [Export]
    public class LoggingManager
    {
        [Import]
        private DirectoryManager Directories { get; set; }

        [Import]
        public ITestingContext Context { get; set; }

        private String LogFileName { get; set; }

        public void initialize(object testinstance)
        {
            // Step 1. Create configuration object 
            var config = new LoggingConfiguration();

            // Step 2. Create targets and add them to the configuration 
            var xmlLogFile = new FileTarget();
            LogFileName = Path.Combine(Directories.CurrentTestOutputDirectory, "testlog.xml");
            xmlLogFile.FileName = LogFileName;
            config.AddTarget("test-log-file", xmlLogFile);

            // Step 3. Set target properties 
            //xmlLogFile.Header = "<?xml version=\"1.0\" ?>" + Environment.NewLine + "<LogEntries>" + Environment.NewLine + "\t<Entries>";
            xmlLogFile.Header = "<?xml version=\"1.0\" ?>" + Environment.NewLine + "<LogEntries>";
            xmlLogFile.Layout = "\t\t<LocalLogEntry><EntryTime>${date:universalTime=true:format=ddd, dd MMM yyyy HH\\:mm\\:ss \"GMT\":xmlEncode=true}</EntryTime><Level>${level:xmlEncode=true}</Level><LoggerName>${logger:xmlEncode=true}</LoggerName><Message>${message:xmlEncode=true}</Message><ExceptionClassName>${exception:innerFormat=Type:xmlEncode=true}</ExceptionClassName><ExceptionMessage>${exception:innerFormat=Message:xmlEncode=true}</ExceptionMessage><ExceptionStackTrace>${onexception:inner=${stacktrace:format=Raw:xmlEncode=true}}</ExceptionStackTrace></LocalLogEntry>";
            xmlLogFile.Footer = "</LogEntries>";
            //xmlLogFile.Footer = "\t</Entries>" + Environment.NewLine + "</LogEntries>";

            var consoleLog = new ConsoleTarget();
            consoleLog.Layout = "${date:format=hh\\:mm\\:ss.FFF tt}|${level:uppercase=true}|${logger}]: ${message}${onexception:inner=\r\n\t${exception}\r\n\t${stacktrace:format=Raw}}";

            // Step 4. Define rules
            var rule1 = new LoggingRule("*", LogLevel.Debug, xmlLogFile);
            config.LoggingRules.Clear();
            config.LoggingRules.Add(rule1);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, consoleLog));

            // Step 5. Activate the configuration
            LogManager.Configuration = config;

        }

        public void cleanup()
        {
            LogManager.Configuration = null;

            // Make sure console logging continues
            var config = new LoggingConfiguration();
            var consoleLog = new ConsoleTarget();
            consoleLog.Layout = "${date:format=hh\\:mm\\:ss.FFF tt}|${level:uppercase=true}|${logger}]: ${message}${onexception:inner=\r\n\t${exception}\r\n\t${stacktrace:format=Raw}}";
            config.LoggingRules.Clear();
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, consoleLog));
            LogManager.Configuration = config;

            Context.AddResultFile(LogFileName);
        }
    }
}
