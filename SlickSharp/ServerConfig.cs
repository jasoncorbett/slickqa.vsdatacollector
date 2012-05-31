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
using System.Diagnostics;

namespace SlickSharp
{
	public static class ServerConfig
	{
		public static string BaseUrl { get; set; }

		public static string ObjectUri(Type type)
		{
			Debug.Assert(type != null, "type != null");
			if (type == typeof(Build))
			{
				return String.Empty;
			}
			if (type == typeof(Component))
			{
				return String.Empty;
			}
			if (type == typeof(Configuration))
			{
				return String.Empty;
			}
			if (type == typeof(Project))
			{
				return String.Empty;
			}
			if (type == typeof(Release))
			{
				return String.Empty;
			}
			if (type == typeof(Result))
			{
				return String.Empty;
			}
			if (type == typeof(Testcase))
			{
				return String.Empty;
			}
			return String.Empty;
		}
	}
}