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
	public sealed class TestCaseReference : JsonObject<TestCaseReference>, IJsonObject
	{
		[DataMember(Name = "automationId")]
		public String AutomationId;

		[DataMember(Name = "automationKey")]
		public String AutomationKey;

		[DataMember(Name = "automationTool")]
		public String AutomationTool;

		[DataMember(Name = "testcaseId")]
		public String Id;

		[DataMember(Name = "name")]
		public String Name;


		private TestCaseReference(Testcase testcase)
		{
			Id = testcase.Id;
			Name = testcase.Name;
			AutomationId = testcase.AutomationId;
			AutomationKey = testcase.AutomationKey;
			AutomationTool = testcase.AutomationTool;
		}

		public static implicit operator TestCaseReference(Testcase testcase)
		{
			return new TestCaseReference(testcase);
		}

		public static implicit operator Testcase(TestCaseReference testCaseReference)
		{
			var t = new Testcase
			        {
			        	Id = testCaseReference.Id,
			        	Name = testCaseReference.Name,
			        	AutomationId = testCaseReference.AutomationId,
			        	AutomationKey = testCaseReference.AutomationKey,
			        	AutomationTool = testCaseReference.AutomationTool
			        };
			t.Get();
			return t;
		}
	}
}