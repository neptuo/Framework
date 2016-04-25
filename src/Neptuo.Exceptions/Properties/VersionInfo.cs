using System;

namespace Neptuo.Exceptions
{
    public static class VersionInfo
    {
        internal const string Version = "0.1.1";

        public static Version GetVersion()
        {
            return new Version(Version);
        }
    }
}
