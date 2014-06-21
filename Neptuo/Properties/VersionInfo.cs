using System;

namespace Neptuo
{
    public static class VersionInfo
    {
        internal const string Version = "2.16.2";

        public static Version GetVersion()
        {
            return new Version(Version);
        }
    }
}
