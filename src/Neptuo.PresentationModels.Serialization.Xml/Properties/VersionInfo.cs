﻿using System;

namespace Neptuo.PresentationModels.Serialization.Xml
{
    public static class VersionInfo
    {
        internal const string Version = "1.1.0";

        public static Version GetVersion()
        {
            return new Version(Version);
        }
    }
}
