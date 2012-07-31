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

namespace SlickQA.SlickSharp.ObjectReferences
{
	[DataContract]
	public sealed class ComponentReference : JsonObject<ComponentReference>, IJsonObject
	{
		[DataMember(Name = "code")]
		public String Code;

		[DataMember(Name = "id")]
		public String Id;

		[DataMember(Name = "name")]
		public String Name;

		private ComponentReference(Component component)
		{
			Id = component.Id;
			Name = component.Name;
			Code = component.Code;
		}

		public static implicit operator ComponentReference(Component component)
		{
			return new ComponentReference(component);
		}

		public static implicit operator Component(ComponentReference componentReference)
		{
			var c = new Component {Id = componentReference.Id, Name = componentReference.Name, Code = componentReference.Code};
			c.Get();
			return c;
		}
	}
}