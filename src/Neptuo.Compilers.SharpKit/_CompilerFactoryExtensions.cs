using Neptuo.Compilers.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Compilers
{
    /// <summary>
    /// SharpKit extensions for <see cref="CompilerFactory"/>.
    /// </summary>
    public static class _CompilerFactoryExtensions
    {
        /// <summary>
        /// Creates compiler for SharpKit javascript generation.
        /// </summary>
        /// <returns>Compiler for SharpKit javascript generation.</returns>
        public static ISharpKitCompiler CreateSharpKit(this CompilerFactory factory)
        {
            Ensure.NotNull(factory, "factory");
            return new SharpKitCompiler(factory.CopyConfiguration());
        }
    }
}
