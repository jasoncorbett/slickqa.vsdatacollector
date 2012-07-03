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
using System.Runtime.Serialization;

namespace SlickSharp
{
	[DataContract]
	[ListApi("testcases")]
	public class Testcase : JsonObject<Testcase>, IJsonObject
	{

		[DataMember(Name = "automationId")]
		public String AutomationId;

		[DataMember(Name = "automationKey")]
		public String AutomationKey;

		[DataMember(Name = "automationTool")]
		public String AutomationTool;

		[DataMember(Name = "name")]
		public String Name;

		[DataMember(Name = "id")]
		public String Id;

		[DataMember(Name = "automated")]
		public Boolean IsAutomated;

		[DataMember(Name = "project")]
		public ProjectReference ProjectReference;

		[DataMember(Name = "component")]
		public ComponentReference ComponentReference;

		[DataMember(Name = "purpose")]
		public String Purpose;

		[DataMember(Name = "deleted")]
		public Boolean Deleted;

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
	}
}