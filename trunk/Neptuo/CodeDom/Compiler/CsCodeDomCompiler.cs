using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Neptuo.CodeDom.Compiler
{
    /// <summary>
    /// Wraps <see cref="CodeDomProvider"/> and provides some shortcuts in compilation process.
    /// </summary>
    public class CsCodeDomCompiler
    {
        /// <summary>
        /// Inner code provider.
        /// </summary>
        private CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");

        /// <summary>
        /// Parameters used by this instance.
        /// </summary>
        private CompilerParameters compilerParameters = new CompilerParameters
        {
            GenerateExecutable = false,
            IncludeDebugInformation = false
        };

        /// <summary>
        /// Whether assembly/executable should be generated only in memory.
        /// </summary>
        public bool IsGeneratedInMemory
        {
            get { return compilerParameters.GenerateInMemory; }
            set { compilerParameters.GenerateInMemory = value; }
        }

        /// <summary>
        /// Whether should compiler generate PDB debug file.
        /// </summary>
        public bool IsDebugInformationIncluded
        {
            get { return compilerParameters.IncludeDebugInformation; }
            set { compilerParameters.IncludeDebugInformation = value; }
        }

        /// <summary>
        /// Adds assemblies in <paramref name="path"/> as references for compilation.
        /// </summary>
        /// <param name="path">Paths to add as references.</param>
        public void AddReferencedAssemblies(params string[] path)
        {
            Guard.NotNull(path, "path");
            foreach (string item in path)
                compilerParameters.ReferencedAssemblies.Add(item);
        }

        /// <summary>
        /// Adds executable and dll files from <paramref name="path"/> as references for compilation.
        /// </summary>
        /// <param name="path">Path where to look for executables and dll files.</param>
        public void AddReferencedFolder(string path)
        {
            Guard.NotNullOrEmpty(path, "path");
            AddReferencedAssemblies(Directory.GetFiles(path, "*.dll"));
            AddReferencedAssemblies(Directory.GetFiles(path, "*.exe"));
        }

        /// <summary>
        /// Compiles assembly from file at <paramref name="fileName"/>.
        /// </summary>
        /// <param name="fileName">Source C# code file path.</param>
        /// <param name="output">Output assembly name/path. If not specified, replaces extension from <paramref name="fileName"/>.</param>
        /// <returns>Compiler resutls.</returns>
        public CompilerResults CompileAssemblyFromFile(string fileName, string output = null)
        {
            Guard.NotNullOrEmpty(fileName, "fileName");
            if (output == null && !IsGeneratedInMemory)
            {
                if (fileName.EndsWith(".cs"))
                    output = fileName.Replace(".cs", ".dll");
            }

            compilerParameters.OutputAssembly = output;
            return provider.CompileAssemblyFromFile(compilerParameters, fileName);
        }

        /// <summary>
        /// Compiles assembly from source code in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">C# source code.</param>
        /// <param name="output">
        /// Output assembly name/path. If not specified, generates in memory when <see cref="CsCodeDomCompiler.IsGeneratedInMemory"/> is true; 
        /// otherwise causes <see cref="ArgumentOutOfRangeException"/>.
        /// </param>
        /// <returns>Compiler resutls.</returns>
        public CompilerResults CompileAssemblyFromSource(string source, string output = null)
        {
            Guard.NotNullOrEmpty(source, "source");
            if (output == null && !IsGeneratedInMemory)
                throw Guard.Exception.ArgumentOutOfRange("output", "Output path must be provided or IsGeneratedInMemory must be set to true.");

            compilerParameters.GenerateExecutable = output == null;
            compilerParameters.OutputAssembly = output;
            return provider.CompileAssemblyFromSource(compilerParameters, source);
        }

        /// <summary>
        /// Compiles assembly from code compile unit.
        /// </summary>
        /// <param name="unit">Source CodeDom compile unit.</param>
        /// <param name="output">
        /// Output assembly name/path. If not specified, generates in memory when <see cref="CsCodeDomCompiler.IsGeneratedInMemory"/> is true; 
        /// otherwise causes <see cref="ArgumentOutOfRangeException"/>.
        /// </param>
        /// <returns>Compiler results.</returns>
        public CompilerResults CompileAssemblyFromUnit(CodeCompileUnit unit, string output)
        {
            Guard.NotNull(unit, "unit");
            if (output == null && !IsGeneratedInMemory)
                throw Guard.Exception.ArgumentOutOfRange("output", "Output path must be provided or IsGeneratedInMemory must be set to true.");

            compilerParameters.OutputAssembly = output;
            return provider.CompileAssemblyFromDom(compilerParameters, unit);
        }
    }
}
