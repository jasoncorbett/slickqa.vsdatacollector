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
	[CollectionApiPath("configurations")]
	[ItemApiPath("", "Id", 0)]
	public sealed class Configuration : JsonObject<Configuration>, IJsonObject
	{
		[DataMember(Name = "configurationData")]
		public LinkedHashMap<String> ConfigurationData;

		[DataMember(Name = "configurationType")]
		public String ConfigurationType;

		[DataMember(Name = "filename")]
		public String Filename;

		[DataMember(Name = "id")]
		public String Id;

		[DataMember(Name = "name")]
		public String Name;

		public static Configuration GetEnvironmentConfiguration(string name)
		{
			try
			{
				return GetList("configurations?configurationType=ENVIRONMENT&name=" + name)[0];
			}
			catch
			{
				return null;
			}
		}

		public bool Equals(Configuration other)
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
			var other = obj as Configuration;
			return other != null && Equals(other);
		}
	}
}