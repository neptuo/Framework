using Neptuo.Compilers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors.Processing.Compilation
{
    /// <summary>
    /// Default implementation of <see cref="ICodeDomContext"/>
    /// </summary>
    public class DefaultCodeDomContext : ICodeDomContext
    {
        public ICompilerConfiguration Configuration { get; private set; }
        public Type HandlerType { get; private set; }

        public DefaultCodeDomContext(ICompilerConfiguration configuration, Type handlerType)
        {
            Ensure.NotNull(configuration, "configuration");
            Ensure.NotNull(handlerType, "handlerType");
            Configuration = configuration;
            HandlerType = handlerType;
        }
    }
}
