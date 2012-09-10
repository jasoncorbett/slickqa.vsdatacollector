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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlickQA.SlickSharp.Attributes;
using UriBuilder = SlickQA.SlickSharp.Web.UriBuilder;

namespace SlickQA.SlickSharp.Test.Web
{

	#region ReSharper Directives

	// ReSharper disable InconsistentNaming
	// ReSharper disable ConvertToConstant.Local

	#endregion

	[TestClass]
	public sealed class SelectGetApi
	{
		[TestMethod]
		public void Chooses_the_api_with_the_lowest_index_for_which_the_field_has_a_valid_value()
		{
			var d = new DummyObjectWithFields {Id = "0123456789abcdefABCDEF", Name = "Dummy Object"};

			string expectedPath = "0123456789abcdefABCDEF";

			string actualPath = UriBuilder.SelectGetApi(d);

			Assert.AreEqual(expectedPath, actualPath);
		}

		[TestMethod]
		public void Bypasses_apis_for_which_the_field_has_the_default_value()
		{
			var d = new DummyObjectWithFields {Id = null, Name = "Dummy Object"};

			string expectedPath = "byname/Dummy%20Object";

			string actualPath = UriBuilder.SelectGetApi(d);

			Assert.AreEqual(expectedPath, actualPath);
		}

		[TestMethod]
		public void Chooses_the_api_with_the_lowest_index_for_which_the_property_has_a_valid_value()
		{
			var d = new DummyObjectWithProperties {Id = "0123456789abcdefABCDEF", Name = "Dummy Object"};

			string expectedPath = "0123456789abcdefABCDEF";

			string actualPath = UriBuilder.SelectGetApi(d);

			Assert.AreEqual(expectedPath, actualPath);
		}

		[TestMethod]
		public void Bypasses_apis_for_which_the_property_has_the_default_value()
		{
			var d = new DummyObjectWithProperties {Id = null, Name = "Dummy Object"};

			string expectedPath = "byname/Dummy%20Object";

			string actualPath = UriBuilder.SelectGetApi(d);

			Assert.AreEqual(expectedPath, actualPath);
		}

		#region Nested type: DummyObjectWithFields

		[DataContract]
		[CollectionApiPath("projects")]
		[ItemApiPath("", "Id", 0)]
		[ItemApiPath("byname", "Name", 1)]
		private sealed class DummyObjectWithFields
		{
			[DataMember(Name = "id")]
			public String Id;

			[DataMember(Name = "name")]
			public String Name;
		}

		#endregion

		#region Nested type: DummyObjectWithProperties

		[DataContract]
		[CollectionApiPath("projects")]
		[ItemApiPath("", "Id", 0)]
		[ItemApiPath("byname", "Name", 1)]
		private sealed class DummyObjectWithProperties
		{
			[DataMember(Name = "id")]
			public String Id { get; set; }

			[DataMember(Name = "name")]
			public String Name { get; set; }
		}

		#endregion
	}

	#region ReSharper Directives

	// ReSharper restore InconsistentNaming
	// ReSharper restore ConvertToConstant.Local

	#endregion
}