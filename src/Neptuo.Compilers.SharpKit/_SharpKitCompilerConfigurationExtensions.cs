using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Compilers
{
    /// <summary>
    /// SharpKit extensions for <see cref="ICompilerConfiguration"/>
    /// </summary>
    public static class _SharpKitCompilerConfigurationExtensions
    {
        #region Plugins

        /// <summary>
        /// Returns collection of SharpKit plugins.
        /// </summary>
        /// <param name="configuration">Compiler configuration.</param>
        /// <returns>Collection of SharpKit plugins.</returns>
        public static SharpKitPluginCollection Plugins(this ICompilerConfiguration configuration)
        {
            Ensure.NotNull(configuration, "configuration");

            SharpKitPluginCollection collection;
            if (!configuration.TryGet("Plugins", out collection))
                configuration.Set("Plugins", collection = new SharpKitPluginCollection());

            return collection;
        }

        /// <summary>
        /// Sets collection of SharpKit plugins.
        /// </summary>
        /// <param name="configuration">Compiler configuration.</param>
        /// <param name="plugins">Collection of SharpKit plugins.</param>
        /// <returns>Self (for fluency).</returns>
        public static ICompilerConfiguration Plugins(this ICompilerConfiguration configuration, SharpKitPluginCollection plugins)
        {
            Ensure.NotNull(configuration, "configuration");
            Ensure.NotNull(plugins, "plugins");
            
            configuration.Set("Plugins", plugins);
            return configuration;
        }

        #endregion
    }
}
