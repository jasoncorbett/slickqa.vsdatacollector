/* Copyright 2012 AccessData Group, LLC.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using SlickQA.SlickSharp;
using SlickQA.SlickSharp.Web;

namespace SlickQA.DataCollector.Configuration
{
	public sealed class ConfigurationController : IConfigurationController
	{
		private IConfigurationView _view;

		public ConfigurationController()
		{
			_view = null;
		}

		public ConfigurationController(IConfigurationView configurationEditor)
		{
			_view = configurationEditor;
		}

		public static void SetServerConfig(string scheme, string host, int port, string sitePath)
		{
			ServerConfig.Scheme = scheme;
			ServerConfig.SlickHost = host;
			ServerConfig.Port = port;
			ServerConfig.SitePath = sitePath;
		}

		public void GetProjectsClicked()
		{
			var projects = Project.GetList();

			_view.PopulateProjects(projects);
		}

		#region IConfigurationController Members


		public IConfigurationView View
		{
			set { _view = value; }
		}

		#endregion
	}
}
