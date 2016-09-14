using System;

namespace SharpKit.UnobtrusiveFeatures
{
    public static class VersionInfo
    {
        internal const string Version = "2.2.0";
        internal const string BetaSuffix = "-beta1";

        public static Version GetVersion()
        {
            return new Version(Version);
        }
    }
}
