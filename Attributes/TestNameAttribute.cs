using System;

namespace SlickQA.DataCollector.Attributes
{
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class TestNameAttribute : Attribute, IStringValueAttribute
	{
		public TestNameAttribute(string testName)
		{
			Value = testName;
		}

		public string Value { get; private set; }
	}
}
