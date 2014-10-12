using System;

namespace Neptuo
{
    public static class VersionInfo
    {
        internal const string Version = "3.5.1";

        public static Version GetVersion()
        {
            return new Version(Version);
        }
    }
}
