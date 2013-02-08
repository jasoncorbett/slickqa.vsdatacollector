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
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using SlickQA.SlickSharp.Attributes;

namespace SlickQA.SlickSharp.Web
{
	internal static class UriBuilder
	{
		public static string NormalizePath(object searchObject, string listPath)
		{
			if (searchObject == null)
			{
				return listPath;
			}

			var r = new Regex("{([^/]+)}");
			MatchCollection matches = r.Matches(listPath);
			foreach (Match m in matches)
			{
				string memberName = m.Groups[1].Captures[0].Value;
				string memberValue = GetMemberValue(searchObject, memberName);
				listPath = listPath.Replace(m.Value, memberValue);
			}
			return listPath;
		}

		public static string GetMemberValue(object searchObject, string memberName)
		{
			FieldInfo field = searchObject.GetType().GetField(memberName);
			PropertyInfo prop = searchObject.GetType().GetProperty(memberName);

			if (field != null)
			{
				return field.GetValue(searchObject).ToString();
			}
			return prop != null ? prop.GetValue(searchObject, null).ToString() : String.Empty;
		}

		public static string SelectGetApi(object searchObject)
		{
			string getPath = null;
			Type type = searchObject.GetType();
			object[] apiList = type.GetCustomAttributes(typeof(ItemApiPathAttribute), true);
			List<ItemApiPathAttribute> getApis =
				apiList.OrderBy(a => ((ItemApiPathAttribute)a).Index).Select(b => (ItemApiPathAttribute)b).ToList();
			foreach (ItemApiPathAttribute item in getApis)
			{
			    var value = GetValueByName(searchObject, type, item.PropertyName);

			    if (IsNonDefaultValue(value))
			    {
			        if (String.IsNullOrWhiteSpace(item.ApiPath))
			        {
			            getPath = HttpUtility.UrlPathEncode(value.ToString());
			        }
			        else
			        {
			            getPath = item.ApiPath + "/" + HttpUtility.UrlPathEncode(value.ToString());
			        }
			        break;
			    }
			}


		    return getPath;
		}

	    private static object GetValueByName(object searchObject, Type type, string propertyName)
	    {
	        PropertyInfo property = type.GetProperty(propertyName);
	        object value;
	        if (property != null)
	        {
	            value = property.GetValue(searchObject, null);
	        }
	        else
	        {
	            FieldInfo field = type.GetField(propertyName);
	            value = field.GetValue(searchObject);
	        }
	        return value;
	    }

        public static String GetFindPath(object searchObject)
        {
            string uri = null;
			Type type = searchObject.GetType();
            var queryApis = type.GetCustomAttributes(typeof(ItemQueryParameterAttribute), true).Cast<ItemQueryParameterAttribute>().ToList();
            if (queryApis.Count > 0)
            {
                var queryParams = new StringBuilder("?");
                foreach (var queryApi in queryApis)
                {
                    var value = GetValueByName(searchObject, type, queryApi.PropertyName);
                    if (IsNonDefaultValue(value))
                    {
                        queryParams.AppendFormat("{0}={1}&", queryApi.QueryParamName,
                                                    HttpUtility.UrlPathEncode(value.ToString()));
                    }
                }
                queryParams.Replace("&", String.Empty, queryParams.Length - 1, 1);
                uri = queryParams.ToString();
            }
            return uri;
        }

	    internal static bool IsNonDefaultValue(object propVal)
		{
			if (propVal != null)
			{
				Type type = propVal.GetType();
				if (!type.IsValueType)
				{
					return true;
				}

				object defaultInstance = Activator.CreateInstance(type);
				if (!propVal.Equals(defaultInstance))
				{
					return true;
				}
			}
			return false;
		}

		public static Uri RetrieveGetUri<T>(JsonObject<T> searchObject) where T : class, IJsonObject
		{
			string listPath = GetListPath(searchObject);

			string getPath = SelectGetApi(searchObject);

			return FullUri(listPath + "/" + getPath);
		}

        public static Uri RetrieveFindUri<T>(JsonObject<T> searchObject) where T : class, IJsonObject
        {
            var listPath = GetListPath(searchObject);
            var findPath = GetFindPath(searchObject);
            return FullUri(listPath + findPath);
        }

		public static string GetListPath<T>(JsonObject<T> searchObject) where T : class, IJsonObject
		{
			object[] listAttributes = typeof(T).GetCustomAttributes(typeof(CollectionApiPathAttribute), true);
			if (listAttributes.Length == 0)
			{
				throw new MissingPostUriException();
			}

			string listPath = ((CollectionApiPathAttribute)listAttributes[0]).Uri;
			return NormalizePath(searchObject, listPath);
		}

		public static Uri FullUri(string relUrl)
		{
			return new Uri(String.Format("{0}/{1}", ServerConfig.BaseUri, relUrl));
		}
	}
}