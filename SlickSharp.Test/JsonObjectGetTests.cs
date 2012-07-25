using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SlickQA.SlickSharp.Utility;

namespace SlickQA.SlickSharp.Test
{
	[TestClass]
	public sealed class JsonObjectGetTests
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
		public void GetList()
		{
			var project1 = JsonStreamConverter<Project>.ReadFromStream(ConvertStringToStream(PROJECT_1));
			var project2 = JsonStreamConverter<Project>.ReadFromStream(ConvertStringToStream(PROJECT_2));
			
			const string PROJECT_LIST = "[" + PROJECT_1 + "," + PROJECT_2 + "]";			
			var listStream = ConvertStringToStream(PROJECT_LIST);

			_mockRequest.Setup(request => request.GetResponse()).Returns(_mockResponse.Object);
			_mockResponse.Setup(response => response.GetResponseStream()).Returns(listStream);

			List<Project> projects = Project.GetList();

			CollectionAssert.Contains(projects, project1);
			CollectionAssert.Contains(projects, project2);
		}

		[TestMethod]
		public void GetExistingItem()
		{
			var project1Stream = ConvertStringToStream(PROJECT_1);
			var expectedProject = JsonStreamConverter<Project>.ReadFromStream(project1Stream);
			project1Stream.Position = 0;

			_mockRequest.Setup(request => request.GetResponse()).Returns(_mockResponse.Object);
			_mockResponse.Setup(response => response.GetResponseStream()).Returns(project1Stream);

			var project = new Project { Name = "Slickij Developer Project" };

			var returnedProject = project.Get();

			Assert.AreEqual(expectedProject, returnedProject);
		}

		[TestMethod]
		public void GetItemThatDoesNotExistWithCreateIfNotFoundFalse()
		{
			_mockRequest.Setup(request => request.GetResponse()).Throws<NotFoundException>();

			var project = new Project { Name = "Slickij Developer Project" };

			var returnedProject = project.Get();

			Assert.IsNull(returnedProject);
		}


		[TestMethod]
		public void GetItemThatDoesNotExistWithCreateIfNotFoundTrue()
		{
			var project1Stream = ConvertStringToStream(PROJECT_1);
			var expectedProject = JsonStreamConverter<Project>.ReadFromStream(project1Stream);
			project1Stream.Position = 0;


			var requestStream = new MemoryStream();
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


			var project = new Project { Name = "Slickij Developer Project" };

			var returnedProject = project.Get(true);

			Assert.AreEqual(expectedProject, returnedProject);
		}

		private static MemoryStream ConvertStringToStream(string stringToConvert)
		{
			var listStream = new MemoryStream();
			byte[] data = Encoding.Unicode.GetBytes(stringToConvert);
			listStream.Write(data, 0, data.Length);
			listStream.Position = 0;
			return listStream;
		}
	}
}
