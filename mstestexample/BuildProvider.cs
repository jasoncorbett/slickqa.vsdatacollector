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
            return "4";
        }

        public static string BuildInfo()
        {
            return "5";
        }

        public static string GetBuildDescription()
        {
            return "This\nis\na\nsample\nBuild\nString\nInfo";
        }
    }
}
