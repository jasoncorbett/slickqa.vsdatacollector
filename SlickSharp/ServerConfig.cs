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

using System;

namespace SlickSharp
{
	public static class ServerConfig
	{
		public static string SlickHost { private get; set; }
		public static string SitePath { private get; set; }
		public static int Port { private get; set; }

		public static Uri BaseUri
		{
			get { return new Uri(String.Format("http://{0}:{1}/{2}/api", SlickHost, Port, SitePath)); }
		}
	}
}