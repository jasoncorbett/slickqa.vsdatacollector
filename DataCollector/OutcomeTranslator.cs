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
				//TODO: Need Unit Test Coverage Here
				default:
					return Status.NO_RESULT;
			}
		}
	}
}