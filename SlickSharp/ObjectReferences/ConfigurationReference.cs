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
	public sealed class ConfigurationReference : JsonObject<ConfigurationReference>, IJsonObject
	{
		[DataMember(Name = "configId")]
		public String ConfigId;

		[DataMember(Name = "filename")]
		public String FileName;

		[DataMember(Name = "name")]
		public String Name;

		public ConfigurationReference()
		{
		}

		private ConfigurationReference(Configuration configuration)
		{
			ConfigId = configuration.Id;
			Name = configuration.Name;
			FileName = configuration.Filename;
		}

		public static implicit operator ConfigurationReference(Configuration configuration)
		{
			return new ConfigurationReference(configuration);
		}

		public static implicit operator Configuration(ConfigurationReference reference)
		{
			var conf = new Configuration();
			conf.Id = reference.ConfigId;
			conf.Filename = reference.FileName;
			conf.Name = reference.Name;
			conf.Get();
			return conf;
		}
	}
}