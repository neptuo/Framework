using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Compilers
{
    /// <summary>
    /// Compiler configurable settings.
    /// </summary>
    public interface ICompilerConfiguration
    {
        /// <summary>
        /// Collection of references.
        /// </summary>
        CompilerReferenceCollection References { get; }
        
        /// <summary>
        /// Whether debug mode is enabled.
        /// </summary>
        bool IsDebugMode { get; }
    }
}
