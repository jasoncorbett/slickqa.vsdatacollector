using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SlickQA.SlickTL
{
    public delegate bool WaitForCondition();

    public class TimeoutError : Exception
    {
        public TimeoutError(string condition, int timeoutInSeconds, int intervalInMilliseconds)
            : base(String.Format("Waited {0} seconds for {1} to return true, but it never did.", timeoutInSeconds, condition))
        {
        }
    }

    public static class Wait
    {
        public static void For(WaitForCondition condition, string summary=null, int timeoutInSeconds = 300, int intervalInMilliseconds = 250)
        {
            DateTime start = DateTime.Now;
            bool finished = condition.Invoke();
            while ((DateTime.Now - start).TotalSeconds <= timeoutInSeconds && !finished)
            {
                Thread.Sleep(intervalInMilliseconds);
                finished = condition.Invoke();
            }
            if (!finished)
            {
                if (summary == null)
                    summary = condition.Method.Name;
                throw new TimeoutError(summary, timeoutInSeconds, intervalInMilliseconds);
            }
        }
    }
}
