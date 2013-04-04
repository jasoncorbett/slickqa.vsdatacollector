// Copyright 2012 AccessData Group, LLC.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//  http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SlickQA.SlickSharp.ObjectReferences;
using SlickQA.SlickSharp.Web;

namespace SlickQA.SlickSharp.Test
{
	#region ReSharper Directives

	// ReSharper disable InconsistentNaming

	#endregion

	[TestClass]
	public sealed class ConvertFromReference
	{
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

			_mockRequest.Setup(request => request.GetResponse()).Returns(_mockResponse.Object);
		}

		[TestMethod]
		public void ProjectReference_has_an_implicit_converstion_to_Project()
		{
			const string PROJECT =
				"{\"name\":\"Foo Project\",\"id\":\"0123456789abcdef\",\"attributes\":{},\"description\":\"Blah Blah Blah\",\"configuration\":{},\"releases\":[],\"lastUpdated\":123456789,\"automationTools\":[],\"defaultRelease\":\"\",\"tags\":[\"Blah Tag\"],\"components\":[],\"datadrivenProperties\":[],\"extensions\":[],\"defaultBuildName\":\"\"}";

			MemoryStream stream = StreamConversion.FromString(PROJECT);
			stream.Position = 0;

			_mockResponse.Setup(response => response.GetResponseStream()).Returns(stream);

			RequestFactory.Factory =
				uri => uri == new Uri(ServerConfig.BaseUri + "/projects/0123456789abcdef") ? _mockRequest.Object : null;

			var pr = new ProjectReference
			{
				Id = "0123456789abcdef",
				Name = "Foo Project"
			};

			var expected = new Project
			{
				Id = "0123456789abcdef",
				Name = "Foo Project",
				Description = "Blah Blah Blah",
				LastUpdated = 123456789
			};
			expected.Tags.Add("Blah Tag");

			Project actual = pr;

			Assert.AreEqual(expected.Name, actual.Name);
			Assert.AreEqual(expected.Description, actual.Description);
			Assert.AreEqual(expected.LastUpdated, actual.LastUpdated);
			Assert.AreEqual(expected.Tags[0], actual.Tags[0]);
		}

		[TestMethod]
		public void ConfigurationReference_has_an_implicit_converstion_to_Configuration()
		{
			const string CONFIGURATION =
				"{\"configurationData\":{},\"configurationType\":\"PROJECT\",\"name\":\"Foo\",\"id\":\"01234567890abcdef\",\"filename\":null}";
			MemoryStream stream = StreamConversion.FromString(CONFIGURATION);
			stream.Position = 0;

			_mockResponse.Setup(response => response.GetResponseStream()).Returns(stream);

			RequestFactory.Factory =
				uri => uri == new Uri(ServerConfig.BaseUri + "/configurations/01234567890abcdef") ? _mockRequest.Object : null;

			var cr = new ConfigurationReference {Name = "Foo", ConfigId = "01234567890abcdef", FileName = String.Empty};

			var expected = new Configuration
			               {
			               	Id = "01234567890abcdef",
			               	Name = "Foo",
			               	ConfigurationType = ConfigurationType.PROJECT.ToString()
			               };
			Configuration actual = cr;

			Assert.AreEqual(expected.Id, actual.Id);
			Assert.AreEqual(expected.Name, actual.Name);
			Assert.AreEqual(expected.Filename, actual.Filename);
			Assert.AreEqual(expected.ConfigurationType, actual.ConfigurationType);
		}

		[TestMethod]
		public void ResultReference_has_an_implicit_converstion_to_Result()
		{
			const string RESULT =
				"{\"configurationOverride\":null,\"files\":[],\"history\":null,\"recorded\":\"1970-01-01T00:00:00.0000000Z\",\"runlength\":0,\"runstatus\":\"FINISHED\",\"testcase\":{},\"testrun\":null,\"project\":{},\"build\":{},\"release\":{},\"component\":{},\"id\":\"0123456789abcdef\",\"attributes\":null,\"extensions\":null,\"log\":null,\"reason\":\"Unit Test\",\"status\":\"CANCELLED\",\"config\":null,\"hostname\":\"blah.example.com\"}";

			MemoryStream stream = StreamConversion.FromString(RESULT);
			stream.Position = 0;

			_mockResponse.Setup(response => response.GetResponseStream()).Returns(stream);

			RequestFactory.Factory =
				uri => uri == new Uri(ServerConfig.BaseUri + "/results/0123456789abcdef") ? _mockRequest.Object : null;

			var rr = new ResultReference
			         {
			         	Id = "0123456789abcdef",
						Recorded = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc),
						ResultStatus = ResultStatus.CANCELLED
			         };

			var expected = new Result
			               {
			               	Id = "0123456789abcdef",
			               	Hostname = "blah.example.com",
			               	Recorded = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc),
			               	Status = ResultStatus.CANCELLED,
			               	Reason = "Unit Test"
			               };

			Result actual = rr;

			Assert.AreEqual(expected.Id, actual.Id);
			Assert.AreEqual(expected.Hostname, actual.Hostname);
			Assert.AreEqual(expected.Recorded, actual.Recorded);
			Assert.AreEqual(expected.Status, actual.Status);
			Assert.AreEqual(expected.Reason, actual.Reason);
		}

		[TestMethod]
		public void TestCaseReference_has_an_implicit_converstion_to_TestCase()
		{
			const string TEST_CASE =
				"{\"tags\":null,\"project\":{},\"purpose\":\"Regression\",\"requirements\":null,\"steps\":null,\"stabilityRating\":0,\"dataDriven\":null,\"author\":null,\"automated\":true,\"automationConfiguration\":null,\"automationId\":\"blah.blah.boo\",\"automationKey\":\"foo.bar.bazTest\",\"automationPriority\":50,\"automationTool\":null,\"component\":{},\"deleted\":false,\"name\":\"Dummy Test Case\",\"id\":\"0123456789abcdef\",\"attributes\":null,\"extensions\":null}";

			MemoryStream stream = StreamConversion.FromString(TEST_CASE);
			stream.Position = 0;

			
			_mockResponse.Setup(response => response.GetResponseStream()).Returns(stream);

			RequestFactory.Factory =
				uri => uri == new Uri(ServerConfig.BaseUri + "/testcases/0123456789abcdef") ? _mockRequest.Object : null;

			var tcr = new TestcaseReference
			          {
			          	Id = "0123456789abcdef",
			          	AutomationId = "blah.blah.boo",
			          	AutomationKey = "foo.bar.bazTest",
			          	Name = "Dummy Test Case"
			          };

			var expected = new Testcase
			               {
			               	Id = "0123456789abcdef",
			               	AutomationId = "blah.blah.boo",
			               	AutomationKey = "foo.bar.bazTest",
			               	Name = "Dummy Test Case",
			               	IsDeleted = false,
			               	Purpose = "Regression"
			               };

			Testcase actual = tcr;

			Assert.AreEqual(expected.Id, actual.Id);
			Assert.AreEqual(expected.AutomationId, actual.AutomationId);
			Assert.AreEqual(expected.AutomationKey, actual.AutomationKey);
			Assert.AreEqual(expected.Name, actual.Name);
			Assert.AreEqual(expected.IsDeleted, actual.IsDeleted);
			Assert.AreEqual(expected.Purpose, actual.Purpose);
		}

		[TestMethod]
		public void TestRunReference_has_an_implicit_converstion_to_TestRun()
		{
			const string TEST_RUN =
				"{\"project\":{},\"summary\":{},\"runtimeOptions\":null,\"testplanId\":\"fedcba9876543210\",\"dateCreated\":1344037251844,\"build\":{},\"release\":{},\"testplan\":{},\"name\":\"Bart Test Run\",\"id\":\"0123456789abcdef\",\"extensions\":[],\"config\":{}}";

			MemoryStream stream = StreamConversion.FromString(TEST_RUN);
			stream.Position = 0;

			_mockResponse.Setup(response => response.GetResponseStream()).Returns(stream);

			RequestFactory.Factory =
				uri => uri == new Uri(ServerConfig.BaseUri + "/testruns/0123456789abcdef") ? _mockRequest.Object : null;

			var trr = new TestRunReference {TestRunId = "0123456789abcdef", Name = "Bart Test Run"};

			var expected = new TestRun {Id = "0123456789abcdef", Name = "Bart Test Run", TestPlanId = "fedcba9876543210"};

			TestRun actual = trr;

			Assert.AreEqual(expected.Id, actual.Id);
			Assert.AreEqual(expected.Name, actual.Name);
			Assert.AreEqual(expected.TestPlanId, actual.TestPlanId);
		}

		//TODO: Finish These tests
		//[TestMethod]
		//public void BuildReference_has_an_implicit_converstion_to_Build()
		//{
		//    MemoryStream stream = StreamConversion.FromString(BUILD);
		//    stream.Position = 0;

		//    _mockResponse.Setup(response => response.GetResponseStream()).Returns(stream);

		//    RequestFactory.Factory =
		//        uri => uri == new Uri(ServerConfig.BaseUri + ) ? _mockRequest.Object : null;

		//    var br = new BuildReference();
		//    var expected = new Build();

		//    Build actual = br;

		//}

		//[TestMethod]
		//public void ComponentReference_has_an_implicit_converstion_to_Component()
		//{
		//    MemoryStream stream = StreamConversion.FromString(COMPONENT);
		//    stream.Position = 0;

		//    _mockResponse.Setup(response => response.GetResponseStream()).Returns(stream);

		//    RequestFactory.Factory =
		//        uri => uri == new Uri(ServerConfig.BaseUri + ) ? _mockRequest.Object : null;
		//    var cr = new ComponentReference();
		//    var expected = new Component();

		//    Component actual = cr;

		//}

		//[TestMethod]
		//public void ReleaseReference_has_an_implicit_converstion_to_Release()
		//{
		//    MemoryStream stream = StreamConversion.FromString(RELEASE);
		//    stream.Position = 0;

		//    _mockResponse.Setup(response => response.GetResponseStream()).Returns(stream);

		//    RequestFactory.Factory =
		//        uri => uri == new Uri(ServerConfig.BaseUri + ) ? _mockRequest.Object : null;

		//    var rr = new ReleaseReference();
		//    var expected = new Release();

		//    Release actual = rr;

		//}
	}

	#region ReSharper Directives

	// ReSharper restore InconsistentNaming

	#endregion
}
