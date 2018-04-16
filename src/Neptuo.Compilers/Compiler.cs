using Neptuo.Compilers.Errors;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Compilers
{
    /// <summary>
    /// Internal implementation of <see cref="IStaticCompiler"/> and <see cref="IDynamicCompiler"/>.
    /// </summary>
    internal class Compiler : CompilerConfigurationWrapper, IStaticCompiler, IDynamicCompiler
    {
        /// <summary>
        /// Inner code provider.
        /// </summary>
        private CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");

        internal Compiler(ICompilerConfiguration configuration)
            : base(configuration)
        { }

        #region IStaticCompiler

        public ICompilerResult FromSourceCode(string sourceCode, string assemblyFile)
        {
            Ensure.NotNullOrEmpty(sourceCode, "sourceCode");
            Ensure.NotNullOrEmpty(assemblyFile, "assemblyFile");

            if(Configuration.IsDebugMode())
                File.WriteAllText(Path.ChangeExtension(assemblyFile, ".cs"), sourceCode);

            CompilerParameters compilerParameters = PrepareCompilerParameters(assemblyFile);
            CompilerResults compilerResults = provider.CompileAssemblyFromSource(compilerParameters, sourceCode);
            return ProcessCompilerResults(compilerResults);
        }

        public ICompilerResult FromSourceFile(string sourceFile, string assemblyFile)
        {
            Ensure.NotNullOrEmpty(sourceFile, "sourceFile");
            Ensure.NotNullOrEmpty(assemblyFile, "assemblyFile");

            if (!File.Exists(sourceFile))
                throw Ensure.Exception.ArgumentFileNotExist(sourceFile, "sourceFile");

            CompilerParameters compilerParameters = PrepareCompilerParameters(assemblyFile);
            CompilerResults compilerResults = provider.CompileAssemblyFromFile(compilerParameters, sourceFile);
            return ProcessCompilerResults(compilerResults);
        }

        public ICompilerResult FromUnit(CodeCompileUnit unit, string assemblyFile)
        {
            Ensure.NotNull(unit, "unit");
            Ensure.NotNullOrEmpty(assemblyFile, "assemblyFile");

            if (Configuration.IsDebugMode())
            {
                using (StringWriter writer = new StringWriter())
                {
                    provider.GenerateCodeFromCompileUnit(unit, writer, new CodeGeneratorOptions());
                    File.WriteAllText(Path.ChangeExtension(assemblyFile, ".cs"), writer.ToString());
                }
            }

            CompilerParameters compilerParameters = PrepareCompilerParameters(assemblyFile);
            CompilerResults compilerResults = provider.CompileAssemblyFromDom(compilerParameters, unit);
            return ProcessCompilerResults(compilerResults);
        }

        #endregion

        #region IDynamicCompiler

        public ICompilerResult FromSourceCode(string sourceCode, out Assembly outputAssembly)
        {
            Ensure.NotNullOrEmpty(sourceCode, "sourceCode");

            CompilerParameters compilerParameters = PrepareCompilerParameters(null);
            CompilerResults compilerResults = provider.CompileAssemblyFromSource(compilerParameters, sourceCode);
            outputAssembly = compilerResults.CompiledAssembly;
            return ProcessCompilerResults(compilerResults);
        }

        public ICompilerResult FromSourceFile(string sourceFile, out Assembly outputAssembly)
        {
            Ensure.NotNullOrEmpty(sourceFile, "sourceFile");

            if (!File.Exists(sourceFile))
                throw Ensure.Exception.ArgumentFileNotExist(sourceFile, "sourceFile");

            CompilerParameters compilerParameters = PrepareCompilerParameters(null);
            CompilerResults compilerResults = provider.CompileAssemblyFromFile(compilerParameters, sourceFile);
            outputAssembly = compilerResults.CompiledAssembly;
            return ProcessCompilerResults(compilerResults);
        }

        public ICompilerResult FromUnit(CodeCompileUnit unit, out Assembly outputAssembly)
        {
            Ensure.NotNull(unit, "unit");

            CompilerParameters compilerParameters = PrepareCompilerParameters(null);
            CompilerResults compilerResults = provider.CompileAssemblyFromDom(compilerParameters, unit);
            outputAssembly = compilerResults.CompiledAssembly;
            return ProcessCompilerResults(compilerResults);
        }

        #endregion

        #region Helper methods

        private CompilerParameters PrepareCompilerParameters(string assemblyFile)
        {
            CompilerParameters compilerParameters = new CompilerParameters
            {
                GenerateExecutable = false,
                IncludeDebugInformation = Configuration.IsDebugMode(),
                GenerateInMemory = String.IsNullOrEmpty(assemblyFile)
            };

            SetupDebugMode(compilerParameters);
            CopyReferences(compilerParameters);
            SetupOutputAssembly(compilerParameters, assemblyFile);
            return compilerParameters;
        }

        private void SetupDebugMode(CompilerParameters compilerParameters)
        {
            if (!Configuration.IsDebugMode())
                compilerParameters.CompilerOptions = "/optimize";
        }

        private void CopyReferences(CompilerParameters compilerParameters)
        {
            foreach (string referencedDirectory in Configuration.References().Directories)
            {
                if (Directory.Exists(referencedDirectory))
                {
                    compilerParameters.ReferencedAssemblies.AddRange(Directory.GetFiles(referencedDirectory, "*.dll"));
                    compilerParameters.ReferencedAssemblies.AddRange(Directory.GetFiles(referencedDirectory, "*.exe"));
                }
            }

            foreach (string referencedAssembly in Configuration.References().Assemblies)
                compilerParameters.ReferencedAssemblies.Add(referencedAssembly);
        }

        private void SetupOutputAssembly(CompilerParameters compilerParameters, string assemblyFile)
        {
            if (!String.IsNullOrEmpty(assemblyFile))
                compilerParameters.OutputAssembly = assemblyFile;
        }

        private ICompilerResult ProcessCompilerResults(CompilerResults compilerResults)
        {
            return new CompilerResult(
                compilerResults.Errors.OfType<CompilerError>().Select(CompilerErrorMapper), 
                compilerResults.Output
            );
        }

        private IErrorInfo CompilerErrorMapper(CompilerError error)
        {
            return new ErrorInfo(error.Line, error.Column, error.ErrorNumber, error.ErrorText);
        }

        #endregion
    }
}
