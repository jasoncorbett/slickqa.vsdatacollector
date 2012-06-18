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
using System.Runtime.Serialization;
using System.Net;

namespace SlickSharp
{
	[DataContract]
	[ListApi("files")]
	public class StoredFile : JsonObject<StoredFile>, IJsonObject
	{

		[DataMember(Name = "id")]
		public String Id;

		[DataMember(Name = "filename")]
		public String FileName;

		[DataMember(Name=("mimetype"))]
		public String Mimetype;  

		public StoredFile postContent(byte[] file)
		{
			var uri = new Uri(string.Format("{0}/{1}/{2}/{3}", ServerConfig.BaseUri, "files", Id, "content"));
			var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
			httpWebRequest.ContentType = "application/octet-stream";
			httpWebRequest.Method = "POST";
			httpWebRequest.ContentLength = file.Length;
			using (var stream = httpWebRequest.GetRequestStream())
			{
				stream.Write(file, 0, file.Length);
			}
			using (var response = (HttpWebResponse)httpWebRequest.GetResponse())
			{
				using (var stream = response.GetResponseStream())
				{
					return ReadFromStream(stream);
				}
			}
		}
	}
}
