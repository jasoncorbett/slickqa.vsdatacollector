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
						var assembly = Assembly.Load(File.ReadAllBytes(assemblyFile));
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

	    [XmlIgnore]
	    public string RelativeRoot { get; set; }

        [XmlIgnore]
        public string Directory { get; set; }

        [XmlIgnore]
        public List<string> Logs { get; set; }
        
        
        [XmlArray]
        [XmlArrayItem(ElementName = "Directory")]
        public List<string> SearchDirectories { get; set; }
		public string TypeName { get; set; }
		public string MethodName { get; set; }

		[XmlIgnore]
		public MethodInfo Method
		{
            get
            {
                if(Logs == null)
                    Logs = new List<string>();

                if(_method == null)
                {
                    string assemblyFile = null;

                    Directory = String.Empty;

                    if(String.IsNullOrWhiteSpace(RelativeRoot))
                        RelativeRoot = Environment.CurrentDirectory;

                    Logs.Add(String.Format("Method: RelativeRoot = {0}", RelativeRoot));

                    foreach (var searchPath in SearchDirectories)
                    {
                        Logs.Add(String.Format("Method: Checking relative path {0}", searchPath));
                        var dirPath = Path.Combine(RelativeRoot, searchPath);
                        var assemblyPath = Path.Combine(dirPath, AssemblyName);
                        Logs.Add(String.Format("Method: Checking for {0}", assemblyPath));
                        if(System.IO.Directory.Exists(dirPath) && File.Exists(assemblyPath))
                        {
                            Directory = dirPath;
                            assemblyFile = assemblyPath;
                            Logs.Add(String.Format("Method: Found assembly {0}", assemblyFile));
                            break;
                        }
                    }

                    if (assemblyFile != null)
                    {
                        Logs.Add(String.Format("Method: Loading assembly {0}", assemblyFile));
                        var assembly = Assembly.Load(File.ReadAllBytes(assemblyFile));
                        if (!string.IsNullOrWhiteSpace(TypeName))
                        {
                            Logs.Add(String.Format("Method: Loading type name {0}", TypeName));
                            Type type = assembly.GetType(TypeName);
                            Logs.Add(String.Format("Method: Loading method name {0}", MethodName));
                            _method = type.GetMethod(MethodName);
                        }
                    }
                }
                return _method;
            }
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