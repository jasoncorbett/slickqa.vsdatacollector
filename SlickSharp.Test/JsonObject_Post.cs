/* Copyright 2012 AccessData Group, LLC.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.IO;
using System.Runtime.Serialization;
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
		private sealed class DummyJsonObject : JsonObject<DummyJsonObject>, IJsonObject
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
	}
	#region ReSharper Directives
	// ReSharper restore InconsistentNaming
	#endregion
}
