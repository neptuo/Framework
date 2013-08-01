using System;

namespace Neptuo
{
    public static class VersionInfo
    {
        internal const string Version = "1.7.2";

        public static Version GetVersion()
        {
            return new Version(Version);
        }
    }
}
