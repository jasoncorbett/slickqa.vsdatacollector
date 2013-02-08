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
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using SlickQA.SlickSharp.Attributes;
using SlickQA.SlickSharp.Utility.Json;
using UriBuilder = SlickQA.SlickSharp.Web.UriBuilder;

namespace SlickQA.SlickSharp.Logging
{
	[DataContract]
	[CollectionApiPath("files")]
	[ItemApiPath("", "Id", 0)]
	public sealed class StoredFile : JsonObject<StoredFile>, IJsonObject
	{
		[DataMember(Name = "filename")]
		public String FileName;

		[DataMember(Name = "id")]
		public String Id;

		[DataMember(Name = "length")]
		public int Length;

		[DataMember(Name = "md5")]
		public String Md5;

		[DataMember(Name = ("mimetype"))]
		public String Mimetype;

		[DataMember(Name = "uploadDate")]
		public string UploadDate;

	    [DataMember(Name = "chunkSize")] 
        public int ChunkSize;

        public void PostFile(String filepath)
        {
            if (File.Exists(filepath))
            {
                var md5 = MD5.Create();

                using (var stream = File.OpenRead(filepath))
                {
                    var digest = md5.ComputeHash(stream);
                    StringBuilder sBuilder = new StringBuilder();

                    // Loop through each byte of the hashed data  
                    // and format each one as a hexadecimal string. 
                    for (int i = 0; i < digest.Length; i++)
                    {
                        sBuilder.Append(digest[i].ToString("x2"));
                    }
                    Md5 = sBuilder.ToString();
                    this.Put();
                    // reset the stream to the beggining.
                }
                using (var stream = File.OpenRead(filepath))
                {
                    var buffer = new byte[ChunkSize];
			        Uri uri = UriBuilder.FullUri(UriBuilder.NormalizePath(this, "files/{Id}/addchunk"));
                    while (true)
                    {
                        var read = stream.Read(buffer, 0, ChunkSize);
                        if (read == 0)
                            break;
                        var httpWebRequest = (HttpWebRequest) WebRequest.Create(uri);
                        httpWebRequest.ContentType = "application/octet-stream";
                        httpWebRequest.Method = "POST";
                        httpWebRequest.ContentLength = read;
                        using (Stream webstream = httpWebRequest.GetRequestStream())
                        {
                            webstream.Write(buffer, 0, read);
                        }
                        using (var response = (HttpWebResponse) httpWebRequest.GetResponse())
                        {
                            using (Stream webstream = response.GetResponseStream())
                            {
                                ApplyChanges(StreamConverter<StoredFile>.ReadFromStream(webstream));
                            }
                        }

                    }
                }
                
            }
        }

		public void PostContent(byte[] file)
		{
			Uri uri = UriBuilder.FullUri(UriBuilder.NormalizePath(this, "files/{Id}/content"));
			var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
			httpWebRequest.ContentType = "application/octet-stream";
			httpWebRequest.Method = "POST";
			httpWebRequest.ContentLength = file.Length;
			using (Stream stream = httpWebRequest.GetRequestStream())
			{
				stream.Write(file, 0, file.Length);
			}
			using (var response = (HttpWebResponse)httpWebRequest.GetResponse())
			{
				using (Stream stream = response.GetResponseStream())
				{
					StreamConverter<StoredFile>.ReadFromStream(stream);
					return;
				}
			}
		}
	}
}