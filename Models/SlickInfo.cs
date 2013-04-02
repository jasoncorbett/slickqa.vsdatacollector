using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using SlickQA.SlickSharp;

namespace SlickQA.DataCollector.Models
{
	public sealed class SlickInfo
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Component { get; set; }
		public List<string> Tags { get; set; }
		public LinkedHashMap<string> Attributes { get; set; }
		public string AutomationKey { get; set; }
        public string Author { get; set; }
        public bool IsOrderedTest { get; set; }
        public bool DoNotReport { get; set; }
        public bool SlickTLTest { get; set; }
        public List<SlickInfo> OrderedTestCases { get; set; }

        [XmlIgnore]
        public Result SlickResult { get; set; }

        public IEnumerator<SlickInfo> GetEnumerator()
        {
            return new SlickInfoEnumerator(this);
        }
	}

    public class SlickInfoEnumerator : IEnumerator<SlickInfo>
    {
        private Dictionary<SlickInfo, int> Positions { get; set; }
        private SlickInfo CurrentOrderedTest { get; set; }
        private Stack<SlickInfo> Parents { get; set; }

        public SlickInfoEnumerator(SlickInfo orig)
        {
            Positions = new Dictionary<SlickInfo, int>();
            Parents = new Stack<SlickInfo>();
            Positions.Add(orig, -2);
            CurrentOrderedTest = orig;
            Current = orig;
        }

        public void Dispose()
        {
            // no worries
        }

        public bool MoveNext()
        {
            if (CurrentOrderedTest.OrderedTestCases == null)
                return false;
            if (CurrentOrderedTest.OrderedTestCases.Count == (Positions[CurrentOrderedTest] + 1) &&
                !(Current.IsOrderedTest && !Positions.ContainsKey(Current)))
            {
                // we've hit the end of this ordered test
                if (Parents.Count == 0)
                    return false;
                // We have a parent available
                while (Parents.Count > 0)
                {
                    CurrentOrderedTest = Parents.Pop();

                    // if we have more tests in the Current (now that we've gone up one level) break out of our loop
                    if (CurrentOrderedTest.OrderedTestCases.Count > (Positions[CurrentOrderedTest] + 1))
                        break;
                }

                // if we've gone up all the parents, and we are at the end, we're done.
                if (CurrentOrderedTest.OrderedTestCases.Count == (Positions[CurrentOrderedTest] + 1))
                    return false;
            }

            if (Current.IsOrderedTest && Current.OrderedTestCases != null && Current.OrderedTestCases.Count > 0 && !Positions.ContainsKey(Current))
            {
                Parents.Push(CurrentOrderedTest);
                CurrentOrderedTest = Current;
                Positions[CurrentOrderedTest] = 0;
            }
            else
            {
                // at this point we have somewhere to go
                Positions[CurrentOrderedTest] += 1;
            }
            if(Positions[CurrentOrderedTest] >= 0)
                Current = CurrentOrderedTest.OrderedTestCases[Positions[CurrentOrderedTest]];

            return true;
        }

        public void Reset()
        {
            while (Parents.Count > 0)
            {
                CurrentOrderedTest = Parents.Pop();
            }

            Positions = new Dictionary<SlickInfo, int>();
            Positions[CurrentOrderedTest] = -2;
            Current = CurrentOrderedTest;
        }

        public SlickInfo Current { get; set; }

        object IEnumerator.Current
        {
            get { return Current; }
        }
    }
}