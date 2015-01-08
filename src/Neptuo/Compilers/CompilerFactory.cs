using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Compilers
{
    /// <summary>
    /// Factory for predefining and preconfiguring compilers (both dynamic and static).
    /// </summary>
    public class CompilerFactory : CompilerConfigurationWrapper
    {
        /// <summary>
        /// Creates new empty instance.
        /// </summary>
        public CompilerFactory()
        { }

        /// <summary>
        /// Creates new instance and copies initial configuration from <paramref name="configuration"/>.
        /// </summary>
        /// <param name="configuration">Initial configuration.</param>
        public CompilerFactory(CompilerConfiguration configuration)
            : base(configuration)
        { }

        /// <summary>
        /// Creates compiler for in-memory assembly compilation.
        /// </summary>
        /// <returns>In-memory compiler.</returns>
        public IDynamicCompiler CreateDynamic()
        {
            return new Compiler(Configuration);
        }

        /// <summary>
        /// Creates compiler for compiling assemblies to the file system.
        /// </summary>
        /// <returns>Compiler for compiling assemblies to the file system.</returns>
        public IStaticCompiler CreateStatic()
        {
            return new Compiler(Configuration);
        }
    }
}
