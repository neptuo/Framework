using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Compilers
{
    /// <summary>
    /// Compiles assemblies to the file system.
    /// </summary>
    public interface IStaticCompiler : ICompilerConfiguration
    {
        ICompilerResult FromSourceCode(string sourceCode, string assemblyFile);
         
        ICompilerResult FromSourceFile(string sourceFile, string assemblyFile);
         
        ICompilerResult FromUnit(CodeCompileUnit unit, string assemblyFile);
    }
}
