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