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
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using SlickQA.SlickSharp.Attributes;

namespace SlickQA.SlickSharp
{
	[DataContract]
	[ListApi("projects/{ProjectId}/releases")]
	[Get("", "Id", 0)]
	public class Release : JsonObject<Release>, IJsonObject
	{
		[DataMember(Name = "builds")]
		public List<Build> Builds;

		[DataMember(Name = "defaultBuild")]
		public String DefaultBuildId;

		[DataMember(Name = "id")]
		public String Id;

		[DataMember(Name = "name")]
		public String Name;

		[DataMember(Name = "target")]
		public String Target;

		public Release()
		{
			Builds = new List<Build>();
		}

		[IgnoreDataMember]
		public string ProjectId { get; set; }

		public void SetDefaultRelease()
		{
			var uri =
				new Uri(String.Format("{0}/{1}/{2}/{3}/{4}", ServerConfig.BaseUri, "projects", ProjectId, "setdefaultrelease", Id));
			var httpWebRequest = RequestFactory.Create(uri);
			httpWebRequest.Method = "GET";
			using (var response = httpWebRequest.GetResponse())
			{
			}
		}
	}
}