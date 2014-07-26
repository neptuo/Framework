using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Pipelines.Compilation
{
    /// <summary>
    /// Configuration <see cref="CodeDomPipelineFactory"/>.
    /// </summary>
    public class CodeDomPipelineConfiguration
    {
        /// <summary>
        /// Path to temp directory.
        /// </summary>
        public string TempDirectory { get; private set; }

        /// <summary>
        /// Enumeration of directories to add references from.
        /// </summary>
        private IEnumerable<string> binDirectories;

        /// <summary>
        /// Creates new instance of configuration class.
        /// </summary>
        /// <param name="tempDirectory">Path to temp directory.</param>
        /// <param name="binDirectories">Enumeration of directories to add references from.</param>
        public CodeDomPipelineConfiguration(string tempDirectory, params string[] binDirectories)
        {
            Guard.NotNullOrEmpty(tempDirectory, "tempDirectory");
            Guard.NotNull(binDirectories, "bindDirectories");
            TempDirectory = tempDirectory;
            this.binDirectories = binDirectories;
        }

        /// <summary>
        /// Adds <paramref name="binDirectories"/> as directories with references.
        /// </summary>
        /// <param name="binDirectories">Enumeration of directories to add references from.</param>
        public void AddBindDirectories(params string[] binDirectories)
        {
            Guard.NotNull(binDirectories, "bindDirectories");
            this.binDirectories = Enumerable.Union(this.binDirectories, binDirectories);
        }

        /// <summary>
        /// Returns enumeration of directories to add references from.
        /// </summary>
        /// <returns>Enumeration of directories to add references from.</returns>
        public IEnumerable<string> EnumerateBindDirectories()
        {
            return binDirectories;
        }
    }
}
