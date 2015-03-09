using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Compilers.Internals
{
    internal class SharpKitCompilerConfiguration : CompilerConfiguration, ISharpKitCompilerConfiguration
    {
        public string TempDirectory { get; set; }
        public SharpKitPluginCollection Plugins { get; private set; }
        
        public SharpKitCompilerConfiguration(ICompilerConfiguration configuration)
            : base(configuration.References, configuration.IsDebugMode)
        {
            Plugins = new SharpKitPluginCollection();
        }
    }
}
