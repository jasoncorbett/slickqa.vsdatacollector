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
	[CollectionApiPath("testplans")]
	[ItemApiPath("", "Id", 0)]
    [ItemQueryParameter("name", "Name")]
	public sealed class TestPlan : JsonObject<TestPlan>, IJsonObject
	{
		[DataMember(Name = "createdBy")]
		public String CreatedBy;

		[DataMember(Name = "id")]
		public String Id;

		[DataMember(Name = "name")]
		public String Name;

		[DataMember(Name = "project")]
		public ProjectReference ProjectReference;

		public override string ToString()
		{
			return Name;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var other = obj as TestPlan;
			return other != null && Equals(other);
		}

		public bool Equals(TestPlan other)
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
	}
}