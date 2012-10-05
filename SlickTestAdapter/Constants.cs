using System;

namespace SlickQA.TestAdapter
{
	public static class Constants
	{
		internal static readonly Uri ExecutorUri = new Uri(EXECUTOR_URI_STRING);
		public const string EXECUTOR_URI_STRING = "executor://slicktestadapter";
		public const string FILE_EXTENSION = ".slicktest";
	}
}