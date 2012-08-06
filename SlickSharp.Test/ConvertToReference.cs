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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlickQA.SlickSharp.ObjectReferences;

namespace SlickQA.SlickSharp.Test
{

	#region ReSharper Directives

	// ReSharper disable InconsistentNaming

	#endregion

	[TestClass]
	public sealed class ConvertToReference
	{
		[TestMethod]
		public void ProjectReference_has_an_implicit_conversion_from_Project()
		{
			var p = new Project {Id = "0123456789abcdefABCDEF", Name = "My Project"};

			ProjectReference pr = p;

			Assert.AreEqual(p.Id, pr.Id);
			Assert.AreEqual(p.Name, pr.Name);
		}

		[TestMethod]
		public void BuildReference_has_an_implicit_conversion_from_Build()
		{
			var b = new Build
			        {
			        	Id = "0123456789abcdefABCDEF",
						Name = "My Build"
			        };

			BuildReference br = b;

			Assert.AreEqual(b.Id, br.Id);
			Assert.AreEqual(b.Name, br.Name);
		}

		[TestMethod]
		public void ComponentReference_has_an_implicit_conversion_from_Component()
		{
			var c = new Component {Id = "0123456789abcdefABCDEF", Name = "My Component", Code = "My Code",};

			ComponentReference cr = c;

			Assert.AreEqual(c.Id, cr.Id);
			Assert.AreEqual(c.Name, cr.Name);
			Assert.AreEqual(c.Code, cr.Code);
		}

		[TestMethod]
		public void ConfigurationReference_has_an_implicit_conversion_from_Configuration()
		{
			var c = new Configuration {Id = "0123456789abcdefABCDEF", Name = "My Configuration", Filename = "Foo.txt",};

			ConfigurationReference cr = c;

			Assert.AreEqual(c.Id, cr.ConfigId);
			Assert.AreEqual(c.Name, cr.Name);
			Assert.AreEqual(c.Filename, cr.FileName);
		}

		[TestMethod]
		public void ReleaseReference_has_an_implicit_conversion_from_Release()
		{
			var r = new Release {Id = "0123456789abcdefABCDEF", Name = "My Release"};

			ReleaseReference rr = r;

			Assert.AreEqual(r.Id, rr.Id);
			Assert.AreEqual(r.Name, rr.Name);
		}

		[TestMethod]
		public void ResultReference_has_an_implicit_conversion_from_Result()
		{
			var r = new Result
			        {
			        	Id = "0123456789abcdefABCDEF",
			        	BuildReference = new BuildReference(),
			        	Recorded = 123456,
			        	Status = "NOT_RUN",
			        };
			ResultReference rr = r;

			Assert.AreEqual(r.Id, rr.Id);
			Assert.AreEqual(r.BuildReference, rr.Build);
			Assert.AreEqual(r.Recorded, rr.DateRecorded);
			Assert.AreEqual(r.Status, rr.ResultStatus);
		}

		[TestMethod]
		public void TestCaseReference_has_an_implicit_conversion_from_TestCase()
		{
			var tc = new Testcase
			         {
			         	Id = "0123456789abcdefABCDEF",
			         	Name = "My TestCase",
			         	AutomationId = "a1",
			         	AutomationKey = "b1",
			         	AutomationTool = "c1",
			         };

			TestcaseReference tcr = tc;

			Assert.AreEqual(tc.Id, tcr.Id);
			Assert.AreEqual(tc.Name, tcr.Name);
			Assert.AreEqual(tc.AutomationId, tcr.AutomationId);
			Assert.AreEqual(tc.AutomationKey, tcr.AutomationKey);
			Assert.AreEqual(tc.AutomationTool, tcr.AutomationTool);
		}

		[TestMethod]
		public void TestRunReference_has_an_implicit_conversion_from_TestRun()
		{
			var tr = new TestRun {Id = "0123456789abcdefABCDEF", Name = "My Project"};
			TestRunReference trr = tr;

			Assert.AreEqual(tr.Id, trr.TestRunId);
			Assert.AreEqual(tr.Name, trr.Name);
		}
	}

	#region ReSharper Directives

	// ReSharper restore InconsistentNaming

	#endregion
}