using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Compilers
{
    /// <summary>
    /// Compiles in-memory assemblies.
    /// </summary>
    public interface IDynamicCompiler : ICompilerConfiguration
    {
        ICompilerResult FromSourceCode(string sourceCode, out Assembly outputAssembly);

        ICompilerResult FromSourceFile(string sourceFile, out Assembly outputAssembly);

        ICompilerResult FromUnit(CodeCompileUnit unit, out Assembly outputAssembly);
    }
}
