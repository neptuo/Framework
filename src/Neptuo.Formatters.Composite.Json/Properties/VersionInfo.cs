using System;

namespace Neptuo.Formatters.Composite.Json
{
    public static class VersionInfo
    {
        internal const string Version = "2.0.0";

        public static Version GetVersion()
        {
            return new Version(Version);
        }
    }
}
