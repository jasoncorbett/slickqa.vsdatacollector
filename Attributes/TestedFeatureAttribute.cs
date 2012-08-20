using System;

namespace SlickQA.DataCollector.Attributes
{
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class TestedFeatureAttribute : Attribute, IStringValueAttribute
	{
		public TestedFeatureAttribute(string feature)
		{
			Value = feature;
		}

		public string Value { get; private set; }
	}
}