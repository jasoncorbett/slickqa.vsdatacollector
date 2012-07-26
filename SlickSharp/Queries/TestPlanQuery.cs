using System.Collections.Generic;

namespace SlickQA.SlickSharp.Queries
{
	public static class TestPlanQuery
	{
		public static List<TestPlan> GetTestPlans(this Project project)
		{
			try
			{
				return TestPlan.GetList("testplans?projectid=" + project.Id);
			}
			catch
			{
				return new List<TestPlan>();
			}
		}
	}
}
