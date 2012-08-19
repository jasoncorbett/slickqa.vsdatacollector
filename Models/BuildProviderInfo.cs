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
		public string AssemblyName { get; set; }
		public string Directory { get; set; }
		public MethodInfo Method { get; set; }

		public BuildProviderInfo(string assemblyName, string directory, MethodInfo method)
		{
			AssemblyName = assemblyName;
			Directory = directory;
			Method = method;
		}

		public BuildProviderInfo(BuildProviderInfo other)
			:this(other.AssemblyName, other.Directory, other.Method)
		{
		}

		public BuildProviderInfo(XmlNodeList elements)
		{

			try
			{
				//TODO: Figure out how to serialize the MethodInfo
			}
			catch (IndexOutOfRangeException)
			{
				InitializeWithDefaults();
			}
			catch(InvalidOperationException)
			{
				InitializeWithDefaults();
			}
		}

		private void InitializeWithDefaults()
		{
			AssemblyName = string.Empty;
			Directory = string.Empty;
			Method = null;
		}

		public XmlNode ToXmlNode()
		{
			var serializer = new XmlSerializer(GetType());
			XmlNode node = new XmlDocument();
			serializer.Serialize(node.CreateNavigator().AppendChild(), this);
			return node;
		}
	}
}