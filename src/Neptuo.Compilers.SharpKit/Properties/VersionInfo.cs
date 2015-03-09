using System;

namespace Neptuo.Compilers.SharpKit
{
    public static class VersionInfo
    {
        internal const string Version = "0.1.0";

        public static Version GetVersion()
        {
            return new Version(Version);
        }
    }
}
