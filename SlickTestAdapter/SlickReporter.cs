using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using SlickQA.DataCollector.Models;
using SlickQA.SlickSharp;
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
                            Name = testinfo.Name
                        };
                    test.Post();
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
                                        RunStatus = "TO_BE_RUN"
                                    };
                result.Post();
                Results.Add(result);
            }
            
        }

        public void UpdateResult(TestResult result)
        {
            
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
}
