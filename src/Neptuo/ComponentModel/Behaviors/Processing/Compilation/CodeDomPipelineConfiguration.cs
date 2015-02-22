using Neptuo.Compilers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel.Behaviors.Processing.Compilation
{
    /// <summary>
    /// Configuration <see cref="CodeDomPipelineFactoryBase"/>.
    /// </summary>
    public class CodeDomPipelineConfiguration : CompilerConfiguration
    {
        /// <summary>
        /// Path to temp directory.
        /// </summary>
        public string TempDirectory { get; private set; }

        /// <summary>
        /// Creates new instance of configuration class.
        /// </summary>
        /// <param name="tempDirectory">Path to temp directory.</param>
        /// <param name="binDirectories">List of bin directories to add as references.</param>
        public CodeDomPipelineConfiguration(string tempDirectory, params string[] binDirectories)
        {
            Guard.NotNullOrEmpty(tempDirectory, "tempDirectory");
            TempDirectory = tempDirectory;

            if (!Directory.Exists(TempDirectory))
                Directory.CreateDirectory(TempDirectory);

            foreach (string binDirectory in binDirectories)
                References.AddDirectory(binDirectory);
        }
    }
}
