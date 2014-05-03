using System;

namespace Neptuo
{
    public static class VersionInfo
    {
        internal const string Version = "2.15.1";

        public static Version GetVersion()
        {
            return new Version(Version);
        }
    }
}
