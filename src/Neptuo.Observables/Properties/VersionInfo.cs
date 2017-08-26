using System;

namespace Neptuo.Observables
{
    public static class VersionInfo
    {
        internal const string Version = "1.2.1";

        public static Version GetVersion()
        {
            return new Version(Version);
        }
    }
}
