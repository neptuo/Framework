using Neptuo.Web.Services;
using Neptuo.Web.Services.Behaviors;
using Neptuo.Web.Services.Hosting;
using Neptuo.Web.Services.Hosting.Behaviors;
using Neptuo.Web.Services.Hosting.Http;
using Neptuo.Web.Services.Hosting.Pipelines;
using Neptuo.Web.Services.Hosting.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.WebServices
{
    class TestWebServices
    {
        public static void Test()
        {
            ServiceEnvironment.Behaviors
                .AddEndpoints()
                .Add<IWithRedirect, WithRedirectBehavior>()
                .Add<IWithOutput<string>, WithOutputBehavior<string>>();

            ServiceEnvironment.RouteTable
                .Add(new UriRoute("~/hello", UriKind.Relative), new ReflectionPipelineFactory<GetHello>());


            IHttpContext context = new ConsoleContext(HttpMethod.Get, new Uri("~/hello", UriKind.Relative));
            IPipelineFactory pipelineFactory;
            if (ServiceEnvironment.RouteTable.TryGet(context.Request, out pipelineFactory))
                pipelineFactory.Create().Invoke(context);
        }
    }
}
