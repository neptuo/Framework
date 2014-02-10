using System;

namespace Neptuo.ObjectBuilder
{
    public static class VersionInfo
    {
        internal const string Version = "1.1.6";

        public static Version GetVersion()
        {
            return new Version(Version);
        }
    }
}
