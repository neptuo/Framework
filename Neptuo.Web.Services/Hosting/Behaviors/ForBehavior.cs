using Neptuo.Web.Services.Hosting.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Behaviors
{
    /// <summary>
    /// Base class for behavior in 'Request' processing pipeline.
    /// </summary>
    /// <typeparam name="T">Type of behavior interface.</typeparam>
    public abstract class ForBehavior<T> : IBehavior<T>
    {
        /// <summary>
        /// Invokes abstract <see cref="Execute"/> before promoting to next behavior in pipeline.
        /// </summary>
        /// <param name="handler">Behavior interface.</param>
        /// <param name="context">Current Http context.</param>
        /// <param name="pipeline">Processing pipeline.</param>
        public void Execute(T handler, IHttpContext context, IBehaviorContext pipeline)
        {
            Execute(handler, context);
            pipeline.Next();
        }

        /// <summary>
        /// Invoked when processing 'Request' pipeline.
        /// </summary>
        /// <param name="handler">Behavior interface.</param>
        /// <param name="context">Current Http context.</param>
        protected abstract void Execute(T handler, IHttpContext context);
    }
}
