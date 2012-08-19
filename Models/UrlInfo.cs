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

namespace SlickQA.DataCollector.Models
{
	public sealed class UrlInfo
	{
		public string Scheme { get; set; }
		public string HostName { get; set; }
		public int Port { get; set; }
		public string SitePath { get; set; }

		public UrlInfo()
		{
			InitializeWithDefaults();
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
	}
}
