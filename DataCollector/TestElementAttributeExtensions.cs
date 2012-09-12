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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualStudio.TestTools.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlickQA.DataCollector.Attributes;
using SlickQA.SlickSharp;
using SlickQA.SlickSharp.Utility;

namespace SlickQA.DataCollector
{
	public static class TestElementAttributeExtensions
	{
		private static readonly Guid _orderedTest = new Guid("{ec4800e8-40e5-4ab3-8510-b8bf29b1904d}");

		public static string GetAttributeValue<T>(this ITestElement element) where T : IStringValueAttribute
		{
			string storage = element.Storage;
			string typeName = element.HumanReadableId.Replace("." + element.Name, string.Empty);

			Assembly assembly = Assembly.LoadFrom(storage);
			Type type = assembly.GetType(typeName);
			MethodInfo testMethod = type.GetMethod(element.Name);

			string feature = String.Empty;
			object[] featureAttributes = testMethod.GetCustomAttributes(typeof(T), false);
			if (featureAttributes.Length != 0)
			{
				feature = ((T)featureAttributes[0]).Value;
			}
			return feature;
		}

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

		public static string GetTestCaseId(this MethodInfo method)
		{
			var id = method.GetAttributeValue<TestCaseIdAttribute>();
			return id;
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
			string name = string.Format("{0}.{1}", method.DeclaringType.Name, method.Name);
			return name;
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

		public static Component GetComponent(this MethodInfo method, string projectId)
		{
			string testedFeature = method.GetAttributeValue<TestedFeatureAttribute>();
			Component component = null;
			if (!String.IsNullOrWhiteSpace(testedFeature))
			{
				component = new Component { Name = testedFeature, ProjectId = projectId };
				component.Get(true);
			}
			return component;
		}

		public static List<string> GetTags(this MethodInfo method)
		{
			var tags = new List<string>();
			var properties = method.GetCustomAttributes(typeof(TestCategoryAttribute), true);
			foreach (TestCategoryAttribute property in properties)
			{
				var cat = property.TestCategories;
				tags.AddRange(cat);
			}
			return tags;
		}

		public static LinkedHashMap<string> GetAttributes(this MethodInfo method)
		{
			var attributes = new LinkedHashMap<string>();
			var properties = method.GetCustomAttributes(typeof(TestPropertyAttribute), true);
			foreach (TestPropertyAttribute property in properties)
			{
				attributes.Add(new KeyValuePair<string, string>(property.Name, property.Value));
			}
			return attributes;
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
