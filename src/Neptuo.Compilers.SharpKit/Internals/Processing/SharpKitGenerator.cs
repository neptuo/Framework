using Neptuo.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Compilers.Internals.Processing
{
    internal class SharpKitGenerator
    {
        private static List<string> installationFolders = new List<string> { SkInstallation, CsInstallation };

        public const string SkInstallation = @"C:\Program Files (x86)\SharpKit\5\Assemblies\v4.0\";
        public const string CsInstallation = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319";

        public const string CscPath = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe";
        public const string SkcPath = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\SharpKit\5\skc5.exe";

        public static ICompilerResult Generate(IUniqueNameProvider tempNameProvider, ICompilerConfiguration configuration, TextReader input, TextWriter output)
        {
            Ensure.NotNull(tempNameProvider, "tempNameProvider");
            Ensure.NotNull(configuration, "configuration");
            Ensure.NotNull(input, "input");
            Ensure.NotNull(output, "output");

            using (SharpKitTempProvider tempProvider = new SharpKitTempProvider(tempNameProvider, configuration))
            {
                tempProvider.WriteInput(input);

                SharpKitProcessBuilder skBuilder = new SharpKitProcessBuilder()
                    .Executable(SkcPath)
                    .Rebuild()
                    .OutDll(tempProvider.OutputDllFilePath)
                    .OutputGeneratedJsFile(tempProvider.OutputJsFilePath)
                    .ManifestFile(tempProvider.ManifestFileName)
                    .AddSourceFile(tempProvider.InputCsFileName)
                    .AddPlugin(configuration.Plugins())
                    .AddReference(CopyReferences(configuration));

                ICompilerResult result;

                if (!TryExecuteProcess(tempProvider, SkcPath, skBuilder.Arguments(), out result))
                    return result;

                if (!tempProvider.TryCopyOutputJs(output))
                {
                    return new CompilerResult(
                        new List<IErrorInfo>()
                        {
                            new ErrorInfo(0, 0, "No js files were generated")
                        },
                        new StringCollection()
                    );
                }

                return new CompilerResult();
            }
        }
        
        private static IEnumerable<string> CopyReferences(ICompilerConfiguration configuration)
        {
            List<string> references = new List<string>();
            foreach (string referencedDirectory in configuration.References().Directories)
            {
                if (Directory.Exists(referencedDirectory))
                {
                    references.AddRange(Directory.GetFiles(referencedDirectory, "*.dll"));
                    references.AddRange(Directory.GetFiles(referencedDirectory, "*.exe"));
                }
            }

            foreach (string referencedAssembly in configuration.References().Assemblies)
            {
                string referencePath = ResolveReferencePath(referencedAssembly);
                references.Add(referencePath);
            }

            return references;
        }

        private static string ResolveReferencePath(string path)
        {
            if (Path.IsPathRooted(path))
                return path;

            foreach (string folder in installationFolders)
            {
                string folderPath = Path.Combine(folder, path);
                if (File.Exists(folderPath))
                    return folderPath;
            }
            
            throw Ensure.Exception.ArgumentOutOfRange("path", "Unnable to find reference '{0}' int '{1}'", path, String.Join(",", installationFolders));
        }

        private static bool TryExecuteProcess(SharpKitTempProvider tempProvider, string exePath, string args, out ICompilerResult result)
        {
            StringCollection outputCollection = new StringCollection();
            StringCollection errorCollection = new StringCollection();

            int cscResultCode = ExecuteProcess(tempProvider.TempDirectory, exePath, args, outputCollection, errorCollection);
            if (cscResultCode != 0)
            {
                result = new CompilerResult(
                    ParseErrorOutput(errorCollection),
                    outputCollection
                );
                return false;
            }

            result = null;
            return true;
        }

        private static int ExecuteProcess(string folder, string file, string args, StringCollection output = null, StringCollection error = null)
        {
            Process process = Process.Start(new ProcessStartInfo
            {
                WorkingDirectory = folder,
                FileName = file,
                Arguments = args,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
            });

            if (output != null)
                process.OutputDataReceived += (s, e) => { Console.WriteLine(e.Data); output.Add(e.Data); };

            if (error != null)
                process.ErrorDataReceived += (s, e) => { Console.WriteLine(e.Data); error.Add(e.Data); };

            process.BeginErrorReadLine();
            process.BeginOutputReadLine();
            process.WaitForExit();

            int resultCode = process.ExitCode;
            return resultCode;
        }

        private static IEnumerable<IErrorInfo> ParseErrorOutput(StringCollection error)
        {
            return error.OfType<string>().Select(e => new ErrorInfo(0, 0, e));
        }
    }
}
