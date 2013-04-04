using System;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlickQA.SlickSharp.Utility.Json;

namespace SlickQA.SlickSharp.Test
{
	[TestClass]
	public class DateTimeSerialization
	{
		[TestMethod]
		public void UnixEpochUTCDateTimeSerializesToISO8601String()
		{
			
			var r = new Result
			{
				Recorded = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc),
			};

			var buff = StreamConverter<Result>.ConvertToByteBuffer(r);

			var jsonString = Encoding.UTF8.GetString(buff);

			StringAssert.Contains(jsonString, "\"recorded\":\"1970-01-01T00:00:00.0000000Z\"");
		}
	}
}
