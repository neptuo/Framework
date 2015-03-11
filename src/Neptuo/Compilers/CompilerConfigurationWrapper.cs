using Neptuo.Collections.Specialized;
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
        protected ICompilerConfiguration Configuration { get; private set; }

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
        protected CompilerConfigurationWrapper(ICompilerConfiguration configuration)
        {
            Ensure.NotNull(configuration, "configuration");
            Configuration = configuration;
        }

        #region ICompilerConfiguration

        public IKeyValueCollection Set(string key, object value)
        {
            Configuration.Set(key, value);
            return this;
        }

        public IEnumerable<string> Keys
        {
            get { return Configuration.Keys; }
        }

        public bool TryGet<T>(string key, out T value)
        {
            return Configuration.TryGet(key, out value);
        }

        public ICompilerConfiguration Clone()
        {
            return Configuration.Clone();
        }

        #endregion
    }
}
