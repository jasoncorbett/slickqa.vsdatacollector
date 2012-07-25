using System.IO;

namespace SlickQA.SlickSharp
{
	public interface IHttpWebRequest
	{
		string Method { get; set; }
		long ContentLength { get; set; }
		IHttpWebResponse GetResponse();

		Stream GetRequestStream();
	}
}