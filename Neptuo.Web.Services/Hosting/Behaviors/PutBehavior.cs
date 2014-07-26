using Neptuo.Web.Services.Hosting.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Behaviors
{
    /// <summary>
    /// Executes <see cref="IPut"/> handler.
    /// </summary>
    public class PutBehavior : IBehavior<IPut>
    {
        /// <summary>
        /// Executes <see cref="IPut.Put"/> method on <paramref name="handler"/> if current request is PUT request.
        /// </summary>
        /// <param name="handler">Behavior interface.</param>
        /// <param name="context">Current Http context.</param>
        /// <param name="pipeline">Processing pipeline.</param>
        public void Execute(IPut handler, IHttpContext context, IBehaviorContext pipeline)
        {
            if (context.Request.Method == HttpMethod.Post)
                handler.Put();
            else
                pipeline.Next();
        }
    }
}
