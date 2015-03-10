using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Compilers.Internals
{
    internal class SharpKitCompilerConfigurationWrapper : ISharpKitCompilerConfiguration
    {
        protected SharpKitCompilerConfiguration Configuration { get; private set; }

        public bool IsDebugMode
        {
            get { return Configuration.IsDebugMode; }
            set { Configuration.IsDebugMode = value; }
        }

        public CompilerReferenceCollection References
        {
            get { return Configuration.References; }
        }

        public string TempDirectory
        {
            get { return Configuration.TempDirectory; }
            set { Configuration.TempDirectory = value; }
        }

        public SharpKitPluginCollection Plugins
        {
            get { return Configuration.Plugins; }
        }

        public SharpKitCompilerConfigurationWrapper(SharpKitCompilerConfiguration configuration)
        {
            Ensure.NotNull(configuration, "configuration");
            Configuration = configuration;
        }
    }
}
