using System;
using System.Net;

namespace SlickQA.SlickSharp
{
	public static class RequestFactory
	{
		static RequestFactory()
		{
			Factory = uri =>
			          {
			          	var request = WebRequest.Create(uri);
			          	return new JsonRequest(request);
			          };
		}

		public static Func<Uri, IHttpWebRequest> Factory { private get; set; }

		public static IHttpWebRequest Create(Uri uri)
		{
			return Factory(uri);
		}
	}
}