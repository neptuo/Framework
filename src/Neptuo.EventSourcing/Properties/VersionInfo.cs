using System;

namespace Neptuo.EventSourcing
{
    public static class VersionInfo
    {
        internal const string Version = "1.2.1";
        internal const string Preview = null;

        public static Version GetVersion()
        {
            return new Version(Version);
        }
    }
}