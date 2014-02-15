using System;

namespace Neptuo.PresentationModels
{
    public static class VersionInfo
    {
        internal const string Version = "4.3.1";

        public static Version GetVersion()
        {
            return new Version(Version);
        }
    }
}
