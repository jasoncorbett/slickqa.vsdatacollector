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
using System.Net;
using System.Collections.Generic;

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

		[DataMember(Name = "project")]
		public ProjectReference ProjectReference;

		public static Testcase GetTestCaseByAutomationId(string AutomationId)
		{
			var uri = new Uri(string.Format("{0}/{1}{2}", ServerConfig.BaseUri, "testcases?automationId=", AutomationId));
			var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Method = "GET";

			try
			{
				using (var response = (HttpWebResponse)httpWebRequest.GetResponse())
				{
					using (var stream = response.GetResponseStream())
					{
						return ReadListFromStream(stream)[0];  // slick returns a list in case there are more than one so I will return the first one...
					}
				}
			}
			catch
			{
				return null;
			}
		}

	}
}