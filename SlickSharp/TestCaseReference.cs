﻿/* Copyright 2012 AccessData Group, LLC.
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

namespace SlickQA.SlickSharp
{
	[DataContract]
	public class TestCaseReference : JsonObject<TestCaseReference>, IJsonObject
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


		public TestCaseReference()
		{
			Id = default(String);
			Name = default(String);
			AutomationId = default(String);
			AutomationKey = default(String);
			AutomationTool = default(String);
		}

		public TestCaseReference(Testcase testcase)
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
	}
}