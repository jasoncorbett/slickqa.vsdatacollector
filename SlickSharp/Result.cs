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
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using SlickQA.SlickSharp.Attributes;
using SlickQA.SlickSharp.Utility.Json;

namespace SlickQA.SlickSharp
{
	[DataContract]
	[ListApi("results")]
	public class Result : JsonObject<Result>, IJsonObject
	{
		[DataMember(Name = "attributes")]
		public Dictionary<String, String> Attributes;

		[DataMember(Name = "build")]
		public BuildReference BuildReference;

		[DataMember(Name = "component")]
		public ComponentReference ComponentReference;

		[DataMember(Name = "configurationOverride")]
		public String ConfigurationOverride;

		[DataMember(Name = "config")]
		public ConfigurationReference ConfigurationReference;

		[DataMember(Name = "extensions")]
		public String Extensions;

		[DataMember(Name = "files")]
		public List<StoredFile> Files;

		[DataMember(Name = "history")]
		public List<ResultReference> History;

		[DataMember(Name = "hostname")]
		public String Hostname;

		[DataMember(Name = "id")]
		public String Id;

		[DataMember(Name = "log")]
		public List<LogEntry> Log;

		[DataMember(Name = "project")]
		public ProjectReference ProjectReference;

		[DataMember(Name = "reason")]
		public String Reason;

		[DataMember(Name = "recorded")]
		public String Recorded;

		[DataMember(Name = "release")]
		public ReleaseReference ReleaseReference;

		[DataMember(Name = "runlength")]
		public String RunLength;

		[DataMember(Name = "runstatus")]
		public String RunStatus;

		[DataMember(Name = "status")]
		public String Status;

		[DataMember(Name = "testcase")]
		public TestCaseReference TestCaseReference;

		[DataMember(Name = "testrun")]
		public TestRunReference TestRunReference;

		public static void AddToLog(String resultId, List<LogEntry> logaddon)
		{
			var uri = new Uri(string.Format("{0}/{1}/{2}/{3}", ServerConfig.BaseUri, "results", resultId, "log"));
			var httpWebRequest = RequestFactory.Create(uri);
			httpWebRequest.Method = "POST";

			StreamConverter<LogEntry>.WriteRequestStream(httpWebRequest, logaddon);

			using (var response = httpWebRequest.GetResponse())
			{
				using (Stream stream = response.GetResponseStream())
				{
					//return StreamConverter<Result>.ReadFromStream(stream);
				}
			}
		}
	}
}