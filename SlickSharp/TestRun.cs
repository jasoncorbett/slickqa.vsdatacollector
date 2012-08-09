﻿// Copyright 2012 AccessData Group, LLC.
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
	[CollectionApiPath("testruns")]
	[ItemApiPath("", "Id", 0)]
	public sealed class TestRun : JsonObject<TestRun>, IJsonObject, IEquatable<TestRun>
	{
		[DataMember(Name = "id")]
		public String Id;

		[DataMember(Name = "testplanId")]
		public String TestPlanId;

		[DataMember(Name = "name")]
		public String Name;

		[DataMember(Name = "config")]
		public ConfigurationReference ConfigurationReference;

		[DataMember(Name = "runtimeOptions")]
		public ConfigurationReference runtimeOptions;

		[DataMember(Name = "project")]
		public ProjectReference ProjectReference;

		[DataMember(Name = "dateCreated")]
		public DateTime Created;

		[DataMember(Name = "release")]
		public ReleaseReference ReleaseReference;

		[DataMember(Name = "build")]
		public BuildReference BuildReference;

		#region IEquatable<TestRun> Members

		public bool Equals(TestRun other)
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

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var other = obj as TestRun;
			return other != null && Equals(other);
		}

		public static bool operator ==(TestRun left, TestRun right)
		{
			if ((object)left == null || (object)right == null)
			{
				return Equals(left, right);
			}
			return left.Equals(right);
		}

		public static bool operator !=(TestRun left, TestRun right)
		{
			if (left == null || right == null)
			{
				return !Equals(left, right);
			}
			return !left.Equals(right);
		}
	}
}