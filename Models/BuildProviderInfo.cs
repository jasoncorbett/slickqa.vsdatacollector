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
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace SlickQA.DataCollector.Models
{
	[XmlRoot(TAG_NAME)]
	public sealed class BuildProviderInfo
	{
		public const string TAG_NAME = "BuildProvider";
		private MethodInfo _method;

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
				XmlNode element = elements[0];
				if (element != null)
				{
					var reader = new XmlNodeReader(element);
					var s = new XmlSerializer(GetType());
					var temp = s.Deserialize(reader) as BuildProviderInfo;
					Debug.Assert(temp != null, "temp != null");
					AssemblyName = temp.AssemblyName;
					Directory = temp.Directory;
					TypeName = temp.TypeName;
					MethodName = temp.MethodName;


					//TODO: Load from CWD
					string assemblyFile = null;
					string tempPath = Path.Combine(Environment.CurrentDirectory, AssemblyName);
					if (File.Exists(tempPath))
					{
						assemblyFile = tempPath;
					}
					else
					{
						string path = Path.Combine(Directory, AssemblyName);
						if (File.Exists(path))
						{
							assemblyFile = path;
						}
					}

					if (assemblyFile != null)
					{
						Assembly assembly = Assembly.LoadFrom(assemblyFile);
						if (!string.IsNullOrWhiteSpace(TypeName))
						{
							Type type = assembly.GetType(TypeName);
							Method = type.GetMethod(MethodName);
						}
					}
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

		public string AssemblyName { get; set; }
		public string Directory { get; set; }
		public string TypeName { get; set; }
		public string MethodName { get; set; }

		[XmlIgnore]
		public MethodInfo Method
		{
			get { return _method; }
			set
			{
				_method = value;
				UpdateFromMethod(_method);
			}
		}

		private void UpdateFromMethod(MethodInfo method)
		{
			if (method != null)
			{
				if (method.DeclaringType != null)
				{
					TypeName = method.DeclaringType.FullName;
				}
				MethodName = method.Name;
			}
			else
			{
				TypeName = string.Empty;
				MethodName = string.Empty;
			}
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
			if (Method != null)
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
			return string.Empty;
		}

		public override string ToString()
		{
			string name = string.Format(@"{0}\{1}:{2}", Directory, AssemblyName, FullMethodName());
			return @"\:" == name ? string.Empty : name;
		}

		public XmlNode ToXmlNode()
		{
			var doc = new XmlDocument();

			XPathNavigator nav = doc.CreateNavigator();
			using (XmlWriter writer = nav.AppendChild())
			{
				var ser = new XmlSerializer(GetType());
				ser.Serialize(writer, this);
			}
			XmlNode retVal = doc.FirstChild;
			doc.RemoveChild(retVal);

			return retVal;
		}
	}
}