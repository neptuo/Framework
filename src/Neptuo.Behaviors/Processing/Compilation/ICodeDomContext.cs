using Neptuo.Compilers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel.Behaviors.Processing.Compilation
{
    /// <summary>
    /// Base context for code dom generators.
    /// </summary>
    public interface ICodeDomContext
    {
        /// <summary>
        /// Pipeline configuration.
        /// </summary>
        ICompilerConfiguration Configuration { get; }

        /// <summary>
        /// Type of handler to wrap.
        /// </summary>
        Type HandlerType { get; }
    }
}
