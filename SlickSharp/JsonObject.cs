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
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using SlickQA.SlickSharp.Utility.Json;
using SlickQA.SlickSharp.Web;
using UriBuilder = SlickQA.SlickSharp.Web.UriBuilder;

namespace SlickQA.SlickSharp
{
    using JetBrains.Annotations;

    [DataContract]
	public abstract class JsonObject<T> where T : class, IJsonObject
	{
		private const int MAX_ATTEMPTS = 3;

		//TODO: Test with base Exception thrown
        [PublicAPI]
		public void Get(bool createIfNotFound = false, int attempts = 0)
		{
			Uri uri = UriBuilder.RetrieveGetUri(this);
			IHttpWebRequest httpWebRequest = RequestFactory.Create(uri);
			httpWebRequest.Method = WebRequestMethods.Http.Get;

			// at times we don't get a good response back from slick when the GET should succeed
			//  because of this we try 3 times, just to be sure
			Exception ex = null;
			while (attempts <= 3)
			{
				try
				{
					T temp = StreamConverter<T>.ReadResponse(httpWebRequest);
					ApplyChanges(temp);
					return;
				}
				catch (NotFoundException)
				{
					if (createIfNotFound)
					{
						Post();
					}
					return;
				}
				catch (RetryException)
				{
					Get(createIfNotFound, attempts);
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

        [PublicAPI]
        public void FindOne(bool createIfNotFound = false, int attempts = 0)
        {
            Uri uri = UriBuilder.RetrieveFindUri(this);
            IHttpWebRequest httpWebRequest = RequestFactory.Create(uri);
            httpWebRequest.Method = WebRequestMethods.Http.Get;

            // at times we don't get a good response back from slick when the GET should succeed
            //  because of this we try 3 times, just to be sure
            Exception ex = null;
            while (attempts <= 3)
            {
                try
                {
                    List<T> temp = StreamConverter<T>.ReadListResponse(httpWebRequest);
                    if (temp.Count > 0)
                    {
                        ApplyChanges(temp[0]);
                        return;
                    }
                    Post();
                }
                catch (NotFoundException)
                {
                    if (createIfNotFound)
                    {
                        Post();
                    }
                    return;
                }
                catch (RetryException)
                {
                    FindOne(createIfNotFound, attempts);
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

        [PublicAPI]
		public static List<T> GetList()
		{
			Uri uri = UriBuilder.FullUri(UriBuilder.GetListPath<T>(null));

			return RetrieiveList(uri);
		}

        [PublicAPI]
		public static List<T> GetList(string relUrl)
		{
			try
			{
				Uri uri = UriBuilder.FullUri(relUrl);
				return RetrieiveList(uri);
			}
			catch
			{
				return new List<T>();
			}
		}

		private static List<T> RetrieiveList(Uri uri, int attempts = 0)
		{
			try
			{
				IHttpWebRequest httpWebRequest = RequestFactory.Create(uri);
				httpWebRequest.Method = WebRequestMethods.Http.Get;

				return StreamConverter<T>.ReadListResponse(httpWebRequest);
			}
			catch (RetryException)
			{
				if (attempts > MAX_ATTEMPTS)
				{
					throw;
				}

				attempts++;
				return RetrieiveList(uri, attempts);
			}
		}

        [PublicAPI]
		public void Post()
		{
			Uri uri = UriBuilder.FullUri(UriBuilder.GetListPath(this));

			Post(uri);
		}

		private void Post(Uri uri, int attempts = 0)
		{
			try
			{
				IHttpWebRequest httpWebRequest = RequestFactory.Create(uri);
				httpWebRequest.Method = WebRequestMethods.Http.Post;

				StreamConverter<T>.WriteRequestStream(httpWebRequest, this);

				T temp = StreamConverter<T>.ReadResponse(httpWebRequest);
				ApplyChanges(temp);

			}
			catch (RetryException)
			{
				if (attempts > MAX_ATTEMPTS)
				{
					throw;
				}
				attempts++;
				Post(uri, attempts);
			}
		}

        [PublicAPI]
		public void Put()
		{
			Uri uri = UriBuilder.RetrieveGetUri(this);
			Put(uri);
		}

		private void Put(Uri uri, int attempts = 0)
		{
			try
			{
				IHttpWebRequest httpWebRequest = RequestFactory.Create(uri);
				httpWebRequest.Method = WebRequestMethods.Http.Put;

				StreamConverter<T>.WriteRequestStream(httpWebRequest, this);
				T temp = StreamConverter<T>.ReadResponse(httpWebRequest);

				ApplyChanges(temp);

			}
			catch (RetryException)
			{
				if (attempts > MAX_ATTEMPTS)
				{
					throw;
				}
				attempts++;
				Put(uri, attempts);
			}
		}

	    protected void ApplyChanges(T temp)
		{
			Type type = typeof(T);

			FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);

			foreach (FieldInfo field in fields)
			{
				object val = field.GetValue(temp);
				field.SetValue(this, val);
			}
		}

        [PublicAPI]
		public void Delete()
		{
			throw new NotImplementedException(
				"This is included for future rest completeness, we currently have no use for delete.");
		}
	}
}