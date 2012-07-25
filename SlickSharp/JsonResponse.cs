using System.IO;
using System.Net;

namespace SlickQA.SlickSharp
{
	public sealed class JsonResponse : IHttpWebResponse
	{
		private readonly HttpWebResponse _response;

		public JsonResponse(HttpWebResponse webResponse)
		{
			_response = webResponse;
		}

		#region IHttpWebResponse Members

		public void Dispose()
		{
			_response.Close();
		}

		public Stream GetResponseStream()
		{
			return _response.GetResponseStream();
		}

		public WebResponse InnerResponse
		{
			get { return _response; }
		}

		#endregion
	}
}