using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
