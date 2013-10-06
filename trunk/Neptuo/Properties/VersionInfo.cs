using System;

namespace Neptuo
{
    public static class VersionInfo
    {
        internal const string Version = "2.2.3";

        public static Version GetVersion()
        {
            return new Version(Version);
        }
    }
}
