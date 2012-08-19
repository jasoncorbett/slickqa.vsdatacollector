using System.Xml;

namespace SlickQA.DataCollector.ConfigurationEditor
{
	static internal class ConfigExtensions
	{
		public static void UpdateTagWithNewValue(this XmlElement config, string tagName, XmlNode newNode)
		{
			var elements = config.GetElementsByTagName(tagName);
			if (elements.Count != 0)
			{
				config.ReplaceChild(newNode, elements[0]);
			}
			else
			{
				config.AppendChild(newNode);
			}
		}
	}
}