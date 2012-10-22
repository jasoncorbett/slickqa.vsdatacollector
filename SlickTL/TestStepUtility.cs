using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SlickQA.SlickTL
{
    public class TestStep
    {
        public string StepName { get; set; }
        public string ExpectedResult { get; set; }
    }

    public interface ITestStepUtility
    {
        void AddStep(string step, string expectedResult = "");
    }

    [Export(typeof(ITestStepUtility))]
    [Export(typeof(IFrameworkInitializePart))]
    class XmlTestStepUtility :ITestStepUtility, IFrameworkInitializePart
    {
        [Import]
        public DirectoryManager Directories { get; set; }

        public TestContext TestContext { get; set; }

        public string Name
        {
            get { return "XmlTestStepUtility"; }
        }

        public void AddStep(string step, string expectedResult = "")
        {
            var stepFilePath = Path.Combine(Directories.CurrentTestOutputDirectory, "steps.xml");
            if (! File.Exists(stepFilePath))
            {
                var steplist = new List<TestStep>(1);
                steplist.Add(new TestStep() {StepName = step, ExpectedResult = expectedResult});
                var serializer = new XmlSerializer(typeof (List<TestStep>), new XmlRootAttribute("Steps"));
                using (var stepFileStream = new FileStream(stepFilePath, FileMode.CreateNew))
                {
                    serializer.Serialize(stepFileStream, steplist);
                }
                TestContext.AddResultFile(stepFilePath);
            }
            else
            {
                var serializer = new XmlSerializer(typeof (List<TestStep>), new XmlRootAttribute("Steps"));
                List<TestStep> stepList = null;
                using (var stepFileStream = new FileStream(stepFilePath, FileMode.Open))
                {
                    stepList = (List<TestStep>) serializer.Deserialize(stepFileStream);
                }
                if(stepList == null)
                    stepList = new List<TestStep>();
                stepList.Add(new TestStep() {StepName = step, ExpectedResult = expectedResult});
                using (var stepFileStream = new FileStream(stepFilePath, FileMode.Truncate))
                {
                    serializer.Serialize(stepFileStream, stepList);
                }
            }
        }

        public void initialize(object instance, TestContext context)
        {
            TestContext = context;
        }
    }
}
