using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neptuo.System.Client
{
    public static class VersionInfo
    {
        internal const string Version = "4.3.7";

        public static Version GetVersion()
        {
            return new Version(Version);
        }
    }
}
