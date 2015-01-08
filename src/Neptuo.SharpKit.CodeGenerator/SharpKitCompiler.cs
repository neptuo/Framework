using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.SharpKit.CodeGenerator
{
    public class SharpKitCompiler
    {
        public const string SkInstallation = @"C:\Program Files (x86)\SharpKit\5\Assemblies\v4.0\";
        public const string CsInstallation = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319";

        public const string CscPath = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe";
        public const string SkcPath = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\SharpKit\5\skc5.exe";

        protected HashSet<string> Folders { get; private set; }
        protected HashSet<string> References { get; private set; }
        protected HashSet<string> SharpKitReferences { get; private set; }
        protected HashSet<string> Plugins { get; private set; }

        public string TempDirectory { get; set; }
        public bool RunCsc { get; set; }

        public SharpKitCompiler()
        {
            Folders = new HashSet<string> { SkInstallation, CsInstallation };
            References = new HashSet<string>();
            SharpKitReferences = new HashSet<string>();
            Plugins = new HashSet<string>();
            RunCsc = true;
        }

        public void AddReferenceFolder(string path)
        {
            AddReference(Directory.GetFiles(path, "*.dll"));
            AddReference(Directory.GetFiles(path, "*.exe"));
        }

        public void AddReference(params string[] path)
        {
            foreach (string item in path)
            {
                if (!item.Contains("Neptuo.SharpKit.CodeGenerator")) //TODO: Do it better!
                {
                    string reference = ResolveReferencePath(item);
                    References.Add(reference);
                    SharpKitReferences.Add(reference);
                }
            }
        }

        public void AddSharpKitReferenceFolder(string path)
        {
            AddSharpKitReference(Directory.GetFiles(path, "*.dll"));
            AddSharpKitReference(Directory.GetFiles(path, "*.exe"));
        }

        public void AddSharpKitReference(params string[] path)
        {
            foreach (string item in path)
                SharpKitReferences.Add(ResolveReferencePath(item));
        }

        public void RemoveReference(params string[] path)
        {
            foreach (string item in path)
            {
                if (!References.Remove(item))
                {
                    string targetReference = null;
                    foreach (string reference in References)
                    {
                        if (reference.EndsWith(item))
                        {
                            targetReference = reference;
                            break;
                        }
                    }

                    if (targetReference != null)
                        References.Remove(targetReference);
                }
            }
        }

        public void AddPlugin(string assemblyTypeName)
        {
            Plugins.Add(assemblyTypeName);
        }

        public void AddPlugin(string assemlbyName, string typeName)
        {
            AddPlugin(String.Format("{0}, {1}", typeName, assemlbyName));
        }

        protected string ResolveReferencePath(string path)
        {
            if (Path.IsPathRooted(path))
                References.Add(path);

            foreach (string folder in Folders)
            {
                string folderPath = Path.Combine(folder, path);
                if (File.Exists(folderPath))
                    return folderPath;
            }
            throw new ArgumentOutOfRangeException("path", String.Format("Unnable to find reference '{0}' int '{1}'", path, String.Join(",", Folders)));
        }

        public void Generate(SharpKitCompilerContext context)
        {
            string inputContent = context.Input.ReadToEnd();

            if (!Directory.Exists(TempDirectory))
                Directory.CreateDirectory(TempDirectory);

            string key = Guid.NewGuid().ToString("N");

            string sourceCodeFileName = String.Format("{0}.cs", key);
            string sourceCodePath = Path.Combine(TempDirectory, sourceCodeFileName);

            if (File.Exists(sourceCodePath))
                File.Delete(sourceCodePath);

            string dllFileName = String.Format("{0}.dll", key);
            string dllPath = Path.Combine(TempDirectory, dllFileName);
            
            if (File.Exists(dllPath))
                File.Delete(dllPath);
            
            string jsFileName = String.Format("{0}.js", key);
            string jsPath = Path.Combine(TempDirectory, jsFileName);

            if (File.Exists(jsPath))
                File.Delete(jsPath);
            
            string manifestFileName = String.Format("{0}.skccache", key);
            string manifestPath = Path.Combine(TempDirectory, manifestFileName);
            
            if (File.Exists(manifestPath))
                File.Delete(manifestPath);
            
            File.WriteAllText(sourceCodePath, inputContent);


            SharpKitProcessBuilder skBuilder = new SharpKitProcessBuilder()
                .Executable(SkcPath)
                .Rebuild()
                .OutDll(dllFileName)
                .OutputGeneratedJsFile(jsFileName)
                .ManifestFile(manifestFileName)
                .AddSourceFile(sourceCodeFileName)
                .AddReference(References.ToArray())
                .AddPlugin(Plugins.ToArray());

            SharpKitProcessBuilder cscBuilder = new SharpKitProcessBuilder()
                .AddReference(References.ToArray());


            List<string> blackList = new List<string> { sourceCodeFileName, jsFileName, dllFileName, manifestFileName };

            string cscArgs = String.Format("/target:library /nologo /out:{1} {2} \"{0}\"", sourceCodeFileName, dllFileName, cscBuilder.Arguments());

            if (RunCsc)
            {
                ExecuteResult cscResult = Execute(TempDirectory, CscPath, cscArgs);
                if (cscResult.ExitCode != 0)
                    throw new SharpKitCompilerException(cscResult);
            }

            //string args = String.Format("/rebuild /out:{3} {4} {5} /OutputGeneratedJsFile:\"{0}\" \"{1}\" /ManifestFile:\"{2}\"", jsPath, sourceCodeFileName, manifestPath, dllFileName, sysRefsArgs, refsArgs);

            ExecuteResult skResult = Execute(TempDirectory, skBuilder.Executable(), skBuilder.Arguments());
            //skcRes.Output = GetOutput(skResult.Output, blackList);
            //skcRes.Success = skResult.ExitCode == 0 && File.Exists(jsPath);
            if (skResult.ExitCode != 0)
            {
                //skcRes.ErrorMessage = "Compilation error";
                throw new SharpKitCompilerException(skResult);
            }
            else if (!File.Exists(jsPath))
            {
                //skcRes.ErrorMessage = "No js files were generated";
                throw new Exception("No js files were generated");
            }
            else
            {
                context.Output.Write(File.ReadAllText(jsPath));
                //skcRes.Success = true;
                File.Delete(jsPath);
            }
            if (File.Exists(manifestPath))
                File.Delete(manifestPath);



            if (File.Exists(dllPath))
                File.Delete(dllPath);

            if (File.Exists(sourceCodePath))
                File.Delete(sourceCodePath);
        }


        protected ExecuteResult Execute(string folder, string file, string args)
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

            ExecuteResult result = new ExecuteResult();

            process.OutputDataReceived += (s, e) => { Console.WriteLine(e.Data); result.Output.Add(e.Data); };
            process.ErrorDataReceived += (s, e) => { Console.WriteLine(e.Data); result.Error.Add(e.Data); };
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();
            process.WaitForExit();

            result.ExitCode = process.ExitCode;
            return result;
        }


        #region Deprecated

        string GetOutput(List<string> output, List<string> blackList)
        {
            var s = String.Join("\n", output.ToArray());
            foreach (var name in blackList)
                s = s.Replace(name, "");
            return s;
        }

        public class ExecuteResult
        {
            public int ExitCode { get; set; }
            public List<string> Output { get; private set; }
            public List<string> Error { get; private set; }

            public ExecuteResult()
            {
                Output = new List<string>();
                Error = new List<string>();
            }
        }

        #endregion
    }
}
