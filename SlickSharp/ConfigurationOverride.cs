using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SlickQA.SlickSharp
{
	[DataContract]
	class ConfigurationOverride : JsonObject<ConfigurationOverride>, IJsonObject
	{
		[DataMember(Name = "key")]
		public String Key;

		[DataMember(Name = "value")]
		public String Value;

		[DataMember(Name = "isRequirement")]
		public bool IsRequirement;
	}
}
