using System;

namespace Neptuo.SharpKit.CodeGenerator
{
    public static class VersionInfo
    {
        internal const string Version = "1.3.2";

        public static Version GetVersion()
        {
            return new Version(Version);
        }
    }
}
