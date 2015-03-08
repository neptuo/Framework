using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Compilers
{
    /// <summary>
    /// Configuration of the compiler.
    /// </summary>
    public class CompilerConfiguration : ICompilerConfiguration
    {
        /// <summary>
        /// Collection of references.
        /// </summary>
        public CompilerReferenceCollection References { get; private set; }

        /// <summary>
        /// Whether debug mode is enabled.
        /// </summary>
        public bool IsDebugMode { get; set; }

        /// <summary>
        /// Creates empty instance.
        /// </summary>
        public CompilerConfiguration()
        {
            References = new CompilerReferenceCollection();
        }

        /// <summary>
        /// Creates instance for copying from the another.
        /// </summary>
        /// <param name="references">Collection of references.</param>
        /// <param name="isDebugMode">Whether debug mode is enabled.</param>
        protected CompilerConfiguration(CompilerReferenceCollection references, bool isDebugMode)
        {
            Ensure.NotNull(references, "references");
            References = new CompilerReferenceCollection(references.Assemblies, references.Directories);
            IsDebugMode = isDebugMode;
        }

        /// <summary>
        /// Creates deep copy of this instance.
        /// </summary>
        /// <returns>New instance with values copied from this instance.</returns>
        public CompilerConfiguration Copy()
        {
            return new CompilerConfiguration(References, IsDebugMode);
        }
    }
}
