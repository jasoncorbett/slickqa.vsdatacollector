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
using System.Runtime.Serialization;
using SlickQA.SlickSharp.Attributes;
using SlickQA.SlickSharp.ObjectReferences;

namespace SlickQA.SlickSharp
{
	[DataContract]
	[CollectionApiPath("testcases")]
	[ItemApiPath("", "Id", 0)]
	public sealed class Testcase : JsonObject<Testcase>, IJsonObject, IEquatable<Testcase>
	{
		[DataMember(Name = "automationId")]
		public String AutomationId;

		[DataMember(Name = "automationKey")]
		public String AutomationKey;

		[DataMember(Name = "automationTool")]
		public String AutomationTool;

		[DataMember(Name = "component")]
		public ComponentReference ComponentReference;

		[DataMember(Name = "deleted")]
		public Boolean Deleted;

		[DataMember(Name = "id")]
		public String Id;

		[DataMember(Name = "automated")]
		public Boolean IsAutomated;

		[DataMember(Name = "name")]
		public String Name;

		[DataMember(Name = "project")]
		public ProjectReference ProjectReference;

		[DataMember(Name = "purpose")]
		public String Purpose;

		#region IEquatable<Testcase> Members

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

		#endregion

		//TODO: Need Unit Test Coverage Here
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

		//TODO: Need Unit Test Coverage Here
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

		public static bool operator ==(Testcase left, Testcase right)
		{
			if ((object)left == null || (object)right == null)
			{
				return Equals(left, right);
			}
			return left.Equals(right);
		}

		public static bool operator !=(Testcase left, Testcase right)
		{
			if (left == null || right == null)
			{
				return !Equals(left, right);
			}
			return !left.Equals(right);
		}
	}
}