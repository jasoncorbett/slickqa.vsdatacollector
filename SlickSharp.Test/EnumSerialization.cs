using System;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlickQA.SlickSharp.Logging;
using SlickQA.SlickSharp.Utility.Json;

namespace SlickQA.SlickSharp.Test
{
	/// <summary>
	/// Summary description for EnumSerialization
	/// </summary>
	[TestClass]
	public class EnumSerialization
	{
		[TestMethod]
		public void RunStatusEnumTranslatesToStringCorrectly()
		{
			var r = new Result
			{
				RunStatus = RunStatus.TO_BE_RUN,
				Status = ResultStatus.BROKEN_TEST
			};

			var buff = StreamConverter<Result>.ConvertToByteBuffer(r);

			var jsonString = Encoding.UTF8.GetString(buff);

			StringAssert.DoesNotMatch(jsonString, new Regex(".*statusstring.*", RegexOptions.IgnoreCase));
			StringAssert.Contains(jsonString, "\"status\":\"BROKEN_TEST\"");
			StringAssert.Contains(jsonString, "\"runstatus\":\"TO_BE_RUN\"");
		}

		[TestMethod]
		public void LogLevelEnumTranslatesToStringCorrectly()
		{
			var l = new LogEntry();
			l.Level = LogLevel.ERROR;

			var buff = StreamConverter<LogEntry>.ConvertToByteBuffer(l);

			var jsonString = Encoding.UTF8.GetString(buff);

			StringAssert.Contains(jsonString, "\"level\":\"ERROR\"");
		}
	}
}
