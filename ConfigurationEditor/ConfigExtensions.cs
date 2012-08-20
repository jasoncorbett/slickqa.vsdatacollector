using System.Diagnostics;
using System.Xml;

namespace SlickQA.DataCollector.ConfigurationEditor
{
	static internal class ConfigExtensions
	{
		public static void UpdateTagWithNewValue(this XmlElement config, string tagName, XmlNode newNode)
		{
			Debug.Assert(config.OwnerDocument != null, "config.OwnerDocument != null");
			var node = config.OwnerDocument.ImportNode(newNode, true);
			
			var elements = config.GetElementsByTagName(tagName);
			if (elements.Count != 0)
			{
				config.ReplaceChild(node, elements[0]);
			}
			else
			{
				config.AppendChild(node);
			}
		}
	}
}