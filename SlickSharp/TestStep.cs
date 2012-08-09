using System;
using System.Runtime.Serialization;

namespace SlickQA.SlickSharp
{
	[DataContract]
	class TestStep
	{
		[DataMember(Name = "name")]
		public String Name;

		[DataMember(Name = "expectedResult")]
		public String ExpectedResult;
	}
}
