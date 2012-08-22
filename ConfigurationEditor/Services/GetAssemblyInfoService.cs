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
using System.Reflection;
using SlickQA.DataCollector.ConfigurationEditor.AppController;
using SlickQA.DataCollector.ConfigurationEditor.Commands;
using SlickQA.DataCollector.ConfigurationEditor.Events;

namespace SlickQA.DataCollector.ConfigurationEditor.Services
{
	internal class GetAssemblyInfoService : ICommand<GetAssemblyInfoData>
	{
		public GetAssemblyInfoService(IApplicationController appController)
		{
			AppController = appController;
		}

		private IApplicationController AppController { get; set; }

		#region ICommand<GetAssemblyInfoData> Members

		public void Execute(GetAssemblyInfoData commandData)
		{
			if (string.IsNullOrWhiteSpace(commandData.AssemblyPath))
			{
				return;
			}

			var candidateTypes = new Dictionary<Type, List<MethodInfo>>();

			IEnumerable<Type> types = GetAssemblyTypes(commandData);
			foreach (Type type in types)
			{
				if (type == null)
				{
					continue;
				}

				List<MethodInfo> methods = GetMethodsWithStringReturnAndNoArguments(type);

				if (methods.Count == 0)
				{
					continue;
				}
				candidateTypes.Add(type, methods);
			}

			AppController.Raise(new AssemblyParsedEvent(candidateTypes));
		}

		#endregion

		private static List<MethodInfo> GetMethodsWithStringReturnAndNoArguments(IReflect type)
		{
			var retVal = new List<MethodInfo>();

			MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Static);
			foreach (MethodInfo methodInfo in methods)
			{
				try
				{
					if (methodInfo.ReturnType != typeof(string))
					{
						continue;
					}
				}
				catch (Exception)
				{
					continue;
				}

				ParameterInfo[] parms = methodInfo.GetParameters();
				if (parms.Length != 0)
				{
					continue;
				}

				retVal.Add(methodInfo);
			}
			return retVal;
		}

		private static IEnumerable<Type> GetAssemblyTypes(GetAssemblyInfoData commandData)
		{
			Type[] types;
			try
			{
				Assembly assembly = Assembly.LoadFile(commandData.AssemblyPath);
				types = assembly.GetTypes();
			}
			catch (ReflectionTypeLoadException e)
			{
				types = e.Types;
			}
			return types;
		}
	}
}
