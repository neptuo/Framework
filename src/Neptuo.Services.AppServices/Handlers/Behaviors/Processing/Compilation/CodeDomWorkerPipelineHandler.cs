using Neptuo.Compilers;
using Neptuo.ComponentModel.Behaviors;
using Neptuo.ComponentModel.Behaviors.Processing.Compilation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.AppServices.Handlers.Behaviors.Processing.Compilation
{
    public class CodeDomWorkerPipelineHandler<T> : TransientWorkerHandler
        where T: IBackgroundHandler
    {
        public CodeDomWorkerPipelineHandler(CodeDomWorkerPipelineConfiguration configuration)
            : base(new CodeDomPipelineFactory<IBackgroundHandler>(typeof(T), configuration))
        { }
    }
}
