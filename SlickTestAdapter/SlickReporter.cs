using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using SlickQA.DataCollector.Models;
using SlickQA.SlickSharp;
using SlickQA.SlickSharp.Logging;
using SlickQA.SlickSharp.ObjectReferences;
using SlickQA.SlickSharp.Utility;
using SlickQA.SlickSharp.Web;

namespace SlickQA.TestAdapter
{
    public class SlickReporter
    {
        public SlickTest SlickRunInfo { get; set; }
        public Project Project { get; set; }
        public Release Release { get; set; }
        public Build Build { get; set; }
        public TestPlan Testplan { get; set; }
        public TestRun Testrun { get; set; }
        public List<Result> Results { get; set; }
        public int TestCount { get; set; }
        public bool CreateIfNecessaryMode { get; set; }
        public IDictionary<string, Component> ComponentCache { get; set; }


        public SlickReporter(SlickTest slickInfo, bool createIfNecessary = true)
        {
            Results = new List<Result>();
            ComponentCache = new Dictionary<string, Component>();
            CreateIfNecessaryMode = createIfNecessary;
            SlickRunInfo = slickInfo;
            TestCount = 0;
        }



        public void Initialize()
        {
            ServerConfig.Scheme = SlickRunInfo.Url.Scheme;
            ServerConfig.SlickHost = SlickRunInfo.Url.HostName;
            ServerConfig.Port = SlickRunInfo.Url.Port;
            ServerConfig.SitePath = SlickRunInfo.Url.SitePath;

            InitializeProject();
            InitializeRelease();
            InitializeBuild();
            InitializeTestplan();
            InitializeTestrun();
        }

        public void RecordEmptyResults()
        {
            foreach (var testinfo in SlickRunInfo.Tests)
            {
                Component component = null;
                if(!String.IsNullOrWhiteSpace(testinfo.Component))
                {
                    if (ComponentCache.ContainsKey(testinfo.Component))
                    {
                        component = ComponentCache[testinfo.Component];
                    }
                    else
                    {
                        component = new Component()
                                        {
                                            Name = testinfo.Component,
                                            Code = testinfo.Component,
                                            ProjectId = Project.Id
                                        };
                        component.Get(CreateIfNecessaryMode, 3);
                    }
                }
                Testcase test = Testcase.GetTestCaseByAutomationKey(testinfo.AutomationKey);
                if (test == null)
                {
                    test = new Testcase()
                        {
                            AutomationKey = testinfo.AutomationKey,
                            Attributes = new LinkedHashMap<string>(testinfo.Attributes),
                            AutomationTool = "mstest",
                            AutomationId = testinfo.Id,
                            ProjectReference = Project,
                            ComponentReference = component,
                            Purpose = testinfo.Description,
                            Tags = testinfo.Tags,
                            Name = testinfo.Name,
                            Author = testinfo.Author,
                            IsAutomated = true
                        };
                    test.Post();
                }
                else
                {
                    // Update just in case something has changed
                    test.Name = testinfo.Name;
                    test.ComponentReference = component;
                    test.Tags = testinfo.Tags;
                    test.Purpose = testinfo.Description;
                    test.IsAutomated = true;
                    test.IsDeleted = false;
                    test.AutomationTool = "mstest";
                    test.Attributes = new LinkedHashMap<string>(testinfo.Attributes);
                    test.Author = testinfo.Author;
                    test.Put();
                }
                Result result = new Result()
                                    {
                                        TestcaseReference = test,
                                        TestRunReference = Testrun,
                                        ProjectReference = Project,
                                        ComponentReference = component,
                                        ReleaseReference = Release,
                                        BuildReference = Build,
                                        Status = "NO_RESULT",
                                        Hostname = Environment.MachineName,
                                        RunStatus = "TO_BE_RUN",
                                    };
                result.Post();
                Results.Add(result);
            }
            
        }

        public void UpdateResult(TestResult result)
        {
            // TODO: Detect and handle out of range results
            var slickResult = Results[TestCount++];
            // TODO: Check DisplayName to make sure it matches the test name
            slickResult.Recorded = DateTime.Now.ToUnixTime();
            slickResult.RunStatus = "FINISHED";
            slickResult.Status = result.Outcome.ConvertToSlickResultStatus();
            if(!String.IsNullOrWhiteSpace(result.ErrorMessage))
                slickResult.Reason = String.Format("ERROR: {0}\r\n{1}", result.ErrorMessage, result.ErrorStackTrace);
            slickResult.RunLength = result.Duration.TotalMilliseconds.ToString("F0"); // from http://msdn.microsoft.com/en-us/library/kfsatb94.aspx
            slickResult.Put();

            // TODO: Handle additional files
            var posted = new List<Uri>();
            slickResult.Files = new List<StoredFile>();
            foreach (var attachment in result.Attachments)
            {
                foreach (var file in attachment.Attachments)
                {
                    if (!posted.Contains(file.Uri))
                    {
                        var filepath = file.Uri.LocalPath;
                        var filebytes = File.ReadAllBytes(filepath);
                        var slickFile = new StoredFile()
                                            {
                                                FileName = Path.GetFileName(filepath),
                                                Mimetype = MIMEAssistant.GetMIMEType(filepath),
                                                Length = filebytes.Length
                                            };
                        slickFile.Post();
                        slickFile.PostContent(filebytes);
                        slickResult.Files.Add(slickFile);
                        if(Path.GetFileName(filepath).Equals("testlog.xml", StringComparison.OrdinalIgnoreCase))
                        {
                            if(slickResult.Log == null)
                                slickResult.Log = new List<LogEntry>();
                            slickResult.Log.AddRange(LocalLogEntry.ReadFromFile(filepath));
                        }
                    }
                }
            }
            slickResult.Put();
            // TODO: Handle logs
        }

        private void addToResultLog(Result result, String message, params object[] parms)
        {
            var logentries = new List<LogEntry>();
            logentries.Add(new LogEntry()
                               {
                                   EntryTime = (DateTime.Now.ToString(new CultureInfo( "en-US", false ).DateTimeFormat.RFC1123Pattern)),
                                   Level = "INFO",
                                   LoggerName = "SlickReporter",
                                   Message = String.Format(message, parms)
                               });
            result.AddToLog(logentries);
        }

        public void AllDone()
        {
            // Not sure if we need this now, but just in case
        }

        private void InitializeTestrun()
        {
            Testrun = new TestRun()
                          {
                              ProjectReference = Project,
                              ReleaseReference = Release,
                              BuildReference = Build,
                              TestPlanId = Testplan.Id,
                              Name = Testplan.Name
                          };
            Testrun.Post();
        }

        private void InitializeTestplan()
        {
            Testplan = new TestPlan()
                           {
                               ProjectReference = Project,
                               Name = SlickRunInfo.TestPlan.Name,
                               Id = SlickRunInfo.TestPlan.Id
                           };
            Testplan.Get(CreateIfNecessaryMode, 3);
        }

        private void InitializeBuild()
        {
            // TODO: Get build info from static method
            Build = new Build() {ProjectId = Project.Id, Id = Release.DefaultBuildId};
        }

        private void InitializeRelease()
        {
            Release = new Release()
                          {
                              ProjectId = SlickRunInfo.Release.ProjectId,
                              Id = SlickRunInfo.Release.Id,
                              Name = SlickRunInfo.Release.Name
                          };
            Release.Get(CreateIfNecessaryMode, 3);
        }

        private void InitializeProject()
        {
            Project = new Project {Id = SlickRunInfo.Project.Id, Name = SlickRunInfo.Project.Name};
            Project.Get(CreateIfNecessaryMode, 3);
            if(Project.Components != null && Project.Components.Count > 0)
            {
                foreach(var component in Project.Components)
                {
                    ComponentCache.Add(component.Name, component);
                }
            }
        }

    }

    public static class ResultStatusConverter
    {
        public static string ConvertToSlickResultStatus(this TestOutcome outcome)
        {
            switch (outcome)
            {
                case TestOutcome.Passed:
                    return "PASS";
                case TestOutcome.Failed:
                    return "FAIL";
                case TestOutcome.Skipped:
                    return "SKIPPED";
                case TestOutcome.NotFound:
                case TestOutcome.None:
                default:
                    return "NOT_TESTED";
            }
        }
    }

    public static class UnixDateConverter
    {
        public static long ToUnixTime(this DateTime time)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((time.ToUniversalTime() - epoch).TotalMilliseconds);
        }
    }

    public static class MIMEAssistant
    {
        private static readonly Dictionary<string, string> MIMETypesDictionary = new Dictionary<string, string>
        {
            {"ai", "application/postscript"},
            {"aif", "audio/x-aiff"},
            {"aifc", "audio/x-aiff"},
            {"aiff", "audio/x-aiff"},
            {"asc", "text/plain"},
            {"atom", "application/atom+xml"},
            {"au", "audio/basic"},
            {"avi", "video/x-msvideo"},
            {"bcpio", "application/x-bcpio"},
            {"bin", "application/octet-stream"},
            {"bmp", "image/bmp"},
            {"cdf", "application/x-netcdf"},
            {"cgm", "image/cgm"},
            {"class", "application/octet-stream"},
            {"cpio", "application/x-cpio"},
            {"cpt", "application/mac-compactpro"},
            {"csh", "application/x-csh"},
            {"css", "text/css"},
            {"dcr", "application/x-director"},
            {"dif", "video/x-dv"},
            {"dir", "application/x-director"},
            {"djv", "image/vnd.djvu"},
            {"djvu", "image/vnd.djvu"},
            {"dll", "application/octet-stream"},
            {"dmg", "application/octet-stream"},
            {"dms", "application/octet-stream"},
            {"doc", "application/msword"},
            {"docx","application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
            {"dotx", "application/vnd.openxmlformats-officedocument.wordprocessingml.template"},
            {"docm","application/vnd.ms-word.document.macroEnabled.12"},
            {"dotm","application/vnd.ms-word.template.macroEnabled.12"},
            {"dtd", "application/xml-dtd"},
            {"dv", "video/x-dv"},
            {"dvi", "application/x-dvi"},
            {"dxr", "application/x-director"},
            {"eps", "application/postscript"},
            {"etx", "text/x-setext"},
            {"exe", "application/octet-stream"},
            {"ez", "application/andrew-inset"},
            {"gif", "image/gif"},
            {"gram", "application/srgs"},
            {"grxml", "application/srgs+xml"},
            {"gtar", "application/x-gtar"},
            {"hdf", "application/x-hdf"},
            {"hqx", "application/mac-binhex40"},
            {"htm", "text/html"},
            {"html", "text/html"},
            {"ice", "x-conference/x-cooltalk"},
            {"ico", "image/x-icon"},
            {"ics", "text/calendar"},
            {"ief", "image/ief"},
            {"ifb", "text/calendar"},
            {"iges", "model/iges"},
            {"igs", "model/iges"},
            {"jnlp", "application/x-java-jnlp-file"},
            {"jp2", "image/jp2"},
            {"jpe", "image/jpeg"},
            {"jpeg", "image/jpeg"},
            {"jpg", "image/jpeg"},
            {"js", "application/x-javascript"},
            {"kar", "audio/midi"},
            {"latex", "application/x-latex"},
            {"lha", "application/octet-stream"},
            {"lzh", "application/octet-stream"},
            {"log", "text/plain"},
            {"m3u", "audio/x-mpegurl"},
            {"m4a", "audio/mp4a-latm"},
            {"m4b", "audio/mp4a-latm"},
            {"m4p", "audio/mp4a-latm"},
            {"m4u", "video/vnd.mpegurl"},
            {"m4v", "video/x-m4v"},
            {"mac", "image/x-macpaint"},
            {"man", "application/x-troff-man"},
            {"mathml", "application/mathml+xml"},
            {"me", "application/x-troff-me"},
            {"mesh", "model/mesh"},
            {"mid", "audio/midi"},
            {"midi", "audio/midi"},
            {"mif", "application/vnd.mif"},
            {"mov", "video/quicktime"},
            {"movie", "video/x-sgi-movie"},
            {"mp2", "audio/mpeg"},
            {"mp3", "audio/mpeg"},
            {"mp4", "video/mp4"},
            {"mpe", "video/mpeg"},
            {"mpeg", "video/mpeg"},
            {"mpg", "video/mpeg"},
            {"mpga", "audio/mpeg"},
            {"ms", "application/x-troff-ms"},
            {"msh", "model/mesh"},
            {"mxu", "video/vnd.mpegurl"},
            {"nc", "application/x-netcdf"},
            {"oda", "application/oda"},
            {"ogg", "application/ogg"},
            {"pbm", "image/x-portable-bitmap"},
            {"pct", "image/pict"},
            {"pdb", "chemical/x-pdb"},
            {"pdf", "application/pdf"},
            {"pgm", "image/x-portable-graymap"},
            {"pgn", "application/x-chess-pgn"},
            {"pic", "image/pict"},
            {"pict", "image/pict"},
            {"png", "image/png"}, 
            {"pnm", "image/x-portable-anymap"},
            {"pnt", "image/x-macpaint"},
            {"pntg", "image/x-macpaint"},
            {"ppm", "image/x-portable-pixmap"},
            {"ppt", "application/vnd.ms-powerpoint"},
            {"pptx","application/vnd.openxmlformats-officedocument.presentationml.presentation"},
            {"potx","application/vnd.openxmlformats-officedocument.presentationml.template"},
            {"ppsx","application/vnd.openxmlformats-officedocument.presentationml.slideshow"},
            {"ppam","application/vnd.ms-powerpoint.addin.macroEnabled.12"},
            {"pptm","application/vnd.ms-powerpoint.presentation.macroEnabled.12"},
            {"potm","application/vnd.ms-powerpoint.template.macroEnabled.12"},
            {"ppsm","application/vnd.ms-powerpoint.slideshow.macroEnabled.12"},
            {"ps", "application/postscript"},
            {"qt", "video/quicktime"},
            {"qti", "image/x-quicktime"},
            {"qtif", "image/x-quicktime"},
            {"ra", "audio/x-pn-realaudio"},
            {"ram", "audio/x-pn-realaudio"},
            {"ras", "image/x-cmu-raster"},
            {"rdf", "application/rdf+xml"},
            {"rgb", "image/x-rgb"},
            {"rm", "application/vnd.rn-realmedia"},
            {"roff", "application/x-troff"},
            {"rtf", "text/rtf"},
            {"rtx", "text/richtext"},
            {"sgm", "text/sgml"},
            {"sgml", "text/sgml"},
            {"sh", "application/x-sh"},
            {"shar", "application/x-shar"},
            {"silo", "model/mesh"},
            {"sit", "application/x-stuffit"},
            {"skd", "application/x-koan"},
            {"skm", "application/x-koan"},
            {"skp", "application/x-koan"},
            {"skt", "application/x-koan"},
            {"smi", "application/smil"},
            {"smil", "application/smil"},
            {"snd", "audio/basic"},
            {"so", "application/octet-stream"},
            {"spl", "application/x-futuresplash"},
            {"src", "application/x-wais-source"},
            {"sv4cpio", "application/x-sv4cpio"},
            {"sv4crc", "application/x-sv4crc"},
            {"svg", "image/svg+xml"},
            {"swf", "application/x-shockwave-flash"},
            {"t", "application/x-troff"},
            {"tar", "application/x-tar"},
            {"tcl", "application/x-tcl"},
            {"tex", "application/x-tex"},
            {"texi", "application/x-texinfo"},
            {"texinfo", "application/x-texinfo"},
            {"tif", "image/tiff"},
            {"tiff", "image/tiff"},
            {"tr", "application/x-troff"},
            {"tsv", "text/tab-separated-values"},
            {"txt", "text/plain"},
            {"ustar", "application/x-ustar"},
            {"vcd", "application/x-cdlink"},
            {"vrml", "model/vrml"},
            {"vxml", "application/voicexml+xml"},
            {"wav", "audio/x-wav"},
            {"wbmp", "image/vnd.wap.wbmp"},
            {"wbmxl", "application/vnd.wap.wbxml"},
            {"wml", "text/vnd.wap.wml"},
            {"wmlc", "application/vnd.wap.wmlc"},
            {"wmls", "text/vnd.wap.wmlscript"},
            {"wmlsc", "application/vnd.wap.wmlscriptc"},
            {"wmv", "video/x-ms-wmv"},
            {"wrl", "model/vrml"},
            {"xbm", "image/x-xbitmap"},
            {"xht", "application/xhtml+xml"},
            {"xhtml", "application/xhtml+xml"},
            {"xls", "application/vnd.ms-excel"},                        
            {"xml", "application/xml"},
            {"xpm", "image/x-xpixmap"},
            {"xsl", "application/xml"},
            {"xlsx","application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
            {"xltx","application/vnd.openxmlformats-officedocument.spreadsheetml.template"},
            {"xlsm","application/vnd.ms-excel.sheet.macroEnabled.12"},
            {"xltm","application/vnd.ms-excel.template.macroEnabled.12"},
            {"xlam","application/vnd.ms-excel.addin.macroEnabled.12"},
            {"xlsb","application/vnd.ms-excel.sheet.binary.macroEnabled.12"},
            {"xslt", "application/xslt+xml"},
            {"xul", "application/vnd.mozilla.xul+xml"},
            {"xwd", "image/x-xwindowdump"},
            {"xyz", "chemical/x-xyz"},
            {"zip", "application/zip"}
        };

        public static string GetMIMEType(string fileName)
        {
            if (MIMETypesDictionary.ContainsKey(Path.GetExtension(fileName).Remove(0, 1)))
            {
                return MIMETypesDictionary[Path.GetExtension(fileName).Remove(0, 1)];
            }
            return "application/octet-stream";
        }
    }
}
