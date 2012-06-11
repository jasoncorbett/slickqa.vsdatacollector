/* Copyright 2012 AccessData Group, LLC.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SlickSharp
{
	[DataContract]
	public abstract class JsonObject<T> where T : class, IJsonObject
	{
		public static T ReadFromStream(Stream stream)
		{
			var ser = new DataContractJsonSerializer(typeof(T));
			return (T)ser.ReadObject(stream);
		}

		public static List<T> ReadListFromStream(Stream stream)
		{
			var ser = new DataContractJsonSerializer(typeof(List<T>));
		   
			return (List<T>)ser.ReadObject(stream);
		}

		public byte[] ConvertToByteBuffer()
		{
			using (var tempStream = new MemoryStream())
			{
				var ser = new DataContractJsonSerializer(typeof(T));
				ser.WriteObject(tempStream, this);
				return tempStream.GetBuffer();
			}
		}

		public string normalizePath(string listPath)
		{
			Regex r = new Regex("{([^/]+)}");
			var matches = r.Matches(listPath);
			foreach (Match m in matches)
			{
				var memberName = m.Groups[1].Captures[0].Value;
				var memberValue = GetMemberValue(memberName);
				listPath = listPath.Replace(m.Value, memberValue);
			}
			return listPath;
		}

		private string GetMemberValue(string memberName)
		{

			var field = this.GetType().GetField(memberName);
			var prop = this.GetType().GetProperty(memberName);

			if (field != null)
			{
				return field.GetValue(this).ToString();
			}
			if (prop != null)
			{
				return prop.GetValue(this, null).ToString();
			}
			return String.Empty;
		}

		public T Get()
		{
			var type = typeof(T);
			var listAttributes = type.GetCustomAttributes(typeof(ListApiAttribute), true);
			var apiList = type.GetCustomAttributes(typeof(GetAttribute), true);
			string listPath = null;

			if (listAttributes.Length != 0)
			{
				listPath = ((ListApiAttribute)listAttributes[0]).Uri;
				listPath = normalizePath(listPath);
			}

			string getPath = null;
			var getApis = apiList.OrderBy(a => { return ((GetAttribute)a).Index; }).Select(b => (GetAttribute)b).ToList();
			foreach (var item in getApis)
			{
				var property = this.GetType().GetProperty(item.PropertyName);
				if (property != null)
				{
					var propVal = property.GetValue(this, null);
					var constructor = property.PropertyType.GetConstructor(System.Type.EmptyTypes);
					if (ValidItem(propVal, constructor))
					{
						getPath = item.ApiPath + "/" + propVal;
						break;
					}
				}
				else
				{
					var field = this.GetType().GetField(item.PropertyName);
					var propVal = field.GetValue(this);
					var constructor = field.FieldType.GetConstructor(System.Type.EmptyTypes);
					if (ValidItem(propVal, constructor))
					{
						getPath = item.ApiPath + "/" + propVal;
						break;
					}
				}
			}

			var fullGetPath = listPath + "/" + getPath;

			var uri = new Uri(String.Format("{0}/{1}", ServerConfig.BaseUri, fullGetPath));
			var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Method = "GET";

			try
			{
				using (var response = (HttpWebResponse)httpWebRequest.GetResponse())
				{
					using (var stream = response.GetResponseStream())
					{
						return ReadFromStream(stream);
					}
				}
			}
			catch (Exception e)
			{
				return null;
			}
		}

		private bool ValidItem(object propVal, ConstructorInfo constructor)
		{
			if (propVal != null)
			{
				if (constructor == null)
				{
					return true;
				}
				else if (propVal != constructor.Invoke(new Object[0]))
				{
					return true;
				}
			}
			return false;
		}

		public static List<T> GetList()
		{
			var type = typeof(T);
			var attributes = type.GetCustomAttributes(typeof(ListApiAttribute), true);
			if (attributes.Length != 0)
			{
				var relUrl = ((ListApiAttribute)attributes[0]).Uri; //TODO: Fix this URL handling.
				var uri = new Uri(string.Format("{0}/{1}", ServerConfig.BaseUri, relUrl));
				var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
				httpWebRequest.ContentType = "application/json";
				httpWebRequest.Method = "GET";

				using (var response = (HttpWebResponse)httpWebRequest.GetResponse())
				{
					using (var stream = response.GetResponseStream())
					{
						return ReadListFromStream(stream);
					}
				}
			}
			return new List<T>();
		}

		public static List<T> GetList(string relUrl)
		{
			var type = typeof(T);
			try
			{
				var uri = new Uri(string.Format("{0}/{1}", ServerConfig.BaseUri, relUrl));
				var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
				httpWebRequest.ContentType = "application/json";
				httpWebRequest.Method = "GET";

				using (var response = (HttpWebResponse)httpWebRequest.GetResponse())
				{
					using (var stream = response.GetResponseStream())
					{
						return ReadListFromStream(stream);
					}
				}
			}
			catch
			{
				return new List<T>();
			}
		}

		public T Post()
		{
			var type = typeof(T);
			var attributes = type.GetCustomAttributes(typeof(ListApiAttribute), true);
			if (attributes.Length == 0)
			{
				return null;
			}
			var relUrl = ((ListApiAttribute)attributes[0]).Uri; //TODO: Fix this URL handling.
			relUrl = normalizePath(relUrl);
			var uri = new Uri(string.Format("{0}/{1}", ServerConfig.BaseUri, relUrl));
			var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Method = "POST";
			var body = ConvertToByteBuffer();
			httpWebRequest.ContentLength = body.Length;
			using (var stream = httpWebRequest.GetRequestStream())
			{
				stream.Write(body, 0, body.Length);
			}
			using (var response = (HttpWebResponse)httpWebRequest.GetResponse())
			{
				using (var stream = response.GetResponseStream())
				{
					return ReadFromStream(stream);
				}
			}
		}

		public void Put()
		{
		}

		public void Delete()
		{
		}
	}
}
