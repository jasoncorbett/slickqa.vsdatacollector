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

using System.IO;
using System.Text;

namespace SlickQA.SlickSharp.Test
{
	static class StreamConversion
	{
		internal static MemoryStream FromString(string stringToConvert)
		{
			MemoryStream retStream;
			MemoryStream tempStream = null;
			try
			{
				tempStream = new MemoryStream();
				byte[] data = Encoding.Unicode.GetBytes(stringToConvert);
				tempStream.Write(data, 0, data.Length);
				tempStream.Position = 0;
				retStream = tempStream;
				tempStream = null;
			}
			finally
			{
				if (tempStream != null)
				{
					tempStream.Close();
				}
			}
			return retStream;
		}
	}
}
