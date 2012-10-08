﻿// Copyright 2012 AccessData Group, LLC.
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

using Archive;
using Microsoft.VisualStudio.TestTools.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlickQA.SlickSharp;

namespace SlickQA.DataCollector.Test
{
	[TestClass]
	public sealed class ResultTranslator
	{

		[TestMethod]
		public void Converts_test_outcome_to_status_enum()
		{
			Assert.AreEqual(ResultStatus.BROKEN_TEST, OutcomeTranslator.Convert(TestOutcome.Error));
			Assert.AreEqual(ResultStatus.CANCELLED, OutcomeTranslator.Convert(TestOutcome.Aborted));
			Assert.AreEqual(ResultStatus.FAIL, OutcomeTranslator.Convert(TestOutcome.Failed));
			Assert.AreEqual(ResultStatus.NO_RESULT, OutcomeTranslator.Convert(TestOutcome.Inconclusive));
			Assert.AreEqual(ResultStatus.NO_RESULT, OutcomeTranslator.Convert(TestOutcome.InProgress));
			Assert.AreEqual(ResultStatus.PASS, OutcomeTranslator.Convert(TestOutcome.Passed));
			Assert.AreEqual(ResultStatus.SKIPPED, OutcomeTranslator.Convert(TestOutcome.NotExecuted));
			Assert.AreEqual(ResultStatus.FAIL, OutcomeTranslator.Convert(TestOutcome.Timeout));
			Assert.AreEqual(ResultStatus.NO_RESULT, OutcomeTranslator.Convert(TestOutcome.Pending));
		}
	}
}