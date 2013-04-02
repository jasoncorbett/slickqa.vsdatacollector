namespace mstestexample
{
    using JetBrains.Annotations;

    [PublicAPI]
    static class BuildProvider
    {
        [PublicAPI]
        public static string ReleaseInfo()
        {
            return "4";
        }

        [PublicAPI]
        public static string BuildInfo()
        {
            return "5";
        }

        [PublicAPI]
        public static string GetBuildDescription()
        {
            return "This\nis\na\nsample\nBuild\nString\nInfo";
        }
    }
}
