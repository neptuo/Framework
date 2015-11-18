using Neptuo;
using Neptuo.Services.Commands;
using Neptuo.Services.Commands.Handlers;
using Neptuo.Events;
using Neptuo.Events.Handlers;
using Neptuo.Activators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestConsole.Commands
{
    class TestCommands : TestClass
    {
        public static IDependencyContainer DependencyContainer;

        public static void Test()
        {
            DependencyContainer = new UnityDependencyContainer();

            DefaultEventManager eventManager = new DefaultEventManager();

            DependencyContainer.Definitions
                .Add(typeof(ICommandHandler<CreateProductCommand>), DependencyLifetime.Transient, typeof(CreateProductCommandHandler));

            ICommandDispatcher commandDispatcher = new DependencyCommandDispatcher(DependencyContainer);

            Console.WriteLine("Console ThreadID: {0}", Thread.CurrentThread.ManagedThreadId);

            for (int i = 0; i < 1; i++)
            {
                CreateProductCommand command = new CreateProductCommand("Pen", 5.0);

                commandDispatcher.HandleAsync(command);
                GC.Collect();
            }


            //int count = 1000;
            //TestCommandDispatcher(count, commandDispatcher);
            //TestDirect(count);
        }

        static void TestCommandDispatcher(int count, ICommandDispatcher commandDispatcher)
        {
            DebugIteration("CommandDispatcher", count, () => commandDispatcher.HandleAsync(new CreateProductCommand("Pen", 5.0)));
        }

        static void TestDirect(int count)
        {
            DebugIteration("Direct", count, () => new CreateProductCommandHandler().HandleAsync(new CreateProductCommand("Pen", 5.0)));
        }
    }
}
