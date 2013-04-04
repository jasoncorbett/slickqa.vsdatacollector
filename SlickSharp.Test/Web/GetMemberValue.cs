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
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UriBuilder = SlickQA.SlickSharp.Web.UriBuilder;

namespace SlickQA.SlickSharp.Test.Web
{
	[TestClass]
	public sealed class GetMemberValue
	{
		[TestMethod]
		public void Returns_an_empty_string_if_the_field_or_property_does_not_exist()
		{
			var p = new Project();
			string value = UriBuilder.GetMemberValue(p, "Blah");

			Assert.AreEqual(String.Empty, value);
		}

		[TestMethod]
		public void Can_retrieve_a_value_from_a_field()
		{
			var f = new Foo {Name = "Blah", Count = 7};

			string value = UriBuilder.GetMemberValue(f, "Name");

			Assert.AreEqual(f.Name, value);
		}

		[TestMethod]
		public void Can_retrieve_a_value_from_a_property()
		{
			var f = new Foo {Name = "Blah", Count = 7};

			string value = UriBuilder.GetMemberValue(f, "Count");

			Assert.AreEqual(f.Count.ToString(CultureInfo.InvariantCulture), value);
		}

		#region Nested type: Foo

		private sealed class Foo
		{
			public String Name;
			public int Count { get; set; }
		}

		#endregion
	}
}