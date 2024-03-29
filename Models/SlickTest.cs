﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace SlickQA.DataCollector.Models
{
    [XmlRoot(TagName)]
    public class SlickTest
    {
        public const string TagName = "SlickTest";


        public UrlInfo Url { get; set; }
        public ProjectInfo Project { get; set; }
        public String Release { get; set; }
        public BuildProviderInfo ReleaseProvider { get; set; }
        public TestPlanInfo TestPlan { get; set; }
        public String Build { get; set; }
        public BuildProviderInfo BuildProvider { get; set; }
        public BuildProviderInfo BuildDescriptionProvider { get; set; }
        [XmlArray]
        [XmlArrayItem(ElementName = "OrderedTest")]
        public List<String> OrderedTests { get; set; }

        public String OrderedTest { get; set; }
        public String Environment { get; set; }

        [DefaultValue(false)]
        public Boolean UseMsTestDuration { get; set; }

        [XmlIgnore]
        public SlickInfo Tests { get; set; }
    }
}
