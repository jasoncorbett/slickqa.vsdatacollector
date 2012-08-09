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
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Xml;
using SlickQA.SlickSharp.Web;

namespace SlickQA.DataCollector.Configuration
{
	public sealed class SlickUrl : INotifyPropertyChanged
	{
		public SlickUrl(string scheme = "http", string host = "", int port = 8080, string sitePath = "slick")
		{
			Scheme = scheme;
			Host = host;
			Port = port;
			SitePath = sitePath;
		}

		private string _scheme;
		public string Scheme
		{
			get { return _scheme; }
			set
			{
				_scheme = value;
				NotifyPropertyChanged("Scheme");
			}
		}

		private string _host;
		public string Host
		{
			get { return _host; }
			set
			{
				_host = value;
				NotifyPropertyChanged("Host");
			}
		}

		private int _port;
		public int Port
		{
			get { return _port; }
			set
			{
				_port = value;
				NotifyPropertyChanged("Port");
			}
		}

		private string _sitePath;
		public string SitePath
		{
			get { return _sitePath; }
			set
			{
				_sitePath = value;
				NotifyPropertyChanged("SitePath");
			}
		}

		public SlickUrl(XmlNode urlElem)
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

		public bool IsValid
		{
			get
			{
				SetServerConfig(Scheme, Host, Port, SitePath);
				return Uri.CheckSchemeName(Scheme) && !String.IsNullOrWhiteSpace(Host);
			}
		}

		private void NotifyPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public static void SetServerConfig(string scheme, string host, int port, string sitePath)
		{
			ServerConfig.Scheme = scheme;
			ServerConfig.SlickHost = host;
			ServerConfig.Port = port;
			ServerConfig.SitePath = sitePath;
		}
	}
}