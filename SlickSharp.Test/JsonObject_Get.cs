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
using System.Collections.Generic;
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
	public sealed class JsonObject_Get
	{
		private Mock<IHttpWebRequest> _mockRequest;
		private Mock<IHttpWebResponse> _mockResponse;

		const string PROJECT_1 = "  {"
										+ "    \"name\":\"Slickij Developer Project\","
										+ "    \"id\":\"4ffd9866e4b097a5f43e5e2b\","
										+ "    \"attributes\":{},"
										+ "    \"description\":\"A Project to be used by slickij developers to test features.\","
										+ "    \"configuration\":{"
										+ "      \"name\":\"Slickij Developer Project's Project Configuration\","
										+ "      \"id\":\"4ffd9866e4b097a5f43e5e29\","
										+ "      \"configurationData\":{},"
										+ "      \"filename\":null,"
										+ "      \"configurationType\":\"PROJECT\""
										+ "    },"
										+ "    \"releases\":["
										+ "      {"
										+ "        \"name\":\"1.0.0\","
										+ "        \"id\":\"4ffd9866e4b097a5f43e5e2f\","
										+ "        \"target\":1304269945716,"
										+ "        \"builds\":["
										+ "          {"
										+ "            \"name\":\"SNAPSHOT\","
										+ "            \"id\":\"4ffd9866e4b097a5f43e5e30\","
										+ "            \"built\":1342019686934"
										+ "          }"
										+ "        ],"
										+ "        \"defaultBuild\":\"4ffd9866e4b097a5f43e5e30\""
										+ "      }"
										+ "    ],"
										+ "    \"lastUpdated\":1342019686993,"
										+ "    \"automationTools\":[],"
										+ "    \"defaultRelease\":\"4ffd9866e4b097a5f43e5e2f\","
										+ "    \"tags\":[],"
										+ "    \"components\":[],"
										+ "    \"datadrivenProperties\":[],"
										+ "    \"extensions\":[],"
										+ "    \"defaultBuildName\":\"1.0.0 Build SNAPSHOT\""
										+ "  }";
		const string PROJECT_2 = "  {"
										+ "    \"name\":\"Foo Project\","
										+ "    \"id\":\"4ffc9e3ee4b097a5f43e5d27\","
										+ "    \"attributes\":{},"
										+ "    \"description\":null,"
										+ "    \"configuration\":{"
										+ "      \"name\":\"FTK's Project Configuration\","
										+ "      \"id\":\"4ffc9e3ee4b097a5f43e5d26\","
										+ "      \"configurationData\":{},"
										+ "      \"filename\":null,"
										+ "      \"configurationType\":\"PROJECT\""
										+ "    },"
										+ "    \"releases\":["
										+ "      {"
										+ "        \"name\":\"4.1.0\","
										+ "        \"id\":\"4ffc9e3ee4b097a5f43e5d28\","
										+ "        \"target\":1341955646690,"
										+ "        \"builds\":["
										+ "          {"
										+ "            \"name\":\"76\","
										+ "            \"id\":\"4ffc9e3ee4b097a5f43e5d2a\","
										+ "            \"built\":1341955646758"
										+ "          },"
										+ "        ],"
										+ "        \"defaultBuild\":\"4ffc9e3ee4b097a5f43e5d2a\""
										+ "      },"
										+ "    ],"
										+ "    \"lastUpdated\":1343081801244,"
										+ "    \"automationTools\":[],"
										+ "    \"defaultRelease\":\"500440a4e4b0aea40e783409\","
										+ "    \"tags\":[],"
										+ "    \"components\":[],"
										+ "    \"datadrivenProperties\":[],"
										+ "    \"extensions\":[],"
										+ "    \"defaultBuildName\":\"4.1.0 Build 76\""
										+ "  }";

		[TestInitialize]
		public void Setup()
		{
			ServerConfig.Scheme = Uri.UriSchemeHttp;
			ServerConfig.SlickHost = "example.com";
			ServerConfig.Port = 8080;
			ServerConfig.SitePath = "slick";

			_mockRequest = new Mock<IHttpWebRequest>();
			_mockResponse = new Mock<IHttpWebResponse>();

			RequestFactory.Factory = uri =>
			{
				if (uri == new Uri(ServerConfig.BaseUri + "/projects/byname/Slickij Developer Project")
					|| uri == new Uri(ServerConfig.BaseUri + "/projects"))
				{
					return _mockRequest.Object;
				}
				return null;
			};
		}

		[TestMethod]
		public void List()
		{
			var project1 = StreamConverter<Project>.ReadFromStream(StreamConversion.FromString(PROJECT_1));
			var project2 = StreamConverter<Project>.ReadFromStream(StreamConversion.FromString(PROJECT_2));
			
			const string PROJECT_LIST = "[" + PROJECT_1 + "," + PROJECT_2 + "]";			
			var listStream = StreamConversion.FromString(PROJECT_LIST);

			_mockRequest.Setup(request => request.GetResponse()).Returns(_mockResponse.Object);
			_mockResponse.Setup(response => response.GetResponseStream()).Returns(listStream);

			List<Project> projects = Project.GetList();

			CollectionAssert.Contains(projects, project1);
			CollectionAssert.Contains(projects, project2);
		}

		[TestMethod]
		public void Existing_Item()
		{
			var project1Stream = StreamConversion.FromString(PROJECT_1);
			var expectedProject = StreamConverter<Project>.ReadFromStream(project1Stream);
			project1Stream.Position = 0;

			_mockRequest.Setup(request => request.GetResponse()).Returns(_mockResponse.Object);
			_mockResponse.Setup(response => response.GetResponseStream()).Returns(project1Stream);

			var project = new Project { Name = "Slickij Developer Project" };

			project.Get();

			Assert.AreEqual(expectedProject.Id, project.Id);
		}

		[TestMethod]
		public void Item_that_does_not_exist_with_createIfNotFound_false()
		{
			_mockRequest.Setup(request => request.GetResponse()).Throws<NotFoundException>();

			var project = new Project { Name = "Slickij Developer Project" };

			project.Get();

			Assert.IsNull(project.Id);
		}


		[TestMethod]
		public void Item_that_does_not_exist_with_createIfNotFound_true()
		{
			var project1Stream = StreamConversion.FromString(PROJECT_1);
			var expectedProject = StreamConverter<Project>.ReadFromStream(project1Stream);
			project1Stream.Position = 0;

			Project project;
			using (var requestStream = new MemoryStream())
			{
				_mockRequest.Setup(request => request.GetRequestStream()).Returns(requestStream);

				bool firstTimeCalled = true;
				_mockRequest.Setup(request => request.GetResponse())
					.Returns(() => _mockResponse.Object)
					.Callback(() =>
					{
						if (firstTimeCalled)
						{
							firstTimeCalled = false;
							throw new NotFoundException();
						}
					});
				_mockResponse.Setup(response => response.GetResponseStream()).Returns(project1Stream);


				project = new Project { Name = "Slickij Developer Project" };

				project.Get(true);
				
			}

			Assert.AreEqual(expectedProject.Id, project.Id);
			Assert.AreEqual(expectedProject.Name, project.Name);
			Assert.AreEqual(expectedProject.LastUpdated, project.LastUpdated);
		}
		#region ReSharper Directives
		// ReSharper restore InconsistentNaming
		#endregion
	}
}
