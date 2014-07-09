using Neptuo.Web.Services.Hosting.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Behaviors
{
    /// <summary>
    /// Executes <see cref="IPost"/> handler.
    /// </summary>
    public class PostBehavior : IBehavior<IPost>
    {
        /// <summary>
        /// Executes <see cref="IPost.Post"/> method on <paramref name="handler"/> if current request is POST request.
        /// </summary>
        /// <param name="handler">Behavior interface.</param>
        /// <param name="context">Current Http context.</param>
        /// <param name="pipeline">Processing pipeline.</param>
        public void Execute(IPost handler, IHttpContext context, IBehaviorContext pipeline)
        {
            if (context.Request.Method == HttpMethod.Post)
                handler.Post();
            else
                pipeline.Next();
        }
    }
}
