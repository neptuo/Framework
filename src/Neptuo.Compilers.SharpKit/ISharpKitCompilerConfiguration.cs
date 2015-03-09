using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Compilers
{
    /// <summary>
    /// Configuration for SharpKit compiler.
    /// </summary>
    public interface ISharpKitCompilerConfiguration : ICompilerConfiguration
    {
        /// <summary>
        /// Path to root temp directory.
        /// </summary>
        string TempDirectory { get; set; }

        /// <summary>
        /// Collection of SharpKit plugins.
        /// </summary>
        SharpKitPluginCollection Plugins { get; }
    }
}
