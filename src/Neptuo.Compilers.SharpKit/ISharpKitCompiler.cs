using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Compilers
{
    /// <summary>
    /// Javascript generator using SharpKit.
    /// </summary>
    public interface ISharpKitCompiler
    {
        ICompilerResult FromSourceCode(string sourceCode, string javascriptFile);

        ICompilerResult FromSourceFile(string sourceFile, string javascriptFile);

        ICompilerResult FromUnit(CodeCompileUnit unit, string javascriptFile);
    }
}
