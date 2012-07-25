using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using SlickQA.SlickSharp.Attributes;

namespace SlickQA.SlickSharp.Utility
{
	static class UriBuilder
	{
		public static string NormalizePath(object searchObject, string listPath)
		{
			if (searchObject == null)
			{
				return listPath;
			}

			var r = new Regex("{([^/]+)}");
			var matches = r.Matches(listPath);
			foreach (Match m in matches)
			{
				string memberName = m.Groups[1].Captures[0].Value;
				string memberValue = GetMemberValue(searchObject, memberName);
				listPath = listPath.Replace(m.Value, memberValue);
			}
			return listPath;
		}

		private static string GetMemberValue(object searchObject, string memberName)
		{
			FieldInfo field = searchObject.GetType().GetField(memberName);
			PropertyInfo prop = searchObject.GetType().GetProperty(memberName);

			if (field != null)
			{
				return field.GetValue(searchObject).ToString();
			}
			return prop != null ? prop.GetValue(searchObject, null).ToString() : String.Empty;
		}

		public static string SelectRelativeGetPath(object searchObject)
		{
			string getPath = null;
			var type = searchObject.GetType();
			object[] apiList = type.GetCustomAttributes(typeof(GetAttribute), true);
			List<GetAttribute> getApis = apiList.OrderBy(a => ((GetAttribute)a).Index).Select(b => (GetAttribute)b).ToList();
			foreach (GetAttribute item in getApis)
			{
				PropertyInfo property = type.GetProperty(item.PropertyName);
				ConstructorInfo constructor;
				object value;
				if (property != null)
				{
					constructor = property.PropertyType.GetConstructor(Type.EmptyTypes);
					value = property.GetValue(searchObject, null);
				}
				else
				{
					FieldInfo field = type.GetField(item.PropertyName);
					constructor = field.FieldType.GetConstructor(Type.EmptyTypes);

					value = field.GetValue(searchObject);
				}

				if (IsNonDefaultValue(value, constructor))
				{
					getPath = item.ApiPath + "/" + value;
					break;
				}
			}
			return getPath;
		}

		private static bool IsNonDefaultValue(object propVal, ConstructorInfo constructor)
		{
			if (propVal != null)
			{
				if (constructor == null)
				{
					return true;
				}
				if (propVal != constructor.Invoke(new Object[0]))
				{
					return true;
				}
			}
			return false;
		}

		public static Uri RetrieveGetUri<T>(JsonObject<T> searchObject) where T : class, IJsonObject
		{
			var listPath = GetListPath(searchObject);

			string getPath = SelectRelativeGetPath(searchObject);

			string fullGetPath = listPath + "/" + getPath;

			var uri = new Uri(String.Format("{0}/{1}", ServerConfig.BaseUri, fullGetPath));
			return uri;
		}

		public static string GetListPath<T>(JsonObject<T> searchObject) where T : class, IJsonObject
		{
			object[] listAttributes = typeof(T).GetCustomAttributes(typeof(ListApiAttribute), true);
			string listPath = null;

			if (listAttributes.Length != 0)
			{
				listPath = ((ListApiAttribute)listAttributes[0]).Uri;
				listPath = NormalizePath(searchObject, listPath);
			}
			return listPath;
		}

		public static Uri RelativeGetUrl(string relUrl)
		{
			var uri = new Uri(String.Format("{0}/{1}", ServerConfig.BaseUri, relUrl));
			return uri;
		}
	}
}
