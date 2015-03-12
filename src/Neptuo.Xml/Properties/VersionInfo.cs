using System;

namespace Neptuo.Xml
{
    public static class VersionInfo
    {
        internal const string Version = "1.9.0";

        public static Version GetVersion()
        {
            return new Version(Version);
        }
    }
}
