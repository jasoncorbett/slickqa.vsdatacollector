using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SlickQA.DataCollector.Models
{
    [XmlRoot(TagName)]
    public class SlickTest
    {
        public const string TagName = "SlickTest";


        public UrlInfo Url { get; set; }
        public ProjectInfo Project { get; set; }
        public ReleaseInfo Release { get; set; }
        public TestPlanInfo TestPlan { get; set; }
        public BuildProviderInfo BuildProvider { get; set; }
        public String OrderedTest { get; set; }
    }
}
