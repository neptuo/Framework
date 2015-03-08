using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel.Behaviors.Processing.Compilation
{
    /// <summary>
    /// Default implementation of <see cref="ICodeDomContext"/>
    /// </summary>
    public class CodeDomDefaultContext : ICodeDomContext
    {
        public CodeDomPipelineConfiguration Configuration { get; private set; }
        public Type HandlerType { get; private set; }

        public CodeDomDefaultContext(CodeDomPipelineConfiguration configuration, Type handlerType)
        {
            Ensure.NotNull(configuration, "configuration");
            Ensure.NotNull(handlerType, "handlerType");
            Configuration = configuration;
            HandlerType = handlerType;
        }
    }
}
