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
using System.Xml;
using System.Xml.Serialization;

namespace SlickQA.DataCollector.Models
{
	public sealed class UrlInfo
	{
		public const string TAG_NAME = "Url";

		public string Scheme { get; set; }
		public string HostName { get; set; }
		public int Port { get; set; }
		public string SitePath { get; set; }

		public UrlInfo()
		{
			InitializeWithDefaults();
		}

		private UrlInfo(XmlNodeList elements)
		{
			try
			{
				var element = elements[0];
				if (element != null)
				{
					var reader = new XmlNodeReader(element);
					var s = new XmlSerializer(GetType());
					var temp = s.Deserialize(reader) as UrlInfo;
					Scheme = temp.Scheme;
					HostName = temp.HostName;
					Port = temp.Port;
					SitePath = temp.SitePath;					
				}
				else
				{
					InitializeWithDefaults();
				}
			}
			catch (InvalidOperationException)
			{
				InitializeWithDefaults();
			}
		}

		public UrlInfo(UrlInfo other)
			:this(other.Scheme, other.HostName, other.Port, other.SitePath)
		{
		}

		private UrlInfo(string scheme, string hostName, int port, string sitePath)
		{
			Scheme = scheme;
			HostName = hostName;
			Port = port;
			SitePath = sitePath;
		}

		public string DisplayName
		{
			get { return String.Format("{0}://{1}:{2}/{3}", Scheme, HostName, Port, !String.IsNullOrWhiteSpace(SitePath) ? SitePath + "/" : String.Empty); }
		}

		public bool IsComplete
		{
			get
			{
				if (!String.IsNullOrWhiteSpace(HostName))
				{
					if (Uri.CheckSchemeName(Scheme) && (Uri.CheckHostName(HostName) != UriHostNameType.Unknown))
					{
						return true;
					}
				}
				return false;
			}
		}

		private void InitializeWithDefaults()
		{
			Scheme = Uri.UriSchemeHttp;
			HostName = string.Empty;
			Port = 8080;
			SitePath = string.Empty;
		}

		public static UrlInfo FromXml(XmlElement configuration)
		{
			return new UrlInfo(configuration.GetElementsByTagName(TAG_NAME));
		}

		public XmlNode ToXmlNode()
		{
			XmlNode node = new XmlDocument();
			var writer = node.CreateNavigator().AppendChild();

			var s = new XmlSerializer(GetType());
			s.Serialize(writer, this);

			return node;
		}
	}
}
