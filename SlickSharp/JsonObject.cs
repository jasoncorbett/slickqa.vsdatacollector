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

using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;

namespace SlickSharp
{
	public abstract class JsonObject<T> where T : class, IJsonObject
	{
		public static T ReadFromStream(Stream stream)
		{
			var ser = new DataContractJsonSerializer(typeof(T));
			return (T)ser.ReadObject(stream);
		}

		public void WriteToStream(Stream stream)
		{
			var ser = new DataContractJsonSerializer(typeof(T));
			ser.WriteObject(stream, this);
		}

		public static T Get()
		{
			var relUrl = ServerConfig.ObjectUri(typeof(T));
			var httpWebRequest = (HttpWebRequest)WebRequest.Create(ServerConfig.BaseUrl + relUrl);
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

		public void Post()
		{
			var relUrl = ServerConfig.ObjectUri(typeof(T));
			var httpWebRequest = (HttpWebRequest)WebRequest.Create(ServerConfig.BaseUrl + relUrl);
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Method = "POST";
			using (var stream = httpWebRequest.GetRequestStream())
			{
				WriteToStream(stream);
			}
			using (var response = (HttpWebResponse)httpWebRequest.GetResponse())
			{
				using (var stream = response.GetResponseStream())
				{
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
