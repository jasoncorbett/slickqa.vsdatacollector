using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlickQA.SlickSharp.Attributes
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    class ItemQueryParameterAttribute : Attribute
    {
        public string QueryParamName { get; private set; }
        public string PropertyName { get; private set; }

		public ItemQueryParameterAttribute(string queryParamName, string propertyName)
		{
		    QueryParamName = queryParamName;
		    PropertyName = propertyName;
		}
    }
}
