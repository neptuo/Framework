using System;

namespace Neptuo.EventSourcing
{
    public static class VersionInfo
    {
        internal const string Version = "0.4.1";

        public static Version GetVersion()
        {
            return new Version(Version);
        }
    }
}
