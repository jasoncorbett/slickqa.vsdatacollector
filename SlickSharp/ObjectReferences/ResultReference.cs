﻿// Copyright 2012 AccessData Group, LLC.
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
	public sealed class ResultReference : JsonObject<ResultReference>, IJsonObject
	{
		[DataMember(Name = "build")]
		public BuildReference Build;

		[DataMember(Name = "recorded")]
		public string RecordedString
		{
			get { return Recorded.ToUniversalTime().ToString("o"); }
			set
			{
				DateTime t;
				Recorded = DateTime.TryParse(value, out t) ? t : DateTime.UtcNow;
			}
		}

		public DateTime Recorded { get; set; }

		[DataMember(Name = "resultId")]
		public String Id;

		public ResultStatus ResultStatus { get; set; }

		[DataMember(Name = "status")]
		public String ResultStatusString
		{
			get { return ResultStatus.ToString(); }
			set
			{
				ResultStatus s;
				ResultStatus = Enum.TryParse(value, true, out s) ? s : ResultStatus.NO_RESULT;
			}
		}

		public ResultReference()
		{
		}

		private ResultReference(Result result)
		{
			Id = result.Id;
			ResultStatus = result.Status;
			Recorded = result.Recorded;
			Build = result.BuildReference;
		}

		public static implicit operator ResultReference(Result result)
		{
			return result == null ? null : new ResultReference(result);
		}

		public static implicit operator Result(ResultReference resultReference)
		{
			var r = new Result
			        {
			        	Id = resultReference.Id,
			        	Status = resultReference.ResultStatus,
			        	Recorded = resultReference.Recorded,
			        	BuildReference = resultReference.Build
			        };
			r.Get();
			return r;
		}
	}
}