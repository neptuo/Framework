using Neptuo.Web.Services.Behaviors;
using Neptuo.Web.Services.Hosting.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Behaviors
{
    /// <summary>
    /// Implementation of <see cref="IWithOutput<>"/> contract.
    /// </summary>
    /// <typeparam name="T">Type of output.</typeparam>
    public class WithOutputBehavior<T> : WithBehavior<IWithOutput<T>>
    {
        protected override void Execute(IWithOutput<T> handler, IHttpContext context)
        {
            string output = handler.Output as string;
            if (output != null)
            {
                context.Response.OutputWriter.Write(output);
                return;
            }

            if (handler.Output != null)
                context.Response.OutputContext.Serializer.TrySerialize(context.Response, handler.Output);
        }
    }
}
