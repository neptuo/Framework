using Neptuo.ComponentModel.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel.Behaviors.Processing
{
    /// <summary>
    /// Pipeline for handlers with parameterless constructor.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DefaultPipelineBase<T> : PipelineBase<T>
        where T: new()
    {
        protected override IActivator<T> GetHandlerFactory()
        {
            return new DefaultActivator<T>();
        }
    }
}
