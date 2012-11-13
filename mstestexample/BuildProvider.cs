using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mstestexample
{
    static class BuildProvider
    {

        public static string ReleaseInfo()
        {
            return "2";
        }

        public static string BuildInfo()
        {
            throw new NotImplementedException();
        }
    }
}
