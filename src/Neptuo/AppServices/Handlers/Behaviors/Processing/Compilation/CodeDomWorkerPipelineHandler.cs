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
        public CodeDomWorkerPipelineHandler(IBehaviorCollection behaviorCollection, CodeDomPipelineConfiguration configuration)
            : base(new CodeDomPipelineFactory<IBackgroundHandler>(typeof(T), behaviorCollection, configuration))
        { }

        public CodeDomWorkerPipelineHandler()
            : this(Engine.Environment.WithAppServices().WithBehaviors(), Engine.Environment.WithAppServices().WithCodeDomConfiguration())
        { }
    }
}
