using Neptuo.Web.Services.Behaviors;
using Neptuo.Web.Services.Hosting.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Behaviors
{
    public class WithRedirectBehavior : WithBehaviorBase<IWithRedirect>
    {
        protected override void Execute(IWithRedirect handler, IHttpContext context)
        {
            if (!String.IsNullOrEmpty(handler.Location))
                context.Response.Write("Redirect to: " + handler.Location);
        }
    }
}
