using Neptuo.ComponentModel.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neptuo.Collections.Specialized;

namespace Neptuo.ComponentModel.Behaviors.Processing
{
    /// <summary>
    /// Base implementation of pipeline that operates on handler of type <typeparamref name="T"/>.
    /// Integrates execution of behaviors during handler execution.
    /// </summary>
    /// <typeparam name="T">Type of handler.</typeparam>
    public abstract class PipelineBase<T>
    {
        /// <summary>
        /// Gets factory for handlers of type <typeparamref name="T"/>.
        /// </summary>
        /// <returns>Factory for handlers of type <typeparamref name="T"/>.</returns>
        protected abstract IActivator<T> GetHandlerFactory();

        /// <summary>
        /// Gets enumeration of behaviors for handler of type <typeparamref name="T"/>.
        /// </summary>
        /// <returns>Enumeration of behaviors for handler of type <typeparamref name="T"/>.</returns>
        protected abstract IEnumerable<IBehavior<T>> GetBehaviors();

        /// <summary>
        /// Should create instance of <see cref="IBehaviorContext"/> for <paramref name="behaviors"/> and <paramref name="handler"/>.
        /// </summary>
        /// <param name="behaviors">Enumeration of behaviors for current handler.</param>
        /// <param name="handler">Inner handler of current pipeline.</param>
        /// <returns></returns>
        protected virtual IBehaviorContext GetBehaviorContext(IEnumerable<IBehavior<T>> behaviors, T handler)
        {
            return new DefaultBehaviorContext<T>(behaviors, handler);
        }

        /// <summary>
        /// Executed behavior list.
        /// </summary>
        protected Task ExecutePipeline()
        {
            IEnumerable<IBehavior<T>> behaviors = GetBehaviors();
            IActivator<T> handlerFactory = GetHandlerFactory();
            T handler = handlerFactory.Create();

            IBehaviorContext context = GetBehaviorContext(behaviors, handler);
            return context.NextAsync();
        }
    }
}
