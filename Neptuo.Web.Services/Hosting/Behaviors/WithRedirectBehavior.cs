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
    /// Implementation of <see cref="IWithRedirect"/> contract.
    /// </summary>
    public class WithRedirectBehavior : WithBehavior<IWithRedirect>
    {
        protected override void Execute(IWithRedirect handler, IHttpContext context)
        {
            if (!String.IsNullOrEmpty(handler.Location))
                context.Response.OutputWriter.Write("Redirect to: " + handler.Location);
        }
    }
}
