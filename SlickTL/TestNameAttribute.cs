﻿// Copyright 2012 AccessData Group, LLC.
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

namespace SlickQA.SlickTL
{
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class TestNameAttribute : Attribute, IStringValueAttribute
	{
		public TestNameAttribute(string testName)
		{
			Value = testName;
		}

		#region IStringValueAttribute Members

		public string Value { get; private set; }

		#endregion
	}
}
