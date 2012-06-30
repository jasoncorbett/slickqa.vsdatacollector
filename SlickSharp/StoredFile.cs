using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;

namespace SlickSharp
{
	[DataContract]
	[ListApi("files")]
	public class StoredFile : JsonObject<StoredFile>, IJsonObject
	{
		[DataMember(Name = "filename")]
		public string FileName;

		[DataMember(Name = "id")]
		public string Id;

		[DataMember(Name = "mimetype")]
		public string Mimetype;

		public StoredFile PostContent(byte[] file)
		{
			var uri = String.Format("{0}/{1}/{2}/{3}", ServerConfig.BaseUri, "files", Id, "content");
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(uri));
			httpWebRequest.ContentType = "application/octet-stream";
			httpWebRequest.Method = "POST";
			httpWebRequest.ContentLength = file.Length;

			using (Stream requestStream = httpWebRequest.GetRequestStream())
			{
				requestStream.Write(file, 0, file.Length);
			}
			using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
			{
				using (Stream responseStream = httpWebResponse.GetResponseStream())
				{
					return ReadFromStream(responseStream);
				}
			}
		}
	}
}
