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
using SlickQA.SlickSharp.Attributes;
using SlickQA.SlickSharp.Utility;

namespace SlickQA.SlickSharp
{
	[DataContract]
	[ListApi("projects")]
	[Get("", "Id", 0)]
	[Get("byname", "Name", 1)]
	public class Project : JsonObject<Project>, IJsonObject
	{
		[DataMember(Name = "attributes")]
		public LinkedHashMap<String> Attributes;

		[DataMember(Name = "automationTools")]
		public List<String> AutomationTools;

		[DataMember(Name = "components")]
		public List<Component> Components;

		[DataMember(Name = "configuration")]
		public Configuration Configuration;

		[DataMember(Name = "datadrivenProperties")]
		public List<DataDrivenProperty> DataDrivenProperties;

		[DataMember(Name = "defaultBuildName")]
		public String DefaultBuildName;

		[DataMember(Name = "defaultRelease")]
		public String DefaultRelease;

		[DataMember(Name = "description")]
		public String Description;

		[DataMember(Name = "extensions")]
		public List<DataExtension<Project>> Extensions;

		[DataMember(Name = "id", EmitDefaultValue = false)]
		public String Id; //TODO: Possibly org.bson.types.ObjectId compatible

		[DataMember(Name = "lastUpdated")]
		public long LastUpdated; //TODO: java.util.Date;

		[DataMember(Name = "name")]
		public String Name;

		[DataMember(Name = "releases")]
		public List<Release> Releases;

		[DataMember(Name = "tags")]
		public List<String> Tags;

		public Project()
		{
			Configuration = new Configuration();
			Releases = new List<Release>();
			Tags = new List<string>();
			Attributes = new LinkedHashMap<string>();
			AutomationTools = new List<string>();
			Components = new List<Component>();
			DataDrivenProperties = new List<DataDrivenProperty>();
			Extensions = new List<DataExtension<Project>>();
		}

		public override string ToString()
		{
			return Name;
		}

		public override bool Equals(object obj)
		{
			var other = obj as Project;
			if (other == null)
			{
				return false;
			}
			return other.Id == Id;
		}

		public override int GetHashCode()
		{
			return Convert.ToInt32(Id.Substring(0, Id.Length > 5 ? 5 : Id.Length), 16);
		}
	}
}