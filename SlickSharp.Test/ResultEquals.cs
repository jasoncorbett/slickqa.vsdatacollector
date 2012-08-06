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

namespace SlickQA.SlickSharp.Test
{
	[TestClass]
	public sealed class ResultEquals
	{
		// ReSharper disable SuspiciousTypeConversion.Global
		// ReSharper disable ConvertToConstant.Local
		[TestMethod]
		public void Is_false_for_non_result_objects()
		{
			var b = new Result { Id = "1234567890" };

			string f = "1234567890";

			Assert.IsFalse(b.Equals(f));
		}

		// ReSharper restore ConvertToConstant.Local
		// ReSharper restore SuspiciousTypeConversion.Global

		[TestMethod]
		public void Is_true_for_items_with_the_same_id()
		{
			var item = new Result { Id = "1234567890" };
			var item2 = new Result { Id = "1234567890" };

			Assert.IsTrue(item.Equals(item2));
		}

		[TestMethod]
		public void Is_false_for_items_with_different_non_null_ids()
		{
			var item = new Result { Id = "1234567890" };
			var item2 = new Result { Id = "0987654321" };

			Assert.IsFalse(item.Equals(item2));
		}
	}
}
