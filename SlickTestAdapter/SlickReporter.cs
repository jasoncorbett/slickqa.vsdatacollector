using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using SlickQA.DataCollector.Models;
using SlickQA.SlickSharp;
using SlickQA.SlickSharp.ObjectReferences;
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


        public SlickReporter(SlickTest slickInfo, bool createIfNecessary = true)
        {
            Results = new List<Result>();
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
        }
    }
}
