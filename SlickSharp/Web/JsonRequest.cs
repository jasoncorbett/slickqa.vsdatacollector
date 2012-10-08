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
using System.IO;
using System.Net;

namespace SlickQA.SlickSharp.Web
{
	[Serializable]
	public sealed class JsonRequest : WebRequest, IHttpWebRequest
	{
		private readonly WebRequest _request;

		public JsonRequest(WebRequest webRequest)
		{
			_request = webRequest;
			_request.ContentType = "application/json";
		}

		#region IHttpWebRequest Members

		public override string Method
		{
			set { _request.Method = value; }
		}

		public override long ContentLength
		{
			set { _request.ContentLength = value; }
		}

		public new IHttpWebResponse GetResponse()
		{
			try
			{
				return new JsonResponse((HttpWebResponse)_request.GetResponse());
			}
			catch (WebException e)
			{
				if (e.Status == WebExceptionStatus.Timeout)
				{
					throw new TimeoutException("Timed out connecting to server.", e);
				}
				if (e.Status == WebExceptionStatus.KeepAliveFailure)
				{
					throw new RetryException();
				}

				var resp = e.Response as HttpWebResponse;
				if (resp != null && resp.StatusCode == HttpStatusCode.NotFound)
				{
					throw new NotFoundException();
				}
				if (resp != null && resp.StatusCode == HttpStatusCode.BadRequest)
				{
					Stream stream = null;
					try
					{
						stream = resp.GetResponseStream();
						if (stream != null)
						{
							using (var reader = new StreamReader(stream))
							{
								throw new Exception(reader.ReadToEnd(), e);
							}
						}
					}
					catch (WebException)
					{
						// We tried, nothing to see here.
					}
					finally
					{
						if (stream != null)
						{
							stream.Dispose();
						}
					}
				}
				throw;
			}
		}

		public override Stream GetRequestStream()
		{
			return _request.GetRequestStream();
		}

		#endregion
	}
}