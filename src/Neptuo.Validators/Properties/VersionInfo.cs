using System;

namespace Neptuo.Validators
{
    public static class VersionInfo
    {
        internal const string Version = "1.1.0";
        internal const string Preview = "-beta1";

        public static Version GetVersion()
        {
            return new Version(Version);
        }
    }
}
