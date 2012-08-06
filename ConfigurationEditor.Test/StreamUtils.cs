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

using System.IO;
using System.Text;

namespace SlickQA.DataCollector.ConfigurationEditor.Test
{
	class StreamUtils
	{
		public static MemoryStream ConvertStringToStream(string stringToConvert)
		{
			var listStream = new MemoryStream();
			byte[] data = Encoding.Unicode.GetBytes(stringToConvert);
			listStream.Write(data, 0, data.Length);
			listStream.Position = 0;
			return listStream;
		}
	}
}
