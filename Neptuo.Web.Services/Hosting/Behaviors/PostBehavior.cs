using Neptuo.Web.Services.Hosting.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Behaviors
{
    public class PostBehavior : IBehavior<IPost>
    {
        public void Execute(IPost handler, IHttpContext context, IBehaviorContext pipeline)
        {
            handler.Post();
        }
    }
}
