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
using SlickQA.SlickSharp.Web;

namespace SlickQA.SlickSharp.Test.Web
{
	[TestClass]
	public sealed class IsNonDefaultValue
	{
		#region ReSharper Directives

		// ReSharper disable ConvertToConstant.Local
		// ReSharper disable ConditionIsAlwaysTrueOrFalse

		#endregion

		[TestMethod]
		public void Returns_false_for_null_values()
		{
			Assert.IsFalse(UriBuilder.IsNonDefaultValue(null));
		}

		[TestMethod]
		public void Returns_true_if_object_value_is_not_equal_to_type_default_value()
		{
			int count = 2;

			string name = "Blah";

			Assert.IsTrue(UriBuilder.IsNonDefaultValue(count));
			Assert.IsTrue(UriBuilder.IsNonDefaultValue(name));
		}

		[TestMethod]
		public void Returns_false_if_object_value_is_equal_to_type_default_value()
		{
			int count = default(int);
			bool isOk = default(bool);

			Assert.IsFalse(UriBuilder.IsNonDefaultValue(count));

			Assert.IsFalse(UriBuilder.IsNonDefaultValue(isOk));
		}

		#region ReSharper Directives

		// ReSharper restore ConditionIsAlwaysTrueOrFalse
		// ReSharper restore ConvertToConstant.Local

		#endregion
	}
}