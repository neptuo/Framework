using Neptuo;
using Neptuo.Commands;
using Neptuo.Commands.Execution;
using Neptuo.Commands.Handlers;
using Neptuo.Unity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Commands
{
    class TestCommands
    {
        static IDependencyContainer dependencyContainer;

        public static void Test()
        {
            DispatchingCommandExecutorFactory commandExecutorFactory = new DispatchingCommandExecutorFactory();
            commandExecutorFactory.OnSearchFactory += OnSearchFactory;

            dependencyContainer = new UnityDependencyContainer()
                .RegisterType<ICommandHandler<CreateProductCommand>, CreateProductCommandHandler>()
                .RegisterInstance<ICommandExecutorFactory>(commandExecutorFactory);

            ICommandDispatcher commandDispatcher = new DependencyCommandDispatcher(dependencyContainer);
            
            
            
            commandDispatcher.Handle(new CreateProductCommand("Pen", 5.0));


            //int count = 1000;
            //TestCommandDispatcher(count, commandDispatcher);
            //TestDirect(count);
        }

        static void TestCommandDispatcher(int count, ICommandDispatcher commandDispatcher)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < count; i++)
            {
                commandDispatcher.Handle(new CreateProductCommand("Pen", 5.0));
            }
            sw.Stop();
            Console.WriteLine("CommandDispatcher: {0}ms", sw.ElapsedMilliseconds);
        }

        static void TestDirect(int count)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < count; i++)
            {
                var handler = new CreateProductCommandHandler();
                handler.Handle(new CreateProductCommand("Pen", 5.0));
            }
            sw.Stop();
            Console.WriteLine("Direct: {0}ms", sw.ElapsedMilliseconds);
        }

        private static ICommandExecutorFactory OnSearchFactory(object arg)
        {
            return new DependencyCommandExecutorFactory(dependencyContainer);
            //return new TestCommandExecutor();
        }
    }

    class TestCommandExecutor : ICommandExecutorFactory, ICommandExecutor
    {
        public ICommandExecutor CreateExecutor(object command)
        {
            return this;
        }

        public void Handle(object command)
        {
            var handler = new CreateProductCommandHandler();
            handler.Handle((CreateProductCommand)command);
        }
    }

}
