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

namespace SlickQA.SlickSharp
{
	[DataContract]
	[CollectionApiPath("projects/{ProjectId}/components")]
	[ItemApiPath("", "Id", 0)]
	public sealed class Component : JsonObject<Component>, IJsonObject, IEquatable<Component>
	{
		[DataMember(Name = "code")]
		public String Code;

		[DataMember(Name = "description")]
		public String Description;

		[DataMember(Name = "id")]
		public String Id;

		[DataMember(Name = "name")]
		public String Name;

		[IgnoreDataMember]
		public String ProjectId;

		#region IEquatable<Component> Members

		public bool Equals(Component other)
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

		#endregion

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			var other = obj as Component;
			return other != null && Equals(other);
		}

		public static bool operator ==(Component left, Component right)
		{
			if ((object)left == null || (object)right == null)
			{
				return Equals(left, right);
			}
			return left.Equals(right);
		}

		public static bool operator !=(Component left, Component right)
		{
			if (left == null || right == null)
			{
				return !Equals(left, right);
			}
			return !left.Equals(right);
		}
	}
}