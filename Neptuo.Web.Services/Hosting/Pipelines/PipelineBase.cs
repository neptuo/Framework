using Neptuo.Web.Services.Hosting.Behaviors;
using Neptuo.Web.Services.Hosting.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Pipelines
{
    /// <summary>
    /// Base implementation of pipeline that operates on handler of type <typeparamref name="T"/>.
    /// Integrates execution of behaviors during handler execution.
    /// </summary>
    /// <typeparam name="T">Type of handler.</typeparam>
    public abstract class PipelineBase<T> : IPipeline, IBehaviorContext
    {
        /// <summary>
        /// Enumerator for behaviors for type <typeparamref name="T" />
        /// </summary>
        private IEnumerator<IBehavior<T>> behaviorEnumerator;

        /// <summary>
        /// Current Http request context that this pipeline operates on.
        /// </summary>
        private IHttpContext context;

        /// <summary>
        /// Instance of handler to execute.
        /// </summary>
        private T handler;

        /// <summary>
        /// Gets factory for handlers of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="context">Current Http request context.</param>
        /// <returns>Factory for handlers of type <typeparamref name="T"/>.</returns>
        protected abstract IHandlerFactory<T> GetHandlerFactory(IHttpContext context);

        /// <summary>
        /// Gets enumeration of behaviors for handler of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="context">Current Http request context.</param>
        /// <returns>Enumeration of behaviors for handler of type <typeparamref name="T"/>.</returns>
        protected abstract IEnumerable<IBehavior<T>> GetBehaviors(IHttpContext context);

        /// <summary>
        /// Creates instance of handler and using <see cref="IBehavior"/> executes action.
        /// </summary>
        /// <param name="context">Current Http request context.</param>
        public void Invoke(IHttpContext context)
        {
            IHandlerFactory<T> handlerFactory = GetHandlerFactory(context);
            this.handler = handlerFactory.Create(context);
            this.context = context;

            behaviorEnumerator = GetBehaviors(context).GetEnumerator();
            Next();
        }

        /// <summary>
        /// Moves to next processing to next behavior.
        /// </summary>
        public void Next()
        {
            if (behaviorEnumerator.MoveNext())
                behaviorEnumerator.Current.Execute(handler, context, this);
        }
    }
}
