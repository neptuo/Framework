using System;

namespace Neptuo.SharpKit.Exugin
{
    public static class VersionInfo
    {
        internal const string Version = "2.0.3";

        public static Version GetVersion()
        {
            return new Version(Version);
        }
    }
}
