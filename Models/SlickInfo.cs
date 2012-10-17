using System.Collections.Generic;

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
	}
}