using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Activators;
using Neptuo.Commands;
using Neptuo.Data;
using Neptuo.EventSourcing;
using Neptuo.Formatters;
using Neptuo.Formatters.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo.EventSourcing
{
    [TestClass]
    public class T_EventSourcing_PersistentCommandDispatcher
    {
        [TestMethod]
        public void DistributorAndThreadPool()
        {
            PersistentCommandDispatcherBuilder builder = new PersistentCommandDispatcherBuilder()
                .UseCommandDistributor(new SerialCommandDistributor())
                .UseFormatter(new CompositeCommandFormatter(new ReflectionCompositeTypeProvider(new ReflectionCompositeDelegateFactory()), Factory.Default<JsonCompositeStorage>()))
                .UseSchedulingProvider(new DateTimeNowSchedulingProvider())
                .UseStore(new EmptyCommandStore());

            CommandHandlerService service = new CommandHandlerService();

            PersistentCommandDispatcher dispatcher = builder.Create();
            dispatcher.Handlers
                .Add(new SlowCommandHandler(service))
                .Add(new FastCommandHandler(service));

            dispatcher.HandleAsync(new SlowCommand()).Wait();
            dispatcher.HandleAsync(new FastCommand()).Wait();
            dispatcher.HandleAsync(new FastCommand()).Wait();
            dispatcher.HandleAsync(new SlowCommand()).Wait();
            dispatcher.HandleAsync(new FastCommand()).Wait();

            Thread.Sleep(10000);
            Assert.AreEqual(5, service.Log.Count);
            Assert.AreEqual(CommandType.Slow, service.Log[0]);
            Assert.AreEqual(CommandType.Fast, service.Log[1]);
            Assert.AreEqual(CommandType.Fast, service.Log[2]);
            Assert.AreEqual(CommandType.Slow, service.Log[3]);
            Assert.AreEqual(CommandType.Fast, service.Log[4]);
        }
    }
}
