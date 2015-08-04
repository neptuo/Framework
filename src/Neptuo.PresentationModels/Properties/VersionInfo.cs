using System;

namespace Neptuo.PresentationModels
{
    public static class VersionInfo
    {
        internal const string Version = "6.2.0";

        public static Version GetVersion()
        {
            return new Version(Version);
        }
    }
}
