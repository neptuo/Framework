using Neptuo.AppServices.Behaviors;
using Neptuo.AppServices.Behaviors.Hosting;
using Neptuo.ComponentModel.Behaviors;
using Neptuo.ComponentModel.Behaviors.Processing;
using Neptuo.ComponentModel.Behaviors.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Behaviors
{
    class TestBehaviors
    {
        public static void Test()
        {
            IBehaviorCollection behaviorCollection = new BehaviorProviderCollection()
                .Add(
                    new AttributeBehaviorProvider()
                        .AddMapping(typeof(ReprocessAttribute), typeof(ReprocessBehavior))
                );

            MethodInvokePipeline<HelloService, string> pipeline = new MethodInvokePipeline<HelloService, string>(behaviorCollection, "SayHello");
            pipeline.ExecuteAsync().ContinueWith(message => Console.WriteLine(message.Result));
        }
    }

    [Reprocess]
    public class HelloService
    {
        bool isFirst = true;

        public string SayHello()
        {
            if (isFirst)
            {
                isFirst = false;
                throw new NotSupportedException();
            }

            return "Hello!";
        }
    }
}
