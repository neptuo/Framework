﻿using System;

namespace Neptuo.PresentationModels.UI
{
    public static class VersionInfo
    {
        internal const string Version = "1.0.0";

        public static Version GetVersion()
        {
            return new Version(Version);
        }
    }
}