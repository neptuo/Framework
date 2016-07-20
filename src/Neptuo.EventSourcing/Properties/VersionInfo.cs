using System;

namespace Neptuo.EventSourcing
{
    public static class VersionInfo
    {
        internal const string Version = "1.1.0";

        public static Version GetVersion()
        {
            return new Version(Version);
        }
    }
}

//#if !DEBUG

//"Add nuget reference Neptuo.EventSourcing.Domains"

//#endif