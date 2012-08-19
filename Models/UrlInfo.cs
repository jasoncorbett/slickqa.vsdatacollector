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
using System.Xml;
using System.Xml.Serialization;

namespace SlickQA.DataCollector.Models
{
	public sealed class UrlInfo
	{
		public string Scheme { get; set; }
		public string HostName { get; set; }
		public int Port { get; set; }
		public string SitePath { get; set; }

		public UrlInfo(string scheme, string hostName, int port, string sitePath)
		{
			Scheme = scheme;
			HostName = hostName;
			Port = port;
			SitePath = sitePath;
		}

		public UrlInfo(UrlInfo other)
			:this(other.Scheme, other.HostName, other.Port, other.SitePath)
		{
		}

		public UrlInfo(XmlNodeList elements)
		{
			try
			{
				XmlReader reader = new XmlNodeReader(elements[0]);
				var serializer = new XmlSerializer(GetType());
				var currentUrl = serializer.Deserialize(reader) as UrlInfo;
				Debug.Assert(currentUrl != null, "currentUrl != null");
				Scheme = currentUrl.Scheme;
				HostName = currentUrl.HostName;
				Port = currentUrl.Port;
				SitePath = currentUrl.SitePath;
			}
			catch (IndexOutOfRangeException e)
			{
				//Create a default UrlInfo since none was specified in the xml
				InitializeWithDefaults();
			}
			catch (InvalidOperationException e)
			{
				//Create a default UrlInfo since we can't read from the Xml
				InitializeWithDefaults();
			}
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
			HostName = String.Empty;
			Port = 8080;
			SitePath = String.Empty;
		}

		public XmlNode ToXmlNode()
		{
			var serializer = new XmlSerializer(GetType());
			XmlNode node = new XmlDocument();
			serializer.Serialize(node.CreateNavigator().AppendChild(), this);
			return node;
		}

		public const string TAG_NAME = "Url";
	}
}
