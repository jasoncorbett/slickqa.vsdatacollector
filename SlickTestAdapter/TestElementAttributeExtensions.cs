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
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlickQA.SlickTL;

namespace SlickQA.TestAdapter
{
	public static class TestElementAttributeExtensions
	{
		private static string GetAttributeValue<T>(this MethodInfo method) where T : IStringValueAttribute
		{
			var feature = String.Empty;
			object[] featureAttributes = method.GetCustomAttributes(typeof(T), false);
			if (featureAttributes.Length != 0)
			{
				feature = ((T)featureAttributes[0]).Value;
			}
			return feature;
		}

        public static string GetAuthor(this MethodInfo method)
        {
            return method.GetAttributeValue<TestAuthorAttribute>();
        }

		public static string GetTestCaseId(this MethodInfo method)
		{
			return method.GetAttributeValue<TestCaseIdAttribute>();
		}

		public static string GetTestName(this MethodInfo method)
		{
			Debug.Assert(method.DeclaringType != null, "method.DeclaringType != null");
			string name = string.Format("{0}.{1}", method.DeclaringType.Name, method.Name);
			string testName = method.GetAttributeValue<TestNameAttribute>();
			if (!String.IsNullOrWhiteSpace(testName))
			{
				name = testName;
			}
			return name;
		}

		public static string GetAutomationKey(this MethodInfo method)
		{
			Debug.Assert(method.DeclaringType != null, "method.DeclaringType != null");
			return string.Format("{0}.{1}", method.DeclaringType.Name, method.Name);
		}

		public static string GetDescription(this MethodInfo method)
		{
			var description = string.Empty;
			var attrs = method.GetCustomAttributes(typeof(DescriptionAttribute), true);
			if (attrs.Length != 0)
			{
				description = ((DescriptionAttribute)attrs[0]).Description;
			}
			return description;
		}

		public static string GetComponent(this MethodInfo method)
		{
			return method.GetAttributeValue<TestedFeatureAttribute>();
		}

		public static List<string> GetTags(this MethodInfo method)
		{
			var tags = new List<string>();
			var properties = method.GetCustomAttributes(typeof(TestCategoryAttribute), true);
			foreach (var categories in properties.Cast<TestCategoryAttribute>().Select(property => property.TestCategories))
			{
				tags.AddRange(categories);
			}
			return tags;
		}

		public static LinkedHashMap<string> GetAttributes(this MethodInfo method)
		{
			var retMap = new LinkedHashMap<string>();

			var properties = method.GetCustomAttributes(typeof(TestPropertyAttribute), true);
			var testPropertyAttributes = properties.Cast<TestPropertyAttribute>();
			foreach (var attribute in testPropertyAttributes)
			{
				retMap.Add(attribute.Name, attribute.Value);
			}
			return retMap;
		}

		// This matches the hash used to calculate the guid for use as an id in MS testing tools
		public static Guid GetHash(this MethodInfo method)
		{
			Debug.Assert(method.DeclaringType != null, "method.DeclaringType != null");
			var fullName = string.Format("{0}.{1}", method.DeclaringType.FullName, method.Name);

			var hasher = new SHA1CryptoServiceProvider();

			var hash = hasher.ComputeHash(Encoding.Unicode.GetBytes(fullName));

			var guidBacker = new byte[16];
			Array.Copy(hash, guidBacker, 16);

			return new Guid(guidBacker);
		}
	}
}
