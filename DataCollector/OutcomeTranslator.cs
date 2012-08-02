using Microsoft.VisualStudio.TestTools.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlickQA.SlickSharp;

namespace SlickQA.DataCollector
{
	public static class OutcomeTranslator
	{
		public static Status Convert(TestOutcome testOutcome)
		{
			switch (testOutcome)
			{
				case TestOutcome.Passed:
					return Status.PASS;
				case TestOutcome.Failed:
				case TestOutcome.Timeout:
					return Status.FAIL;
				case TestOutcome.InProgress:
					return Status.NO_RESULT;
				case TestOutcome.Error:
					return Status.BROKEN_TEST;
				case TestOutcome.Pending:
					return Status.NO_RESULT;
				case TestOutcome.Inconclusive:
					return Status.NO_RESULT;
				case TestOutcome.Aborted:
					return Status.CANCELLED;
				case TestOutcome.NotExecuted:
					return Status.SKIPPED;
				default:
					return Status.NO_RESULT;
			}
		}
	}
}