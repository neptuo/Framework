using Neptuo.WebStack;
using Neptuo.WebStack.Http;
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
    public abstract class PipelineBase<T> : IBehaviorContext
    {
        /// <summary>
        /// Enumerator for behaviors for type <typeparamref name="T" />
        /// </summary>
        private IEnumerator<IBehavior<T>> behaviorEnumerator;

        /// <summary>
        /// Instance of handler to execute.
        /// </summary>
        private T handler;

        /// <summary>
        /// Backing storage for <see cref="CustomValues"/>.
        /// </summary>
        private IKeyValueCollection customValues;

        public IKeyValueCollection CustomValues
        {
            get
            {
                if (customValues == null)
                    customValues = new KeyValueCollection();

                return customValues;
            }
        }

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
        /// Creates instance of handler and using <see cref="IBehavior"/> executes action.
        /// </summary>
        /// <returns>Response for the current HTTP request.</returns>
        public Task TryHandleAsync()
        {
            IActivator<T> handlerFactory = GetHandlerFactory();
            this.handler = handlerFactory.Create();

            behaviorEnumerator = GetBehaviors().GetEnumerator();
            return NextAsync();
        }

        /// <summary>
        /// Moves to next processing to next behavior.
        /// </summary>
        /// <param name="httpRequest">Current HTTP request.</param>
        public Task NextAsync()
        {
            // Try to call next behavior in pipeline.
            if (behaviorEnumerator.MoveNext())
                return behaviorEnumerator.Current.ExecuteAsync(handler, this);

            // No more behaviors equal to inability process request this way.
            return Task.FromResult(false);
        }
    }
}
