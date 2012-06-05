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
using System.Runtime.Serialization;
using System.Net;

namespace SlickSharp
{
	[DataContract]
	[ListApi("projects/{ProjectId}/releases/{ReleaseId}/builds")]
	[Get("", "Id", 0)]
	[Get("byname", "Name", 1)]
	public class Build :JsonObject<Build>, IJsonObject
	{
		[DataMember(Name = "built")]
		public String Built;

		[DataMember(Name = "id")]
		public String Id;

		[DataMember(Name = "name")]
		public String Name;

		[IgnoreDataMember]
		public string ProjectId { get; set; }

		[IgnoreDataMember]
		public string ReleaseId { get; set; }

		public void SetDefaultBuild()
		{
			var uri = new Uri(String.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}", ServerConfig.BaseUri.ToString(), "projects", ProjectId, "releases", ReleaseId, "setdefaultbuild", Id));
			var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Method = "GET";
			using (var response = (HttpWebResponse)httpWebRequest.GetResponse()) { }
		}
	}
}