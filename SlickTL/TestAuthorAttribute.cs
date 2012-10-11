using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlickQA.SlickTL
{
	[AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAuthorAttribute : Attribute, IStringValueAttribute
    {
        public TestAuthorAttribute(String authorName)
        {
            Value = authorName;
        }

	    public string Value { get; private set; }
    }
}
