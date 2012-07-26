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

namespace SlickQA.SlickSharp.ObjectReferences
{
	[DataContract]
	public sealed class ReleaseReference : JsonObject<ReleaseReference>, IJsonObject
	{
		[DataMember(Name = "releaseId")]
		public String Id;

		[DataMember(Name = "name")]
		public String Name;

		public ReleaseReference()
		{
			Id = default(String);
			Name = default(String);
		}

		private ReleaseReference(Release release)
		{
			Id = release.Id;
			Name = release.Name;
		}

		public static implicit operator ReleaseReference(Release release)
		{
			return new ReleaseReference(release);
		}

		public static implicit operator Release(ReleaseReference releaseReference)
		{
			var r = new Release
			        {
			        	Id = releaseReference.Id,
						Name = releaseReference.Name
			        };
			r.Get();
			return r;
		}
	}
}