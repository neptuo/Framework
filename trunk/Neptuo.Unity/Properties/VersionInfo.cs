﻿using System;

namespace Neptuo.Unity
{
    public static class VersionInfo
    {
        internal const string Version = "2.1.0";

        public static Version GetVersion()
        {
            return new Version(Version);
        }
    }
}
