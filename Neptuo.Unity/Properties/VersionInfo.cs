using System;

namespace Neptuo.Unity
{
    public static class VersionInfo
    {
        internal const string Version = "1.3.0";

        public static Version GetVersion()
        {
            return new Version(Version);
        }
    }
}
