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
    }
}
