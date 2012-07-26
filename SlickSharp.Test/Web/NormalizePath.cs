/* Copyright 2012 AccessData Group, LLC.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlickQA.SlickSharp.Web;

namespace SlickQA.SlickSharp.Test.Web
{
	#region ReSharper Directives
	// ReSharper disable InconsistentNaming
	// ReSharper disable ConvertToConstant.Local
	#endregion

	[TestClass]
	public sealed class NormalizePath
	{
		[TestMethod]
		public void Returns_path_unmodified_if_no_search_object()
		{
			const string EXPECTED = "listapi";
			var actual = UriBuilder.NormalizePath(null, EXPECTED);

			Assert.AreEqual(EXPECTED, actual);
		}

		[TestMethod]
		public void Returns_path_unmodified_if_no_substitution_is_required()
		{
			var p = new Project {Name = "blah"};

			const string EXPECTED = "listapi";
			var actual = UriBuilder.NormalizePath(p, EXPECTED);

			Assert.AreEqual(EXPECTED, actual);
		}

		[TestMethod]
		public void Replaces_substitution_pattern_with_object_field_value()
		{

			var listPath = "projects/{ProjectId}/releases";

			var r = new Release
			        {
			        	Name = "Foo .4",
						ProjectId = "4ffc9e3ee4b097a5f43e5d27"
			        };

			var actualPath = UriBuilder.NormalizePath(r, listPath);

			Assert.AreEqual("projects/4ffc9e3ee4b097a5f43e5d27/releases", actualPath);
		}

		[TestMethod]
		public void Will_substitute_multiple_values()
		{
			var listPath = "projects/{ProjectId}/releases/{ReleaseId}/builds";
			var b = new Build
			        {
			        	Name = "Blah build",
						ProjectId = "4ffc9e3ee4b097a5f43e5d27",
						ReleaseId = "123456789abcdefg"
			        };

			var actualPath = UriBuilder.NormalizePath(b, listPath);

			Assert.AreEqual("projects/4ffc9e3ee4b097a5f43e5d27/releases/123456789abcdefg/builds", actualPath);
		}
	}
	#region ReSharper Directives
	// ReSharper restore InconsistentNaming
	// ReSharper restore ConvertToConstant.Local
	#endregion
}
