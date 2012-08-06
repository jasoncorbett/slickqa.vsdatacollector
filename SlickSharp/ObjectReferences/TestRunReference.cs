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

namespace SlickQA.SlickSharp.ObjectReferences
{
	[DataContract]
	public sealed class TestRunReference : JsonObject<TestRunReference>, IJsonObject
	{
		[DataMember(Name = "name")]
		public String Name;

		[DataMember(Name = "testrunId")]
		public String TestRunId;

		public TestRunReference()
		{
		}

		private TestRunReference(TestRun testRun)
		{
			TestRunId = testRun.Id;
			Name = testRun.Name;
		}

		public static implicit operator TestRunReference(TestRun testRun)
		{
			return new TestRunReference(testRun);
		}

		public static implicit operator TestRun(TestRunReference testRunReference)
		{
			var t = new TestRun {Id = testRunReference.TestRunId, Name = testRunReference.Name};
			t.Get();
			return t;
		}
	}
}