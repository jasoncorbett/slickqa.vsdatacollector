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
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace SlickSharp
{
	[DataContract]
	public abstract class JsonObject<T> where T : class, IJsonObject
	{
		public static T ReadFromStream(Stream stream)
		{
			var ser = new DataContractJsonSerializer(typeof(T), string.Empty);
			return (T)ser.ReadObject(stream);
		}

		public static List<T> ReadListFromStream(Stream stream)
		{
			var ser = new DataContractJsonSerializer(typeof(List<T>));
			return (List<T>)ser.ReadObject(stream);
		}

		private byte[] ConvertToByteBuffer()
		{
			using (var tempStream = new MemoryStream())
			{
				var ser = new DataContractJsonSerializer(typeof(T));
				ser.WriteObject(tempStream, this);
				return tempStream.GetBuffer();
			}
		}

		public T Get()
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
						return ReadFromStream(stream);
					}
				}
			}
			return default(T);
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

		public void Post()
		{
			var type = typeof(T);
			var attributes = type.GetCustomAttributes(typeof(ListApiAttribute), true);
			if (attributes.Length == 0)
			{
				return;
			}
			var relUrl = ((ListApiAttribute)attributes[0]).Uri; //TODO: Fix this URL handling.
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
					using (TextReader r = new StreamReader(stream))
					{
						Console.WriteLine(r.ReadToEnd());
					}
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
