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
using SlickQA.SlickSharp.Logging;
using SlickQA.SlickSharp.ObjectReferences;
using SlickQA.SlickSharp.Utility.Json;
using SlickQA.SlickSharp.Web;
using UriBuilder = SlickQA.SlickSharp.Web.UriBuilder;

namespace SlickQA.SlickSharp
{
	[DataContract]
	[CollectionApiPath("results")]
	[ItemApiPath("", "Id", 0)]
	public sealed class Result : JsonObject<Result>, IJsonObject
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
		public long Recorded;

		[DataMember(Name = "release")]
		public ReleaseReference ReleaseReference;

		[DataMember(Name = "runlength")]
		public String RunLength;

		public RunStatus RunStatus { get; set; }

		[DataMember(Name = "runstatus")]
		public String RunStatusString
		{
			get { return RunStatus.ToString(); }
			set
			{
				RunStatus s;
				RunStatus = Enum.TryParse(value, true, out s) ? s : RunStatus.TO_BE_RUN;
			}
		}

		public ResultStatus Status { get; set; }

		[DataMember(Name = "status")]
		public String StatusString
		{
			get { return Status.ToString(); }
			set
			{
				ResultStatus s;
				Status = Enum.TryParse(value, true, out s) ? s : ResultStatus.NO_RESULT;
			}
		}

		[DataMember(Name = "testrun")]
		public TestRunReference TestRunReference;

		[DataMember(Name = "testcase")]
		public TestcaseReference TestcaseReference;

		public void AddToLog(List<LogEntry> logaddon)
		{
			Uri uri = UriBuilder.FullUri(UriBuilder.NormalizePath(this, "results/{Id}/log"));
			IHttpWebRequest httpWebRequest = RequestFactory.Create(uri);
			httpWebRequest.Method = "POST";

			StreamConverter<LogEntry>.WriteRequestStream(httpWebRequest, logaddon);

			using (IHttpWebResponse response = httpWebRequest.GetResponse())
			{
				using (response.GetResponseStream())
				{
					//return StreamConverter<Result>.ReadFromStream(stream);
				}
			}
		}

		public bool Equals(Result other)
		{
			if (other == null)
			{
				return false;
			}
			if (Id != null && other.Id != null)
			{
				return other.Id == Id;
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var other = obj as Result;
			return other != null && Equals(other);
		}
	}
}