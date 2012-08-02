using System;
using System.Xml;

namespace SlickQA.DataCollector.Configuration
{
	public class ScreenShotSettings
	{
		public readonly bool PreTest;
		public readonly bool PostTest;
		public readonly bool OnFailure;

		public ScreenShotSettings(XmlNode screenshotElem)
		{
			var attrs = screenshotElem.Attributes;
			if (attrs == null)
			{
				return;
			}
			PreTest = Convert.ToBoolean(attrs["PreTest"].Value);
			PostTest = Convert.ToBoolean(attrs["PostTest"].Value);
			OnFailure = Convert.ToBoolean(attrs["OnFailure"].Value);
		}

		public ScreenShotSettings()
			:this(false, false, true)
		{
		}

		public ScreenShotSettings(bool preTest, bool postTest, bool onFailure)
		{
			PreTest = preTest;
			PostTest = postTest;
			OnFailure = onFailure;
		}

		public override int GetHashCode()
		{
			return PreTest.GetHashCode() + PostTest.GetHashCode() + OnFailure.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			var other = obj as ScreenShotSettings;
			if (other == null)
			{
				return false;
			}

			return PreTest == other.PreTest && PostTest == other.PostTest && OnFailure == other.OnFailure;
		}

		public override string ToString()
		{
			return string.Format("PreTest? {0} PostTest? {1} OnFailure? {2}", PreTest, PostTest, OnFailure);
		}
	}
}