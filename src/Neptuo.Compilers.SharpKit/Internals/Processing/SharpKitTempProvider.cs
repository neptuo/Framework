using Neptuo.ComponentModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Compilers.Internals.Processing
{
    internal class SharpKitTempProvider : DisposableBase
    {
        public string TempDirectory { get; private set; }

        public string ManifestFilePath { get; private set; }
        public string ManifestFileName { get; private set; }

        public string InputCsFilePath { get; private set; }
        public string InputCsFileName { get; private set; }

        public string OutputJsFilePath { get; private set; }
        public string OutputJsFileName { get; private set; }

        public string OutputDllFilePath { get; private set; }
        public string OutputDllFileName { get; private set; }

        public SharpKitTempProvider(IUniqueNameProvider tempNameProvider, ISharpKitCompilerConfiguration configuration)
        {
            if (!Directory.Exists(configuration.TempDirectory))
                Directory.CreateDirectory(configuration.TempDirectory);

            TempDirectory = Path.Combine(configuration.TempDirectory, tempNameProvider.Next());
            if (!Directory.Exists(TempDirectory))
                Directory.CreateDirectory(TempDirectory);

            string key = Guid.NewGuid().ToString("N");

            InputCsFileName = String.Format("{0}.cs", key);
            InputCsFilePath = Path.Combine(TempDirectory, InputCsFileName);

            if (File.Exists(InputCsFilePath))
                File.Delete(InputCsFilePath);

            OutputDllFileName = String.Format("{0}.dll", key);
            OutputDllFilePath = Path.Combine(TempDirectory, OutputDllFileName);

            if (File.Exists(OutputDllFilePath))
                File.Delete(OutputDllFilePath);

            OutputJsFileName = String.Format("{0}.js", key);
            OutputJsFilePath = Path.Combine(TempDirectory, OutputJsFileName);

            if (File.Exists(OutputJsFilePath))
                File.Delete(OutputJsFilePath);

            ManifestFileName = String.Format("{0}.skccache", key);
            ManifestFilePath = Path.Combine(TempDirectory, ManifestFileName);

            if (File.Exists(ManifestFilePath))
                File.Delete(ManifestFilePath);
        }

        public void WriteInput(TextReader input)
        {
            string inputContent = input.ReadToEnd();
            File.WriteAllText(InputCsFilePath, inputContent);
        }

        public string CscProcessArguments(string additionalArguments)
        {
            return String.Format("/target:library /nologo /out:{1} {2} \"{0}\"", InputCsFileName, OutputDllFileName, additionalArguments);
        }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();

            // Delete current temp directory (one for each compilation).
            if (Directory.Exists(TempDirectory))
                Directory.Delete(TempDirectory);
        }

        public bool TryCopyOutputJs(TextWriter output)
        {
            if (File.Exists(OutputJsFilePath))
            {
                output.Write(File.ReadAllText(OutputJsFilePath));
                return true;
            }

            return false;
        }
    }
}
