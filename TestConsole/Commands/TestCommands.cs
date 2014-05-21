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
using System.Threading;
using System.Threading.Tasks;

namespace TestConsole.Commands
{
    class TestCommands
    {
        public static IDependencyContainer DependencyContainer;

        public static void Test()
        {
            DependencyContainer = new UnityDependencyContainer();

            DispatchingCommandExecutorFactory commandExecutorFactory = new DispatchingCommandExecutorFactory()
                .AddFactory(typeof(CreateProductCommand), new ThreadPoolCommandExecutorFactory(new DependencyCommandExecutorFactory(DependencyContainer)));

            commandExecutorFactory.OnSearchFactory += OnSearchFactory;

            DependencyContainer
                .RegisterType<ICommandHandler<CreateProductCommand>, CreateProductCommandHandler>()
                .RegisterInstance<ICommandExecutorFactory>(commandExecutorFactory);

            ICommandDispatcher commandDispatcher = new DependencyCommandDispatcher(DependencyContainer);

            Console.WriteLine("Console ThreadID: {0}", Thread.CurrentThread.ManagedThreadId);

            for (int i = 0; i < 5; i++)
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
            //return new DependencyCommandExecutorFactory(dependencyContainer);
            return new TestCommandExecutor();
        }
    }

    class TestCommandExecutor : ICommandExecutorFactory, ICommandExecutor
    {
        public event Action<ICommandExecutor, object> OnCommandHandled;

        public ICommandExecutor CreateExecutor(object command)
        {
            return new DependencyCommandExecutor(TestCommands.DependencyContainer);
        }

        public void Handle(object command)
        {
            var handler = new CreateProductCommandHandler();
            handler.Handle((CreateProductCommand)command);

            if (OnCommandHandled != null)
                OnCommandHandled(this, command);
        }

    }

}
