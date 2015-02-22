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
        /// Required pipeline base type.
        /// </summary>
        public Type BaseType { get; private set; }

        /// <summary>
        /// Creates new instance of configuration class.
        /// </summary>
        /// <param name="tempDirectory">Path to temp directory.</param>
        /// <param name="binDirectories">List of bin directories to add as references.</param>
        public CodeDomPipelineConfiguration(string tempDirectory, params string[] binDirectories)
            : this(typeof(DefaultPipelineBase<>), tempDirectory, binDirectories)
        { }

        /// <summary>
        /// Creates new instance of configuration class.
        /// </summary>
        /// <param name="baseType">Custom base type (extending <see cref="DefaultPipelineBase{1}"/>).</param>
        /// <param name="tempDirectory">Path to temp directory.</param>
        /// <param name="binDirectories">List of bin directories to add as references.</param>
        public CodeDomPipelineConfiguration(Type baseType, string tempDirectory, params string[] binDirectories)
        {
            Guard.NotNull(baseType, "baseType");
            Guard.NotNullOrEmpty(tempDirectory, "tempDirectory");
            BaseType = baseType;
            TempDirectory = tempDirectory;

            if (!Directory.Exists(TempDirectory))
                Directory.CreateDirectory(TempDirectory);

            foreach (string binDirectory in binDirectories)
                References.AddDirectory(binDirectory);
        }
    }
}
