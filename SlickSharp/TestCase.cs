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
using System.Runtime.Serialization;
using SlickQA.SlickSharp.Attributes;
using SlickQA.SlickSharp.ObjectReferences;
using SlickQA.SlickSharp.Utility;

namespace SlickQA.SlickSharp
{
	[DataContract]
	[CollectionApiPath("testcases")]
	[ItemApiPath("", "Id", 0)]
	public sealed class Testcase : JsonObject<Testcase>, IJsonObject
	{
		[DataMember(Name = "attributes")]
		public LinkedHashMap<string> Attributes;

		[DataMember(Name = "author")]
		public String Author;

		[DataMember(Name = "automationId")]
		public String AutomationId;

		[DataMember(Name = "automationKey")]
		public String AutomationKey;

		[DataMember(Name = "automationTool")]
		public String AutomationTool;

		[DataMember(Name = "component")]
		public ComponentReference ComponentReference;

		[DataMember(Name = "automationConfiguration")]
		public String Configuration;

		[DataMember(Name = "dataDriven")]
		public List<DataDrivenProperty> DataDrivenProperties;

		[DataMember(Name = "id")]
		public String Id;

		[DataMember(Name = "automated")]
		public Boolean IsAutomated;

		[DataMember(Name = "deleted")]
		public Boolean IsDeleted;

		[DataMember(Name = "name")]
		public String Name;

		[DataMember(Name = "automationPriority")]
		public int Priority;

		[DataMember(Name = "project")]
		public ProjectReference ProjectReference;

		[DataMember(Name = "purpose")]
		public String Purpose;

		[DataMember(Name = "requirements")]
		public String Requirements;

		[DataMember(Name = "stabilityRating")]
		public int StabilityRating;

		[DataMember(Name = "steps")]
		public List<Step> Steps;

		[DataMember(Name = "tags")]
		public List<string> Tags;

		public Testcase()
		{
			DataDrivenProperties = new List<DataDrivenProperty>();
			Steps = new List<Step>();
			Tags = new List<string>();
		}

		public bool Equals(Testcase other)
		{
			if (other == null)
			{
				return false;
			}
			if (Id != null && other.Id != null)
			{
				return other.Id == Id;
			}
			return Name != null && other.Name != null && other.Name == Name;
		}

		public static Testcase GetTestCaseByAutomationId(string automationId)
		{
			try
			{
				return GetList("testcases?automationId=" + automationId)[0];
			}
			catch
			{
				return null;
			}
		}

		public static Testcase GetTestCaseByAutomationKey(string automationKey)
		{
			try
			{
				return GetList("testcases?automationKey=" + automationKey)[0];
			}
			catch
			{
				return null;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var other = obj as Testcase;
			return other != null && Equals(other);
		}
	}

	public class Step
	{
	}
}