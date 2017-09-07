using System;

namespace Neptuo.EventSourcing.Domains
{
    public static class VersionInfo
    {
        internal const string Version = "1.3.0";
        internal const string Preview = "-beta1";

        public static Version GetVersion()
        {
            return new Version(Version);
        }
    }
}

#if !DEBUG
    Make reference to Neptuo.Models as NuGet reference.
#endif