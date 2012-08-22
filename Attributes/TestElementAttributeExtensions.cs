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
using System.Reflection;
using Microsoft.VisualStudio.TestTools.Common;

namespace SlickQA.DataCollector.Attributes
{
	public static class TestElementAttributeExtensions
	{
		private static readonly Guid _orderedTest = new Guid("{ec4800e8-40e5-4ab3-8510-b8bf29b1904d}");

		public static string GetAttributeValue<T>(this ITestElement element) where T : IStringValueAttribute
		{
			string storage = element.Storage;
			string typeName = element.HumanReadableId.Replace("." + element.Name, string.Empty);

			string feature = string.Empty;
			if (element.TestType.Id != _orderedTest)
			{
				Assembly assembly = Assembly.LoadFrom(storage);
				Type type = assembly.GetType(typeName);
				MethodInfo testMethod = type.GetMethod(element.Name);

				feature = String.Empty;
				object[] featureAttributes = testMethod.GetCustomAttributes(typeof(T), false);
				if (featureAttributes.Length != 0)
				{
					feature = ((T)featureAttributes[0]).Value;
				}
			}
			return feature;
		}
	}
}
