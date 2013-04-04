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
using System.Globalization;
using System.Runtime.Serialization;
using SlickQA.SlickSharp.Attributes;
using SlickQA.SlickSharp.ObjectReferences;

namespace SlickQA.SlickSharp
{
	[DataContract]
	[CollectionApiPath("testruns")]
	[ItemApiPath("", "Id", 0)]
	public sealed class TestRun : JsonObject<TestRun>, IJsonObject
	{
		[DataMember(Name = "build")]
		public BuildReference BuildReference;

		[DataMember(Name = "config")]
		public ConfigurationReference ConfigurationReference;

        [DataMember(Name = "dateCreated")]
        public long CreatedTime
        {
            get
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return Convert.ToInt64((Created.ToUniversalTime() - epoch).TotalMilliseconds);
            }
            set
            {
                DateTime t;
                Created = DateTime.TryParse(value.ToString(CultureInfo.InvariantCulture), out t) ? t : DateTime.UtcNow;
            }
        }

        public DateTime Created { get; set; }

		[DataMember(Name = "id")]
		public String Id;

		[DataMember(Name = "name")]
		public String Name;

		[DataMember(Name = "project")]
		public ProjectReference ProjectReference;

		[DataMember(Name = "release")]
		public ReleaseReference ReleaseReference;

		[DataMember(Name = "runtimeOptions")]
		public ConfigurationReference RuntimeOptions;

		[DataMember(Name = "testplanId")]
		public String TestPlanId;

        [DataMember(Name = "runStarted")]
        public long RunStartedTime
        {
            get
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return Convert.ToInt64((RunStarted.ToUniversalTime() - epoch).TotalMilliseconds);
            }
            set
            {
                DateTime t;
                RunStarted = DateTime.TryParse(value.ToString(CultureInfo.InvariantCulture), out t) ? t : DateTime.UtcNow;
            }
        }

        public DateTime RunStarted { get; set; }

        [DataMember(Name = "runFinished")]
        public long RunFinishedTime
        {
            get
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return Convert.ToInt64((RunFinished.ToUniversalTime() - epoch).TotalMilliseconds);
            }
            set
            {
                DateTime t;
                RunFinished = DateTime.TryParse(value.ToString(CultureInfo.InvariantCulture), out t) ? t : DateTime.UtcNow;
            }
        }

        public DateTime RunFinished { get; set; }

        public RunStatus State { get; set; }

		[DataMember(Name = "state")]
		public String StateString
		{
			get { return State.ToString(); }
			set
			{
				RunStatus s;
				State = Enum.TryParse(value, true, out s) ? s : RunStatus.TO_BE_RUN;
			}
		}

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

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var other = obj as TestRun;
			return other != null && Equals(other);
		}
	}
}