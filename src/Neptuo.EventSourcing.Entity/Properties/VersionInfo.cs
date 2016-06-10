using System;

namespace Neptuo.EventSourcing.Entity
{
    public static class VersionInfo
    {
        internal const string Version = "0.2.0";

        public static Version GetVersion()
        {
            return new Version(Version);
        }
    }
}
