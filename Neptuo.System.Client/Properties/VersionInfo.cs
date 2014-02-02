using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Client
{
    public static class VersionInfo
    {
        internal const string Version = "4.4.0";

        public static Version GetVersion()
        {
            return new Version(Version);
        }
    }
}
