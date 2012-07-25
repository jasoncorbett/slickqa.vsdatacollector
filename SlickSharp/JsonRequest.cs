using System.IO;
using System.Net;

namespace SlickQA.SlickSharp
{
	public sealed class JsonRequest : WebRequest, IHttpWebRequest
	{
		private readonly WebRequest _request;

		public JsonRequest(WebRequest webRequest)
		{
			_request = webRequest;
			_request.ContentType = "application/json";
		}

		#region IHttpWebRequest Members

		public override string Method
		{
			get { return _request.Method; }
			set { _request.Method = value; }
		}

		public override long ContentLength
		{
			get { return _request.ContentLength; }
			set { _request.ContentLength = value; }
		}

		public new IHttpWebResponse GetResponse()
		{
			try
			{
				return new JsonResponse((HttpWebResponse)_request.GetResponse());
			}
			catch (WebException e)
			{
				var resp = e.Response as HttpWebResponse;
				if (resp != null && resp.StatusCode == HttpStatusCode.NotFound)
				{
					throw new NotFoundException();
				}
				throw;
			}
		}

		public override Stream GetRequestStream()
		{
			return _request.GetRequestStream();
		}

		#endregion
	}
}