using System;

namespace Neptuo.EventSourcing.Formatters.Composite
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

#if !DEBUG
    Make reference to Neptuo.Models as NuGet reference.
#endif
