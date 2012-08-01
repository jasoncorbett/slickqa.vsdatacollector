// Copyright 2012 AccessData Group, LLC.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//  http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Diagnostics;
using System.Globalization;
using System.Xml;

namespace SlickQA.DataCollector.Configuration
{
	public sealed class SlickUrlType
	{
		public readonly string Host;
		public readonly int Port;
		public readonly string Scheme;
		public readonly string SitePath;

		public SlickUrlType() : this("http", String.Empty, 8080, "slick")
		{
		}

		public SlickUrlType(string scheme, string host, int port, string sitePath)
		{
			Scheme = scheme;
			Host = host;
			Port = port;
			SitePath = sitePath;
		}

		public SlickUrlType(XmlNode urlElem)
		{
			if (urlElem.Attributes == null)
			{
				return;
			}
			XmlAttribute schemeAttr = urlElem.Attributes["Scheme"];
			XmlAttribute hostAttr = urlElem.Attributes["Host"];
			XmlAttribute portAttr = urlElem.Attributes["Port"];
			XmlAttribute sitePathAttr = urlElem.Attributes["SitePath"];

			Scheme = schemeAttr.Value;
			Host = hostAttr.Value;
			Port = Convert.ToInt32(portAttr.Value);
			SitePath = sitePathAttr.Value;
		}


		public override string ToString()
		{
			return String.Format("{0}://{1}:{2}/{3}", Scheme, Host, Port, SitePath);
		}

		public override int GetHashCode()
		{
			return Scheme.GetHashCode() + Host.GetHashCode() + Port.GetHashCode() + SitePath.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			var other = obj as SlickUrlType;
			if (other == null)
			{
				return false;
			}

			return Scheme.Equals(other.Scheme) && Host.Equals(other.Host) && Port.Equals(other.Port)
			       && SitePath.Equals(other.SitePath);
		}

		public XmlNode ToXml(XmlDocument doc)
		{
			XmlNode node = doc.CreateNode(XmlNodeType.Element, "Url", String.Empty);
			XmlAttributeCollection attrCol = node.Attributes;

			XmlAttribute nameAttr = doc.CreateAttribute("Scheme");
			nameAttr.Value = Scheme;

			XmlAttribute hostAttr = doc.CreateAttribute("Host");
			hostAttr.Value = Host;

			XmlAttribute portAttr = doc.CreateAttribute("Port");
			portAttr.Value = Port.ToString(CultureInfo.InvariantCulture);

			XmlAttribute sitePathAttr = doc.CreateAttribute("SitePath");
			sitePathAttr.Value = SitePath;


			Debug.Assert(attrCol != null, "attrCol != null");
			attrCol.Append(nameAttr);
			attrCol.Append(hostAttr);
			attrCol.Append(portAttr);
			attrCol.Append(sitePathAttr);

			return node;
		}
	}
}