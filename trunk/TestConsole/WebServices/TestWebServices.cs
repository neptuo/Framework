using Neptuo.Web.Services;
using Neptuo.Web.Services.Behaviors;
using Neptuo.Web.Services.Hosting;
using Neptuo.Web.Services.Hosting.Behaviors;
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
                .Add<IWithRedirect, WithRedirectBehavior>()
                .Add<IWithOutput<string>, WithOutputBehavior<string>>()
                .Add<IGet, GetBehavior>();

            ReflectionPipeline<GetHello> pipeline = new ReflectionPipeline<GetHello>(ServiceEnvironment.Behaviors);
            pipeline.Invoke(new ConsoleContext());
        }
    }
}
