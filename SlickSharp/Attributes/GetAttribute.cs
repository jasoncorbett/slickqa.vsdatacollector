using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlickSharp
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=true, Inherited=true)]
	class GetAttribute : Attribute
	{
		public GetAttribute(string apiPath, string propertyName, int index)
		{
			ApiPath = apiPath;
			PropertyName = propertyName;
			Index = index;
		}

		public string ApiPath { get; private set; }
		public string PropertyName { get; private set; }
		public int Index { get; private set; }
	}
}
