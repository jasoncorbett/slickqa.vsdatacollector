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
using System.Collections.Generic;
using System.Net;
using System.Xml;
using SlickQA.DataCollector.ConfigurationEditor.AppController;
using SlickQA.DataCollector.ConfigurationEditor.Commands;
using SlickQA.DataCollector.ConfigurationEditor.Events;
using SlickQA.DataCollector.EventAggregator;
using SlickQA.DataCollector.Models;
using SlickQA.DataCollector.Repositories;

namespace SlickQA.DataCollector.ConfigurationEditor.App.SupplyUrlInfo
{
	public class UrlController : IGetUrlInfo,
		IEventHandler<UrlValidatedEvent>,
		IEventHandler<SettingsLoadedEvent>,
		IEventHandler<SaveDataEvent>,
		IEventHandler<ResetEvent>
	{
		private static readonly List<string> _schemes = new List<string> {Uri.UriSchemeHttp, Uri.UriSchemeHttps};
		private IApplicationController AppController { get; set; }
		private ISetUrlView View { get; set; }
		private IUrlRepository UrlRepository { get; set; }
		private UrlInfo DefaultUrlInfo { get; set; }
		private UrlInfo CurrentUrlInfo { get; set; }

		public UrlController(ISetUrlView view, IApplicationController appController, IUrlRepository repository)
		{
			View = view;
			View.Controller = this;
			AppController = appController;
			UrlRepository = repository;
		}



		public void SchemeSupplied(string scheme)
		{
			CurrentUrlInfo.Scheme = scheme;
			ValidateUrl();
		}

		public void HostSupplied(string host)
		{
			CurrentUrlInfo.HostName = host;
			ValidateUrl();
		}

		public void PortSupplied(int port)
		{
			CurrentUrlInfo.Port = port;
			ValidateUrl();
		}

		public void SitePathSupplied(string sitePath)
		{
			CurrentUrlInfo.SitePath = sitePath;
			ValidateUrl();
		}

		public void GetProjects()
		{
			AppController.Execute(new RetrieveProjectsData());
		}

		public void Load()
		{
			//TODO: Need to load config here possibly.
			View.LoadSchemes(_schemes);
			View.SetPort(8080);
		}

		private void ValidateUrl()
		{
			bool validUrl = false;
			if (CurrentUrlInfo.IsComplete)
			{
				validUrl = TestServerConnection(CurrentUrlInfo);
			}

			if (validUrl)
			{
				AppController.Raise(new UrlValidatedEvent(CurrentUrlInfo));
			}
			View.EnableButton(validUrl);
		}

		private bool TestServerConnection(UrlInfo urlInfo)
		{
			const string TEST_API = "api/updates";

			try
			{
				var uri = new Uri(urlInfo.DisplayName + TEST_API);
				var request = WebRequest.Create(uri) as HttpWebRequest;
				request.Method = "GET";
				request.Timeout = 200; // Short timeout to keep the gui responsive.
				using (request.GetResponse())
				{
				}
				return true;
			}
			catch (WebException)
			{
				return false;
			}
		}

		public void Handle(UrlValidatedEvent eventData)
		{
			UrlRepository.SetUrl(eventData.UrlInfo);
		}

		//TODO: Fix URL Load and Config Handling

		public void Handle(SettingsLoadedEvent eventData)
		{
			CurrentUrlInfo = GetUrlFromConfig(eventData.Settings.Configuration);
			DefaultUrlInfo = GetUrlFromConfig(eventData.Settings.DefaultConfiguration);

			View.LoadSchemes(_schemes);
			View.Update(CurrentUrlInfo.Scheme, CurrentUrlInfo.HostName, CurrentUrlInfo.Port, CurrentUrlInfo.SitePath);
		}

		private static UrlInfo GetUrlFromConfig(XmlElement xmlElement)
		{
			return new UrlInfo(xmlElement.GetElementsByTagName(UrlInfo.TAG_NAME));
		}

		public void Handle(SaveDataEvent eventData)
		{
			var config = eventData.Settings.Configuration;

			var node = CurrentUrlInfo.ToXmlNode();

			XmlNodeList elements = config.GetElementsByTagName(UrlInfo.TAG_NAME);
			if (elements.Count != 0)
			{
				config.ReplaceChild(node, elements[0]);
			}
			else
			{
				config.AppendChild(node);
			}
		}

		public void Handle(ResetEvent eventData)
		{
			CurrentUrlInfo = new UrlInfo(DefaultUrlInfo);

			View.LoadSchemes(_schemes);
			View.Update(CurrentUrlInfo.Scheme, CurrentUrlInfo.HostName, CurrentUrlInfo.Port, CurrentUrlInfo.SitePath);
		}
	}
}