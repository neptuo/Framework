using Neptuo.Web.Services.Hosting.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Behaviors
{
    /// <summary>
    /// Executes <see cref="IDelete"/> handler.
    /// </summary>
    public class DeleteBehavior : IBehavior<IDelete>
    {
        /// <summary>
        /// Executes <see cref="IDelete.Execute"/> method on <paramref name="handler"/> if current request is DELETE request.
        /// </summary>
        /// <param name="handler">Behavior interface.</param>
        /// <param name="context">Current Http context.</param>
        /// <param name="pipeline">Processing pipeline.</param>
        public void Execute(IDelete handler, IHttpContext context, IBehaviorContext pipeline)
        {
            if (context.Request.Method == HttpMethod.Post)
                handler.Execute();
            else
                pipeline.Next();
        }
    }
}
