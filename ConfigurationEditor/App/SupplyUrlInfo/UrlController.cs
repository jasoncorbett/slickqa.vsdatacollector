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
		IEventHandler<SaveDataEvent>
	{
		public UrlController(ISetUrlView view, IApplicationController appController, IUrlRepository repository)
		{
			View = view;
			View.Controller = this;
			AppController = appController;
			UrlRepository = repository;
			CurrentUrl = new UrlInfo();
			DefaultUrl = new UrlInfo();
		}

		private IApplicationController AppController { get; set; }
		private ISetUrlView View { get; set; }
		private IUrlRepository UrlRepository { get; set; }
		private UrlInfo DefaultUrl { get; set; }
		private UrlInfo CurrentUrl { get; set; }

		#region IEventHandler<SaveDataEvent> Members

		public void Handle(SaveDataEvent eventData)
		{
		}

		#endregion

		#region IEventHandler<SettingsLoadedEvent> Members

		public void Handle(SettingsLoadedEvent eventData)
		{
			CurrentUrl = UrlInfo.FromXml(eventData.Settings.Configuration);
			DefaultUrl = UrlInfo.FromXml(eventData.Settings.DefaultConfiguration);

			if (ValidateUrl())
			{
				GetProjects();
			}
			View.Update(CurrentUrl);
		}

		#endregion

		#region IEventHandler<UrlValidatedEvent> Members

		public void Handle(UrlValidatedEvent eventData)
		{
			UrlRepository.SetUrl(eventData.UrlInfo);
		}

		#endregion

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

		public void Load()
		{
			View.Update(CurrentUrl);
		}

		private bool ValidateUrl()
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
			else
			{
				AppController.Raise(new UrlInvalidatedEvent());
			}
			View.EnableButton(validUrl);
			return validUrl;
		}

		private static bool TestServerConnection(UrlInfo urlInfo)
		{
			const string TEST_API = "api/updates";

			try
			{
				var uri = new Uri(urlInfo.DisplayName + TEST_API);
				var request = WebRequest.Create(uri) as HttpWebRequest;
				Debug.Assert(request != null, "request != null");
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
	}
}