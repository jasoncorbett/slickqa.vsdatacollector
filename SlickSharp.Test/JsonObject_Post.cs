using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SlickQA.SlickSharp.Utility.Json;
using SlickQA.SlickSharp.Web;

namespace SlickQA.SlickSharp.Test
{
	#region ReSharper Directives
	// ReSharper disable InconsistentNaming
	#endregion

	[TestClass]
	public sealed class JsonObject_Post
	{
		[DataContract]
		private class DummyJsonObject : JsonObject<DummyJsonObject>, IJsonObject
		{
			[DataMember(Name = "name")]
			public String Name;
		}

		private Mock<IHttpWebRequest> _mockRequest;
		private Mock<IHttpWebResponse> _mockResponse;

		[TestInitialize]
		public void Setup()
		{
			ServerConfig.Scheme = Uri.UriSchemeHttp;
			ServerConfig.SlickHost = "example.com";
			ServerConfig.Port = 8080;
			ServerConfig.SitePath = "slick";

			_mockRequest = new Mock<IHttpWebRequest>();
			_mockResponse = new Mock<IHttpWebResponse>();

			RequestFactory.Factory = uri => uri == new Uri(ServerConfig.BaseUri + "/projects") ? _mockRequest.Object : null;
		}
		[TestMethod]
		[ExpectedException(typeof(MissingPostUriException))]
		public void With_no_list_api()
		{
			var d = new DummyJsonObject {Name = "Foo Bar Baz"};
			d.Post();
		}
		const string PROJECT_JSON = "  {"
										+ "    \"name\":\"Foo Bar Baz\","
										+ "    \"id\":\"4ffc9e3ee4b097a5f43e5d27\","
										+ "    \"attributes\":{},"
										+ "    \"description\":null,"
										+ "    \"configuration\":{},"
										+ "    \"releases\":[],"
										+ "    \"lastUpdated\":1343081801244,"
										+ "    \"automationTools\":[],"
										+ "    \"defaultRelease\":\"\","
										+ "    \"tags\":[],"
										+ "    \"components\":[],"
										+ "    \"datadrivenProperties\":[],"
										+ "    \"extensions\":[],"
										+ "    \"defaultBuildName\":\"\""
										+ "  }";
		[TestMethod]
		public void Returns_correct_object()
		{
			var responseStream = StreamConversion.FromString(PROJECT_JSON);
			var expectedProject = StreamConverter<Project>.ReadFromStream(responseStream);
			responseStream.Position = 0;

			Project retrievedProject;
			using (var memStream = new MemoryStream())
			{
				_mockRequest.Setup(request => request.GetRequestStream()).Returns(memStream);
				_mockRequest.Setup(request => request.GetResponse()).Returns(() => _mockResponse.Object);
				_mockResponse.Setup(response => response.GetResponseStream()).Returns(responseStream);

				var p = new Project { Name = "Foo Bar Baz" };
				retrievedProject = p.Post(); 
			}

			Assert.AreEqual(expectedProject, retrievedProject);
		}


		//public T Post()
		//{
		//    var uri = new Uri(string.Format("{0}/{1}", ServerConfig.BaseUri, listPath));
		//    IHttpWebRequest httpWebRequest = RequestFactory.Create(uri);
		//    httpWebRequest.Method = "POST";

		//    StreamConverter<T>.WriteRequestStream(httpWebRequest, this);

		//    return StreamConverter<T>.ReadResponse(httpWebRequest);
		//}
		#region ReSharper Directives
		// ReSharper restore InconsistentNaming
		#endregion
	}
}
