using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Compilers
{
    public interface ISharpKitCompilerConfiguration : ICompilerConfiguration
    {
        /// <summary>
        /// Path to root temp directory.
        /// </summary>
        string TempDirectory { get; }

        /// <summary>
        /// If <c>true</c>, SharpKit compiler should also run csc.exe; otherwise only skc.exe is executed.
        /// </summary>
        bool IsCscCompilation { get; }

        /// <summary>
        /// Returns enumeration of assembly qualified type name for registered plugins.
        /// </summary>
        IEnumerable<string> Plugins { get; }

        /// <summary>
        /// Adds SharpKit plugin to the configuration.
        /// </summary>
        /// <param name="typeAssemblyName">Assembly qualified type name.</param>
        /// <returns>Self (for fluency).</returns>
        ISharpKitCompilerConfiguration AddPlugin(string typeAssemblyName);
    }
}
