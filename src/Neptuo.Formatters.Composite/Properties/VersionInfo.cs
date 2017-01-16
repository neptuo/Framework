using System;

namespace Neptuo.Formatters.Composite
{
    public static class VersionInfo
    {
        internal const string Version = "2.1.1";

        public static Version GetVersion()
        {
            return new Version(Version);
        }
    }
}
