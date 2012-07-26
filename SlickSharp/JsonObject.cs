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
using System.Diagnostics;
using System.Runtime.Serialization;
using SlickQA.SlickSharp.Utility.Json;
using SlickQA.SlickSharp.Web;
using UriBuilder = SlickQA.SlickSharp.Web.UriBuilder;

namespace SlickQA.SlickSharp
{
	[DataContract]
	public abstract class JsonObject<T> where T : class, IJsonObject
	{
		public T Get(bool createIfNotFound = false)
		{
			var uri = UriBuilder.RetrieveGetUri(this);
			IHttpWebRequest httpWebRequest = RequestFactory.Create(uri);
			httpWebRequest.Method = "GET";

			// at times we don't get a good response back from slick when the GET should succeed
			//  because of this we try 3 times, just to be sure
			int attempts = 0;
			Exception ex = null;
			while (attempts <= 3)
			{
				try
				{
					return StreamConverter<T>.ReadResponse(httpWebRequest);
				}
				catch (NotFoundException)
				{
					return createIfNotFound ? Post() : null;
				}
				catch (Exception e)
				{
					ex = e;
					attempts++;
				}
			}
			Debug.Assert(ex != null, "ex != null");
			throw ex;
		}

		public static List<T> GetList()
		{
			var listPath = UriBuilder.GetListPath<T>(null);

			if (String.IsNullOrWhiteSpace(listPath))
			{
				return new List<T>();
			}

			var uri = new Uri(string.Format("{0}/{1}", ServerConfig.BaseUri, listPath));
			return RetrieiveList(uri);
		}

		public static List<T> GetList(string relUrl)
		{
			try
			{
				var uri = UriBuilder.FullUri(relUrl);
				return RetrieiveList(uri);
			}
			catch
			{
				return new List<T>();
			}
		}

		private static List<T> RetrieiveList(Uri uri)
		{
			IHttpWebRequest httpWebRequest = RequestFactory.Create(uri);
			httpWebRequest.Method = "GET";

			return StreamConverter<T>.ReadListResponse(httpWebRequest);
		}

		public T Post()
		{
			var uri = UriBuilder.FullUri(UriBuilder.GetListPath(this));

			IHttpWebRequest httpWebRequest = RequestFactory.Create(uri);
			httpWebRequest.Method = "POST";

			StreamConverter<T>.WriteRequestStream(httpWebRequest, this);

			return StreamConverter<T>.ReadResponse(httpWebRequest);
		}

		public void Put()
		{
		}

		public void Delete()
		{
		}
	}
}