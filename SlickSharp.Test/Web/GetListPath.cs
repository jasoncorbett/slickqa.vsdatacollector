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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlickQA.SlickSharp.Attributes;
using SlickQA.SlickSharp.Web;

namespace SlickQA.SlickSharp.Test.Web
{

	#region ReSharper Directives

	// ReSharper disable InconsistentNaming

	#endregion

	[TestClass]
	public sealed class GetListPath
	{
		[TestMethod]
		[ExpectedException(typeof(MissingPostUriException))]
		public void ListApi_attribute_missing_throws()
		{
			UriBuilder.GetListPath(new Test());
		}

		[TestMethod]
		public void Returns_api_path_when_present()
		{
			Assert.AreEqual("foo_bar_baz", UriBuilder.GetListPath(new ListTest()));
		}

		#region Nested type: ListTest

		[CollectionApiPath("foo_bar_baz")]
		private sealed class ListTest : JsonObject<ListTest>, IJsonObject
		{
		}

		#endregion

		#region Nested type: Test

		private sealed class Test : JsonObject<Test>, IJsonObject
		{
		}

		#endregion
	}

	#region ReSharper Directives

	// ReSharper restore InconsistentNaming

	#endregion
}