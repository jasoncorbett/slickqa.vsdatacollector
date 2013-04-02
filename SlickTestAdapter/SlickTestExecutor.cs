using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestPlatform.Extensions.OrderedTestAdapter;
using Microsoft.VisualStudio.TestPlatform.Extensions.TmiHelper;
using Microsoft.VisualStudio.TestPlatform.Extensions.VSTestIntegration;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlickQA.DataCollector.Models;
using SlickQA.SlickTL;

namespace SlickQA.TestAdapter
{
	[ExtensionUri(Constants.EXECUTOR_URI_STRING)]
	public class SlickTestExecutor : ITestExecutor
	{
	    private IFrameworkHandle _frameworkHandle;

		public void RunTests(IEnumerable<TestCase> tests, IRunContext runContext, IFrameworkHandle frameworkHandle)
		{
			ValidateArg.NotNull(frameworkHandle, "frameworkHandle");
			ValidateArg.NotNullOrEmpty(tests, "tests");
		    _frameworkHandle = frameworkHandle;


			using (var bridge = new TmiBridge())
			{
			    foreach (var test in tests)
			    {
                    log("Running '{0}'", test.Source);
			        var slickExecutionRecorder = new SlickExecutionRecorder(frameworkHandle, LoadSlickTest(test.Source));
			        _cancellationToken = new TestRunCancellationToken();
			        foreach (var orderedTest in slickExecutionRecorder.SlickInfo.OrderedTests)
			        {
			            bridge.RunAllTests(new string[] {orderedTest}, runContext, slickExecutionRecorder, new Uri(OrderedTestExecutor.ExecutorUriString), _cancellationToken );
			        }
                    slickExecutionRecorder.AllDone();
			        _cancellationToken = null;
			    }
			}

            /*
            foreach (var result in slickExecutionRecorder.Results)
            {
                log("Result for test {0}: {1}", result.DisplayName, result.Outcome);

                foreach(var attachgroup in result.Attachments)
                {
                    log(" {0}:", attachgroup.DisplayName);
                    foreach (var attachment in attachgroup.Attachments)
                    {
                        log("  {0}: {1}", attachment.Description, attachment.Uri);
                    }
                }

                foreach (var property in result.Properties)
                {
                    log(" {0}: {1}", property.Label, result.GetPropertyValue(property));
                }

                log(" {0} test properties:", result.TestCase.DisplayName);
                foreach (var property in result.TestCase.Properties)
                {
                    if(!String.IsNullOrEmpty(result.TestCase.GetPropertyValue(property).ToString()))
                        log("  {0}: {1}", property.Label, result.TestCase.GetPropertyValue(property));
                }
            }
             */
		}

        public void log(string format, params object[] items)
        {
            if (format == null && items != null && items.Length > 0)
            {
                _frameworkHandle.SendMessage(TestMessageLevel.Informational, items[0].ToString());
            } else if (items == null || items.Length == 0)
            {
                _frameworkHandle.SendMessage(TestMessageLevel.Informational, format);
            }
            else if(format != null && items.Length > 0)
            {
                _frameworkHandle.SendMessage(TestMessageLevel.Informational, String.Format(format, items));
            }
        }

		public void RunTests(IEnumerable<string> sources, IRunContext runContext, IFrameworkHandle frameworkHandle)
		{
			ValidateArg.NotNull(frameworkHandle, "frameworkHandle");
			ValidateArg.NotNullOrEmpty(sources, "sources");
		    _frameworkHandle = frameworkHandle;

			using (var bridge = new TmiBridge())
			{
			    foreach (var testSource in sources)
			    {
			        var slickExecutionRecorder = new SlickExecutionRecorder(frameworkHandle, LoadSlickTest(testSource));
			        _cancellationToken = new TestRunCancellationToken();
			        foreach (var orderedTest in slickExecutionRecorder.SlickInfo.OrderedTests)
			        {
			            bridge.RunAllTests(new string[] {orderedTest}, runContext, slickExecutionRecorder, new Uri(OrderedTestExecutor.ExecutorUriString), _cancellationToken);
			        }
                    slickExecutionRecorder.AllDone();
		            _cancellationToken = null;
			    }
			}
		}

        public SlickTest LoadSlickTest(string source)
        {
            System.Threading.Thread.Sleep(5000);
            SlickTest retval = null;


            try
            {
                var serializer = new XmlSerializer(typeof (SlickTest));
                using (var filestream = new FileStream(source, FileMode.Open))
                {
                    retval = (SlickTest) serializer.Deserialize(filestream);
                }
                if ((retval.OrderedTests == null || retval.OrderedTests.Count == 0) && retval.OrderedTest != null)
                {
                    retval.OrderedTests = new List<string>();
                    retval.OrderedTests.Add(retval.OrderedTest);
                }
                log("There are {0} ordered tests.", retval.OrderedTests.Count);
                for (int i = 0; i < retval.OrderedTests.Count; i++)
                {
                    retval.OrderedTests[i] = Path.Combine(Path.GetDirectoryName(source), retval.OrderedTests[i]);
                }
                if (retval.ReleaseProvider != null)
                    retval.ReleaseProvider.RelativeRoot = Path.GetDirectoryName(source);
                if (retval.BuildProvider != null)
                    retval.BuildProvider.RelativeRoot = Path.GetDirectoryName(source);
                if (retval.BuildDescriptionProvider != null)
                    retval.BuildDescriptionProvider.RelativeRoot = Path.GetDirectoryName(source);
                
                retval.Tests = new SlickInfo()
                                   {
                                       IsOrderedTest = true,
                                       OrderedTestCases = new List<SlickInfo>(),
                                       Name = "Wrapper"
                                   };
                foreach (var orderedTest in retval.OrderedTests)
                {
                    log("Parsing Ordered Test {0}...", orderedTest);
                    var test = ParseOrderedTestXmlToSlickInfoList(orderedTest);
                    log("Parsed Ordered Test {0} and found {1} tests.", orderedTest, test.OrderedTestCases.Count);
                    retval.Tests.OrderedTestCases.Add(test);
                }
            }
                catch(ReflectionTypeLoadException e)
                {
                    foreach(var ex in e.LoaderExceptions)
                    {
                        log("Loader Exception: {0}", ex.GetType().FullName);
                        log("Loader Exception Message: {0}", ex.Message);
                        if (ex.GetType() == typeof (System.IO.FileNotFoundException))
                        {
                            log("File it couldn't find: {0}", ((FileNotFoundException)ex).FileName);
                        }
                        log(ex.StackTrace ?? e.StackTrace ?? "No Stack Trace Found!");
                    }
                }
            catch (Exception e)
            {
                log("Unable to load slicktest '{0}': {1}", source, e.Message);
                log(e.StackTrace);
                log("Exception Class Name: {0}", e.GetType().FullName);
                throw;
            }

            return retval;
        }

        private SlickInfo ParseOrderedTestXmlToSlickInfoList(string orderedTestPath)
        {
            var retval = new SlickInfo()
                         {
                             Name = Path.GetFileName(orderedTestPath),
                             IsOrderedTest = true,
                             OrderedTestCases = new List<SlickInfo>(),
                         };
            var orderedTestDir = Path.GetDirectoryName(orderedTestPath);

            // Read XML
            string xml;

            using (var reader = new StreamReader(File.Open(orderedTestPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
            {
                xml = reader.ReadToEnd();
            }


            XNamespace vs = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010";
            XDocument doc = XDocument.Parse(xml);

            var testlinks = doc.Descendants(vs + "TestLink");
            foreach (var testlink in testlinks)
            {
                var id = Guid.Parse(testlink.Attribute("id").Value);
                var storagePath = testlink.Attribute("storage").Value;

                Debug.Assert(orderedTestDir != null, "orderedTestDir != null");
                var fullPath = Path.Combine(orderedTestDir, storagePath);

                if (storagePath.Contains(".orderedtest"))
                {
                    retval.OrderedTestCases.Add(ParseOrderedTestXmlToSlickInfoList(fullPath));
                }
                else
                {
                    var dll = Assembly.LoadFrom(fullPath);
                    // Find all classes that are test classes
                    foreach (var type in dll.GetTypes())
                    {
                        var extensionAttrs = type.GetCustomAttributes(typeof (TestClassExtensionAttribute), true);
                        var testclassAttrs = type.GetCustomAttributes(typeof (TestClassAttribute), true);

                        bool found = false;
                        if (extensionAttrs.Length != 0 || testclassAttrs.Length != 0)
                        {
                            // Find all methods with TestMethod attribute
                            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);
                            foreach (var method in methods)
                            {
                                if (method.GetCustomAttributes(typeof (TestMethodAttribute), true).Length != 0)
                                {
                                    if (method.GetHash() == id)
                                    {
                                        retval.OrderedTestCases.Add(new SlickInfo
                                                       {
                                                           Id = method.GetTestCaseId(),
                                                           AutomationKey = method.GetAutomationKey(),
                                                           Name = method.GetTestName(),
                                                           Description = method.GetDescription(),
                                                           Component = method.GetComponent(),
                                                           Tags = method.GetTags(),
                                                           Attributes = method.GetAttributes(),
                                                           Author = method.GetAuthor(),
                                                           DoNotReport = method.IsDoNotReport(),
                                                           SlickTLTest = typeof(SlickBaseTest).IsAssignableFrom(type) || typeof(CodedUIScreenshotBaseTest).IsAssignableFrom(type),
                                                       });
                                        found = true;
                                        break;
                                    }
                                }
                            }
                        }
                        if (found)
                        {
                            break;
                        }
                    }
                }

            }
            return retval;
        }


		public void Cancel()
		{
			TestRunCancellationToken cancellationToken = _cancellationToken;
			if (cancellationToken != null)
			{
				cancellationToken.Cancel();
			}
		}

		private TestRunCancellationToken _cancellationToken;
	}
}