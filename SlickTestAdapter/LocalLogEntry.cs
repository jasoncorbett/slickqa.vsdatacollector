﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SlickQA.SlickSharp.Logging;

namespace SlickQA.TestAdapter
{
    public class LocalLogEntry
    {
        public string EntryTime { get; set; }
        public string Level { get; set; }
        public string LoggerName { get; set; }
        public string Message { get; set; }
        public string ExceptionClassName { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionStackTrace { get; set; }

        public LogEntry ConvertToSlickLogEntry()
        {
            var retval = new LogEntry();
            retval.EntryTime = EntryTime;
            retval.Level = Level.ToUpper();
            retval.LoggerName = LoggerName;
            retval.Message = Message;
            retval.ExceptionClassName = ExceptionClassName;
            retval.ExceptionMessage = ExceptionMessage;
            if (!String.IsNullOrWhiteSpace(ExceptionStackTrace))
                retval.ExceptionStackTrace = new List<string>(ExceptionStackTrace.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));
            return retval;
        }

        public static List<LogEntry> ReadFromFile(string filepath)
        {
            var retval = new List<LogEntry>();

            var serializer = new XmlSerializer(typeof (List<LocalLogEntry>), new XmlRootAttribute() {ElementName = "LogEntries", Namespace = ""});
            using (var logfile = new FileStream(filepath, FileMode.Open))
            {
                var log = (List<LocalLogEntry>) serializer.Deserialize(logfile);
                if (log != null && log.Count > 0)
                {
                    foreach (var localLogEntry in log)
                        retval.Add(localLogEntry.ConvertToSlickLogEntry());
                }
            }

            return retval;
        }

    }
}