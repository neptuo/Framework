using System;

namespace Neptuo.EventSourcing.Formatters.Composite.Json
{
    public static class VersionInfo
    {
        internal const string Version = "0.1.2";

        public static Version GetVersion()
        {
            return new Version(Version);
        }
    }
}
