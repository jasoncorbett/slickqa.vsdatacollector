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
	public class BuildReference : JsonObject<BuildReference>, IJsonObject
	{
		[DataMember(Name = "buildId")]
		public String Id;

		[DataMember(Name = "name")]
		public String Name;

		public BuildReference()
		{
			Id = default(String);
			Name = default(String);
		}

		public BuildReference(Build build)
		{
			Id = build.Id;
			Name = build.Name;
		}

		public static implicit operator BuildReference(Build build)
		{
			return new BuildReference(build);
		}
	}
}
