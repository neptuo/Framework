using System;

namespace Neptuo.EventSourcing.Domains
{
    public static class VersionInfo
    {
        internal const string Version = "0.2.3";

        public static Version GetVersion()
        {
            return new Version(Version);
        }
    }
}
