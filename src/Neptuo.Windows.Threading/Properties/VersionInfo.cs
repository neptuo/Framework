using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Windows.Threading
{
    public class VersionInfo
    {
        internal const string Version = "1.0.1";

        public static Version GetVersion()
        {
            return new Version(Version);
        }
    }
}
