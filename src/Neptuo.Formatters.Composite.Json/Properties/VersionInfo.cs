using System;

namespace Neptuo.Formatters.Composite.Json
{
    public static class VersionInfo
    {
        internal const string Version = "1.0.1"; // TODO: Return NUGET reference to the Neptuo.Formatters.Composite.

        public static Version GetVersion()
        {
            return new Version(Version);
        }
    }
}
