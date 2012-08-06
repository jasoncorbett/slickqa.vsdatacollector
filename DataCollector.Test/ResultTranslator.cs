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

using Microsoft.VisualStudio.TestTools.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlickQA.SlickSharp;

namespace SlickQA.DataCollector.Test
{
	[TestClass]
	public class ResultTranslator
	{

		[TestMethod]
		public void Converts_test_outcome_to_status_enum()
		{
			Assert.AreEqual(Status.BROKEN_TEST, OutcomeTranslator.Convert(TestOutcome.Error));
			Assert.AreEqual(Status.CANCELLED, OutcomeTranslator.Convert(TestOutcome.Aborted));
			Assert.AreEqual(Status.FAIL, OutcomeTranslator.Convert(TestOutcome.Failed));
			Assert.AreEqual(Status.NO_RESULT, OutcomeTranslator.Convert(TestOutcome.Inconclusive));
			Assert.AreEqual(Status.NO_RESULT, OutcomeTranslator.Convert(TestOutcome.InProgress));
			Assert.AreEqual(Status.PASS, OutcomeTranslator.Convert(TestOutcome.Passed));
			Assert.AreEqual(Status.SKIPPED, OutcomeTranslator.Convert(TestOutcome.NotExecuted));
			Assert.AreEqual(Status.FAIL, OutcomeTranslator.Convert(TestOutcome.Timeout));
			Assert.AreEqual(Status.NO_RESULT, OutcomeTranslator.Convert(TestOutcome.Pending));
		}
	}
}
