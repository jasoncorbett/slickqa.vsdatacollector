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
using System.Runtime.Serialization;

namespace SlickSharp
{
	[DataContract]
	class Result
	{
		[DataMember(Name = "attributes")]
		public Dictionary<String, String> Attributes;

		[DataMember(Name = "build")]
		public String Build;

		[DataMember(Name = "component")]
		public Component Component;

		[DataMember(Name = "config")]
		public Configuration Config;

		[DataMember(Name = "configurationOverride")]
		public String ConfigurationOverride;

		[DataMember(Name = "extensions")]
		public String Extensions;

		[DataMember(Name = "files")]
		public List<String> Files;

		[DataMember(Name = "history")]
		public String History;

		[DataMember(Name = "hostname")]
		public String Hostname;

		[DataMember(Name = "id")]
		public String Id;

		[DataMember(Name = "log")]
		public String Log;

		[DataMember(Name = "project")]
		public Project Project;

		[DataMember(Name = "reason")]
		public String Reason;

		[DataMember(Name = "recorded")]
		public String Recorded;

		[DataMember(Name = "release")]
		public Release Release;

		[DataMember(Name = "runlength")]
		public String RunLength;

		[DataMember(Name = "runstatus")]
		public RunStatus RunStatus;

		[DataMember(Name = "status")]
		public Status Status;

		[DataMember(Name = "testcase")]
		public Testcase Testcase;

		[DataMember(Name = "testrun")]
		public String TestRun;

	}
}
