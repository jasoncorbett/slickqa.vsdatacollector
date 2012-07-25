using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SlickQA.SlickSharp.Test
{
	static class StreamConversion
	{
		internal static string ToString(Stream stream)
		{
			var oldPos = stream.Position;
			stream.Seek(0, SeekOrigin.Begin);
			var r = new StreamReader(stream);
			var retval = r.ReadToEnd();
			stream.Seek(oldPos, SeekOrigin.Begin);
			return retval;
		}

		internal static MemoryStream FromString(string stringToConvert)
		{
			MemoryStream retStream = null;
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
