using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.CodeDom.Compiler
{
    /// <summary>
    /// Contract for codedom compiler.
    /// </summary>
    public interface ICodeDomCompiler
    {
        /// <summary>
        /// Whether assembly/executable should be generated only in memory.
        /// </summary>
        bool IsGeneratedInMemory { get; }

        /// <summary>
        /// Whether should compiler generate PDB debug file.
        /// </summary>
        bool IsDebugInformationIncluded { get; }

        /// <summary>
        /// Adds assemblies in <paramref name="path"/> as references for compilation.
        /// </summary>
        /// <param name="path">Paths to add as references.</param>
        void AddReferencedAssemblies(params string[] path);
    }
}
