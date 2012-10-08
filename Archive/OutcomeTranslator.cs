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

namespace Archive
{
	public static class OutcomeTranslator
	{
		public static ResultStatus Convert(TestOutcome testOutcome)
		{
			switch (testOutcome)
			{
				case TestOutcome.Passed:
					return ResultStatus.PASS;
				case TestOutcome.Failed:
				case TestOutcome.Timeout:
					return ResultStatus.FAIL;
				case TestOutcome.InProgress:
					return ResultStatus.NO_RESULT;
				case TestOutcome.Error:
					return ResultStatus.BROKEN_TEST;
				case TestOutcome.Pending:
					return ResultStatus.NO_RESULT;
				case TestOutcome.Inconclusive:
					return ResultStatus.NO_RESULT;
				case TestOutcome.Aborted:
					return ResultStatus.CANCELLED;
				case TestOutcome.NotExecuted:
					return ResultStatus.SKIPPED;
				//TODO: Need Unit Test Coverage Here
				default:
					return ResultStatus.NO_RESULT;
			}
		}
	}
}