using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Compilers.Internals
{
    internal class SharpKitCompiler : CompilerConfigurationWrapper, ISharpKitCompiler
    {
        public SharpKitCompiler(CompilerConfiguration configuration)
            : base(configuration)
        { }

        public ICompilerResult FromSourceCode(string sourceCode, string javascriptFile)
        {
            throw new NotImplementedException();
        }

        public ICompilerResult FromSourceFile(string sourceFile, string javascriptFile)
        {
            if (!File.Exists(sourceFile))
                Ensure.Exception.ArgumentFileNotExist(sourceFile, "sourceFile");

            return FromSourceCode(File.ReadAllText(sourceFile), javascriptFile);
        }

        public ICompilerResult FromUnit(CodeCompileUnit unit, string javascriptFile)
        {
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");

            StringWriter output = new StringWriter();
            provider.GenerateCodeFromCompileUnit(unit, output, new CodeGeneratorOptions());

            return FromSourceCode(output.ToString(), javascriptFile);
        }
    }
}
