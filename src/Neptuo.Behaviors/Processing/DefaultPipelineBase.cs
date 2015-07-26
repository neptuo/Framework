using Neptuo.Activators;
using Neptuo.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors.Processing
{
    /// <summary>
    /// Pipeline for handlers with parameterless constructor.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DefaultPipelineBase<T> : PipelineBase<T>
        where T : new()
    {
        private readonly IActivator<T> handlerFactory = new DefaultActivator<T>();

        protected override IActivator<T> HandlerFactory
        {
            get { return handlerFactory; }
        }
    }
}
