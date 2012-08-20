using System;

namespace SlickQA.DataCollector.Attributes
{
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class TestCaseIdAttribute : Attribute, IStringValueAttribute
	{
		public TestCaseIdAttribute(string testName)
		{
			Value = testName;
		}

		public string Value { get; private set; }
	}
}