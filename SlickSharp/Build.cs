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
using System.Runtime.Serialization;
using SlickQA.SlickSharp.Attributes;
using SlickQA.SlickSharp.Web;
using UriBuilder = SlickQA.SlickSharp.Web.UriBuilder;

namespace SlickQA.SlickSharp
{
    using JetBrains.Annotations;

    [DataContract]
	[CollectionApiPath("projects/{ProjectId}/releases/{ReleaseId}/builds")]
	[ItemApiPath("", "Id", 0)]
	[ItemApiPath("byname", "Name", 1)]
	public sealed class Build : JsonObject<Build>, IJsonObject
	{
		[DataMember(Name = "built")]
		public String Built;

		[DataMember(Name = "id")]
		public String Id;

		[DataMember(Name = "name")]
		public String Name;

	    [DataMember(Name = "description")] 
        public String Description;

		[IgnoreDataMember]
		public string ProjectId { [UsedImplicitly] get; set; }

		[IgnoreDataMember]
		public string ReleaseId { [UsedImplicitly] get; set; }

        [PublicAPI]
		public void SetAsDefault()
		{
			Uri uri =
				UriBuilder.FullUri(UriBuilder.NormalizePath(this, "projects/{ProjectId}/releases/{ReleaseId}/setdefaultbuild/{Id}"));
			IHttpWebRequest httpWebRequest = RequestFactory.Create(uri);
			httpWebRequest.Method = "GET";
			using (httpWebRequest.GetResponse())
			{
			}
		}

		public bool Equals(Build other)
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
			var other = obj as Build;
			return other != null && Equals(other);
		}
	}
}