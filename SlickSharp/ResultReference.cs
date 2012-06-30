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
	public class ResultReference : JsonObject<ResultReference>, IJsonObject
	{

		[DataMember(Name = "resultId")]
		public String Id;

		[DataMember(Name = "status")]
		public String ResultStatus;

		[DataMember(Name = "recorded")]
		public long DateRecorded;

		[DataMember(Name = "build")]
		public BuildReference Build;

		public ResultReference()
		{
			Id = default(String);
			ResultStatus = default(String);
			DateRecorded = default(long);
			Build = default(BuildReference);
		}

		public ResultReference(Result result)
		{
			Id = result.Id;
			ResultStatus = result.Status;
			DateRecorded = Convert.ToInt64(result.Recorded);
			Build = result.BuildReference;
		}

		public static implicit operator ResultReference(Result result)
		{
			return new ResultReference(result);
		}
	}
}
