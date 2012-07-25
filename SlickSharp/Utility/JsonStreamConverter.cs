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
using System.Runtime.Serialization.Json;
using System.Text;

namespace SlickQA.SlickSharp.Utility
{
	public static class JsonStreamConverter<T> where T : class, IJsonObject
	{
		public static T ReadFromStream(Stream stream)
		{
			var ser = new DataContractJsonSerializer(typeof(T));
			return (T)ser.ReadObject(stream);
		}

		private static List<T> ReadListFromStream(Stream stream)
		{
			var ser = new DataContractJsonSerializer(typeof(List<T>));

			return (List<T>)ser.ReadObject(stream);
		}

		public static void WriteRequestStream(IHttpWebRequest httpWebRequest, JsonObject<T> jsonObject)
		{
			byte[] body = ConvertToByteBuffer(jsonObject);
			httpWebRequest.ContentLength = body.Length;

			using (Stream stream = httpWebRequest.GetRequestStream())
			{
				stream.Write(body, 0, body.Length);
			}
		}

		public static T ReadResponse(IHttpWebRequest httpWebRequest)
		{
			using (IHttpWebResponse response = httpWebRequest.GetResponse())
			{
				using (Stream stream = response.GetResponseStream())
				{
					return ReadFromStream(stream);
				}
			}
		}

		public static List<T> ReadListResponse(IHttpWebRequest httpWebRequest)
		{
			using (IHttpWebResponse response = httpWebRequest.GetResponse())
			{
				using (Stream stream = response.GetResponseStream())
				{
					return ReadListFromStream(stream);
				}
			}
		}

		private static byte[] ConvertToByteBuffer(JsonObject<T> jsonObject)
		{
			using (var tempStream = new MemoryStream())
			{
				var ser = new DataContractJsonSerializer(typeof(T));
				ser.WriteObject(tempStream, jsonObject);
				Console.WriteLine(Encoding.UTF8.GetString(tempStream.GetBuffer()));
				Console.WriteLine();
				return tempStream.GetBuffer();
			}
		}
	}
}