﻿using Neptuo.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neptuo.Collections.Specialized;
using Neptuo.Activators;

namespace Neptuo.Behaviors.Processing
{
    /// <summary>
    /// Base implementation of pipeline that operates on handler of type <typeparamref name="T"/>.
    /// Integrates execution of behaviors during handler execution.
    /// </summary>
    /// <typeparam name="T">Type of handler.</typeparam>
    public abstract class PipelineBase<T>
    {
        /// <summary>
        /// Factory for handlers of type <typeparamref name="T"/>.
        /// </summary>
        protected abstract IActivator<T> HandlerFactory { get; }

        /// <summary>
        /// Gets enumeration of behaviors for handler of type <typeparamref name="T"/>.
        /// </summary>
        /// <returns>Enumeration of behaviors for handler of type <typeparamref name="T"/>.</returns>
        protected virtual IEnumerable<IBehavior<T>> GetBehaviors()
        {
            return Enumerable.Empty<IBehavior<T>>();
        }

        /// <summary>
        /// Should create instance of <see cref="IBehaviorContext"/> for <paramref name="behaviors"/> and <paramref name="handler"/>.
        /// </summary>
        /// <param name="behaviors">Enumeration of behaviors for current handler.</param>
        /// <param name="handler">Inner handler of current pipeline.</param>
        /// <returns>Instance of <see cref="IBehaviorContext"/> for <paramref name="behaviors"/> and <paramref name="handler"/>.</returns>
        protected virtual IBehaviorContext GetBehaviorContext(IEnumerable<IBehavior<T>> behaviors, T handler)
        {
            return new DefaultBehaviorContext<T>(behaviors, handler);
        }

        /// <summary>
        /// Executes behavior list and target method.
        /// </summary>
        protected Task ExecutePipelineAsync()
        {
            IEnumerable<IBehavior<T>> behaviors = GetBehaviors();
            T handler = HandlerFactory.Create();

            IBehaviorContext context = GetBehaviorContext(behaviors, handler);
            return context.NextAsync();
        }
    }
}
