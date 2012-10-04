using System.Collections.Generic;
using SlickQA.SlickSharp;
using SlickQA.SlickSharp.Utility;

namespace SlickQA.DataCollector
{
	internal sealed class SlickInfo
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Component { get; set; }
		public List<string> Tags { get; set; }
		public LinkedHashMap<string> Attributes { get; set; }
		public string AutomationKey { get; set; }
	}
}