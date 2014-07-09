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
    /// Implementation of <see cref="IForInput<>"/> contract.
    /// </summary>
    /// <typeparam name="T">Type of input.</typeparam>
    public class ForInputBehavior<T> : ForBehavior<IForInput<T>>
    {
        protected override void Execute(IForInput<T> handler, IHttpContext context)
        {
            
        }
    }
}
