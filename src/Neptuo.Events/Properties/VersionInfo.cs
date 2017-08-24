using System;

namespace Neptuo.Events
{
    public static class VersionInfo
    {
        internal const string Version = "1.3.1";
        internal const string Preview = "-beta1";

        public static Version GetVersion()
        {
            return new Version(Version);
        }
    }
}
