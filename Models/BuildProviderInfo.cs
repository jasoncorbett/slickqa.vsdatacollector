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
using System.Xml;
using System.Xml.Serialization;

namespace SlickQA.DataCollector.Models
{
	public sealed class BuildProviderInfo
	{
		public const string TAG_NAME = "BuildProvider";

		public string AssemblyName { get; private set; }
		public string Directory { get; private set; }
		public MethodInfo Method { get; private set; }

		public BuildProviderInfo(string assemblyName, string directory, MethodInfo method)
		{
			AssemblyName = assemblyName;
			Directory = directory;
			Method = method;
		}

		private BuildProviderInfo(XmlNodeList elements)
		{
			try
			{
				var element = elements[0];
				if (element != null)
				{
					var reader = new XmlNodeReader(element);
					var s = new XmlSerializer(GetType());
					var temp = s.Deserialize(reader) as BuildProviderInfo;
					AssemblyName = temp.AssemblyName;
					Directory = temp.Directory;
					Method = temp.Method;
				}
				else
				{
					InitializeWithDefaults();
				}
			}
			catch (InvalidOperationException)
			{
				InitializeWithDefaults();
			}
		}

		public BuildProviderInfo()
		{
			InitializeWithDefaults();
		}

		public BuildProviderInfo(BuildProviderInfo other)
			:this(other.AssemblyName, other.Directory, other.Method)
		{
		}

		private void InitializeWithDefaults()
		{
			AssemblyName = String.Empty;
			Directory = String.Empty;
			Method = null;
		}

		public static BuildProviderInfo FromXml(XmlElement configuration)
		{
			return new BuildProviderInfo(configuration.GetElementsByTagName(TAG_NAME));
		}

		private string FullMethodName()
		{
			string fullMethodName;
			if (Method.DeclaringType != null)
			{
				fullMethodName = Method.DeclaringType.FullName + "." + Method.Name;
			}
			else
			{
				fullMethodName = Method.Name;
			}
			return fullMethodName;
		}

		public override string ToString()
		{
			return string.Format("{0}\\{1}:{2}", Directory, AssemblyName, FullMethodName());
		}

		public XmlNode ToXmlNode()
		{
			XmlNode node = new XmlDocument();
			var writer = node.CreateNavigator().AppendChild();

			var s = new XmlSerializer(GetType());
			s.Serialize(writer, this);

			return node;
		}
	}
}