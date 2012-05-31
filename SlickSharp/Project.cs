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
using System.Runtime.Serialization;

namespace SlickSharp
{
	[DataContract]
	public class Project : JsonObject<Project>, IJsonObject
	{
		[DataMember(Name = "attributes")]
		public Dictionary<String, String> Attributes;

		[DataMember(Name = "automationTools")]
		public List<String> AutomationTools;

		[DataMember(Name = "components")]
		public List<Component> Components;

		[DataMember(Name = "configuration")]
		public Configuration Configuration;

		[DataMember(Name = "datadrivenProperties")]
		public List<String> DataDrivenProperties;

		[DataMember(Name = "defaultBuildName")]
		public String DefaultBuildName;

		[DataMember(Name = "defaultRelease")]
		public String DefaultRelease;

		[DataMember(Name = "description")]
		public String Description;

		[DataMember(Name = "extensions")]
		public List<String> Extensions;

		[DataMember(Name = "id")]
		public String Id; //TODO: Possibly GUID

		[DataMember(Name = "lastUpdated")]
		public int LastUpdated;

		[DataMember(Name = "name")]
		public String Name;

		[DataMember(Name = "releases")]
		public List<Release> Releases;

		[DataMember(Name = "tags")]
		public List<String> Tags;
	}
}
