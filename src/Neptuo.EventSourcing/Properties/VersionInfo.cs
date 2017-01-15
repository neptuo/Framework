using System;

namespace Neptuo.EventSourcing
{
    public static class VersionInfo
    {
        internal const string Version = "1.1.3";

        public static Version GetVersion()
        {
            return new Version(Version);
        }
    }
}