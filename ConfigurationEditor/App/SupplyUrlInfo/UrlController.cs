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
		IEventHandler<ResetEvent>,
		IEventHandler<SaveDataEvent>
	{
		private static readonly List<string> _schemes = new List<string> {Uri.UriSchemeHttp, Uri.UriSchemeHttps};
		private IApplicationController AppController { get; set; }
		private ISetUrlView View { get; set; }
		private IUrlRepository UrlRepository { get; set; }
		protected UrlInfo DefaultUrl { get; set; }
		private UrlInfo CurrentUrl { get; set; }

		public UrlController(ISetUrlView view, IApplicationController appController, IUrlRepository repository)
		{
			View = view;
			View.Controller = this;
			AppController = appController;
			UrlRepository = repository;
			CurrentUrl = new UrlInfo();
			DefaultUrl = new UrlInfo();
		}


		public void SchemeSupplied(string scheme)
		{
			CurrentUrl.Scheme = scheme;
			ValidateUrl();
		}

		public void HostSupplied(string host)
		{
			CurrentUrl.HostName = host;
			ValidateUrl();
		}

		public void PortSupplied(int port)
		{
			CurrentUrl.Port = port;
			ValidateUrl();
		}

		public void SitePathSupplied(string sitePath)
		{
			CurrentUrl.SitePath = sitePath;
			ValidateUrl();
		}

		public void GetProjects()
		{
			AppController.Execute(new RetrieveProjectsData());
		}


		//TODO: Fix URL Load and Config Handling

		public void Load()
		{
			View.LoadSchemes(_schemes);
			View.SetPort(8080);
		}

		private void ValidateUrl()
		{
			bool validUrl = false;
			if (CurrentUrl.IsComplete)
			{
				validUrl = TestServerConnection(CurrentUrl);
			}

			if (validUrl)
			{
				AppController.Raise(new UrlValidatedEvent(CurrentUrl));
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

		public void Handle(SettingsLoadedEvent eventData)
		{
			CurrentUrl = UrlInfo.FromXml(eventData.Settings.Configuration);
			DefaultUrl = UrlInfo.FromXml(eventData.Settings.DefaultConfiguration);

			View.Update(CurrentUrl);
		}

		public void Handle(ResetEvent eventData)
		{
			CurrentUrl = new UrlInfo(DefaultUrl);

			View.Update(CurrentUrl);
		}

		public void Handle(SaveDataEvent eventData)
		{
			var config = eventData.Settings.Configuration;

			config.UpdateTagWithNewValue(UrlInfo.TAG_NAME, CurrentUrl.ToXmlNode());
		}
	}
}