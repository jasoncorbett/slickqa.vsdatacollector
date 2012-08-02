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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SlickQA.DataCollector.Configuration;
using SlickQA.SlickSharp;
using SlickQA.SlickSharp.Utility.Json;
using SlickQA.SlickSharp.Web;

namespace SlickQA.DataCollector.ConfigurationEditor.Test
{
	[TestClass]
	public sealed class GetProjectList
	{
		private const string PROJECT_1 =
			"  {" + "    \"name\":\"Slickij Developer Project\"," + "    \"id\":\"4ffd9866e4b097a5f43e5e2b\","
			+ "    \"attributes\":{}," + "    \"description\":\"A Project to be used by slickij developers to test features.\","
			+ "    \"configuration\":{" + "      \"name\":\"Slickij Developer Project's Project Configuration\","
			+ "      \"id\":\"4ffd9866e4b097a5f43e5e29\"," + "      \"configurationData\":{}," + "      \"filename\":null,"
			+ "      \"configurationType\":\"PROJECT\"" + "    }," + "    \"releases\":[" + "      {"
			+ "        \"name\":\"1.0.0\"," + "        \"id\":\"4ffd9866e4b097a5f43e5e2f\","
			+ "        \"target\":1304269945716," + "        \"builds\":[" + "          {" + "            \"name\":\"SNAPSHOT\","
			+ "            \"id\":\"4ffd9866e4b097a5f43e5e30\"," + "            \"built\":1342019686934" + "          }"
			+ "        ]," + "        \"defaultBuild\":\"4ffd9866e4b097a5f43e5e30\"" + "      }" + "    ],"
			+ "    \"lastUpdated\":1342019686993," + "    \"automationTools\":[],"
			+ "    \"defaultRelease\":\"4ffd9866e4b097a5f43e5e2f\"," + "    \"tags\":[]," + "    \"components\":[],"
			+ "    \"datadrivenProperties\":[]," + "    \"extensions\":[]," + "    \"defaultBuildName\":\"1.0.0 Build SNAPSHOT\""
			+ "  }";

		private const string PROJECT_2 =
			"  {" + "    \"name\":\"Foo Project\"," + "    \"id\":\"4ffc9e3ee4b097a5f43e5d27\"," + "    \"attributes\":{},"
			+ "    \"description\":null," + "    \"configuration\":{" + "      \"name\":\"FTK's Project Configuration\","
			+ "      \"id\":\"4ffc9e3ee4b097a5f43e5d26\"," + "      \"configurationData\":{}," + "      \"filename\":null,"
			+ "      \"configurationType\":\"PROJECT\"" + "    }," + "    \"releases\":[" + "      {"
			+ "        \"name\":\"4.1.0\"," + "        \"id\":\"4ffc9e3ee4b097a5f43e5d28\","
			+ "        \"target\":1341955646690," + "        \"builds\":[" + "          {" + "            \"name\":\"76\","
			+ "            \"id\":\"4ffc9e3ee4b097a5f43e5d2a\"," + "            \"built\":1341955646758" + "          },"
			+ "        ]," + "        \"defaultBuild\":\"4ffc9e3ee4b097a5f43e5d2a\"" + "      }," + "    ],"
			+ "    \"lastUpdated\":1343081801244," + "    \"automationTools\":[],"
			+ "    \"defaultRelease\":\"500440a4e4b0aea40e783409\"," + "    \"tags\":[]," + "    \"components\":[],"
			+ "    \"datadrivenProperties\":[]," + "    \"extensions\":[]," + "    \"defaultBuildName\":\"4.1.0 Build 76\""
			+ "  }";

		private const string PROJECT_LIST = "[" + PROJECT_1 + "," + PROJECT_2 + "]";

		[TestMethod]
		public void With_no_errors()
		{
			MemoryStream listStream = StreamUtils.ConvertStringToStream(PROJECT_LIST);
			MemoryStream project1Stream = StreamUtils.ConvertStringToStream(PROJECT_1);
			MemoryStream project2Stream = StreamUtils.ConvertStringToStream(PROJECT_2);
			Project project1 = StreamConverter<Project>.ReadFromStream(project1Stream);
			Project project2 = StreamConverter<Project>.ReadFromStream(project2Stream);

			IEnumerable<Project> projects = null;

			var mockRequest = new Mock<IHttpWebRequest>();
			var mockView = new Mock<IConfigurationView>();
			var mockResponse = new Mock<IHttpWebResponse>();

			mockRequest.Setup(request => request.GetResponse()).Returns(mockResponse.Object);
			mockResponse.Setup(response => response.GetResponseStream()).Returns(listStream);

			mockView.Setup(view => view.PopulateProjects(It.IsAny<IEnumerable<Project>>())).Callback<IEnumerable<Project>>(
				list => projects = list);

			RequestFactory.Factory = uri => mockRequest.Object;

			IConfigurationController controller = new ConfigurationController(mockView.Object);

			SlickConfig.SetServerConfig("test", "example.com", 8080, "slick");
			controller.GetProjects();

			List<Project> projList = projects.ToList();
			CollectionAssert.Contains(projList, project1);
			CollectionAssert.Contains(projList, project2);
		}


		[TestMethod]
		public void View_delegates_to_controller()
		{
			var mockController = new Mock<IConfigurationController>();

			IConfigurationView view = new ConfigurationEditor(mockController.Object);

			view.GetProjects(null, new EventArgs());

			mockController.Verify(c => c.GetProjects());
		}
	}
}