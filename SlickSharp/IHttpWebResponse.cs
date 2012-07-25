using System;
using System.IO;
using System.Net;

namespace SlickQA.SlickSharp
{
	public interface IHttpWebResponse : IDisposable
	{
		Stream GetResponseStream();
		WebResponse InnerResponse { get; }
	}
}