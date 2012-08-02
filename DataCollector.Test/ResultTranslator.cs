using System;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;
using System.Linq;
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
