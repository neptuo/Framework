using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Compilers
{
    /// <summary>
    /// Base class for wrapping configuration.
    /// </summary>
    public abstract class CompilerConfigurationWrapper : ICompilerConfiguration
    {
        /// <summary>
        /// Compiler configuration.
        /// </summary>
        protected CompilerConfiguration Configuration { get; private set; }

        /// <summary>
        /// Collection of references.
        /// </summary>
        public CompilerReferenceCollection References
        {
            get { return Configuration.References; }
        }

        /// <summary>
        /// Whether debug mode is enabled.
        /// </summary>
        public bool IsDebugMode
        {
            get { return Configuration.IsDebugMode; }
            set { Configuration.IsDebugMode = value; }
        }

        /// <summary>
        /// Creates empty instance with empty configuration.
        /// </summary>
        protected CompilerConfigurationWrapper()
        {
            Configuration = new CompilerConfiguration();
        }

        /// <summary>
        /// Creates instance and copies configuration from <paramref name="configuration"/>.
        /// </summary>
        /// <param name="configuration">Configuration to copy values from.</param>
        protected CompilerConfigurationWrapper(CompilerConfiguration configuration)
        {
            Guard.NotNull(configuration, "configuration");
            Configuration = configuration.Copy();
        }
    }
}
