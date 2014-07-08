using Neptuo.Web.Services.Hosting;
using Neptuo.Web.Services.Hosting.Behaviors;
using Neptuo.Web.Services.Hosting.Http;
using Neptuo.Web.Services.Hosting.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.WebServices
{
    public class GetHelloPipeline : DefaultPipelineBase<GetHello>
    {
        protected override IEnumerable<IBehavior<GetHello>> GetBehaviors(IHttpContext context)
        {
            yield return new WithRedirectBehavior();
            yield return new WithOutputBehavior<string>();
            yield return new GetBehavior();
        }
    }
}
