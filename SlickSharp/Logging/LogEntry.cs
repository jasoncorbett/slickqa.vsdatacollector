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

namespace SlickQA.SlickSharp.Logging
{
	[DataContract]
	public class LogEntry : JsonObject<LogEntry>, IJsonObject
	{
		[DataMember(Name = "entryTime")]
		public String EntryTime;

		[DataMember(Name = "exceptionClassName")]
		public String ExceptionClassName;

		[DataMember(Name = "exceptionMessage")]
		public String ExceptionMessage;

		[DataMember(Name = "exceptionStackTrace")]
		public List<String> ExceptionStackTrace;

		public LogLevel Level { get; set; }

		[DataMember(Name = "level")]
		public String LevelString
		{
			get { return Level.ToString(); }
			set
			{
				LogLevel l;
				Level = Enum.TryParse(value, true, out l) ? l : LogLevel.INFO;
			}
		}

		[DataMember(Name = "loggerName")]
		public String LoggerName;

		[DataMember(Name = "message")]
		public String Message;
	}
}