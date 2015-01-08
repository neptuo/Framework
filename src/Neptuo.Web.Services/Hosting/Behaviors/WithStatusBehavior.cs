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
    /// Implementation of <see cref="IWithStatus"/> contract.
    /// </summary>
    public class WithStatusBehavior : WithBehavior<IWithStatus>
    {
        protected override void Execute(IWithStatus handler, IHttpContext context)
        {
            if (handler.Status != null)
                context.Response.Status = handler.Status;
        }
    }
}
