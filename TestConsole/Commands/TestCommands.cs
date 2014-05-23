using Neptuo;
using Neptuo.Commands;
using Neptuo.Commands.Events;
using Neptuo.Commands.Events.Handlers;
using Neptuo.Commands.Execution;
using Neptuo.Commands.Handlers;
using Neptuo.Commands.Interception;
using Neptuo.Events;
using Neptuo.Events.Handlers;
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
    class TestCommands : TestClass
    {
        public static IDependencyContainer DependencyContainer;

        public static void Test()
        {
            DependencyContainer = new UnityDependencyContainer();

            EventManager eventManager = new EventManager();

            ManualInterceptorProvider interceptorProvider = new ManualInterceptorProvider(DependencyContainer);
                //.AddInterceptorFactory(typeof(CreateProductCommandHandler), provider => new DiscardExceptionAttribute(typeof(NullReferenceException)));

            DispatchingCommandExecutorFactory commandExecutorFactory = new DispatchingCommandExecutorFactory()
                .AddFactory(typeof(CreateProductCommand), new ThreadPoolCommandExecutorFactory(new DependencyCommandExecutorFactory(DependencyContainer, interceptorProvider)));

            commandExecutorFactory.OnSearchFactory += OnSearchFactory;

            DependencyContainer
                .RegisterType<ICommandHandler<CreateProductCommand>, CreateProductCommandHandler>()
                .RegisterInstance<ICommandExecutorFactory>(commandExecutorFactory);

            ICommandDispatcher commandDispatcher = new DependencyCommandDispatcher(DependencyContainer, eventManager);

            Console.WriteLine("Console ThreadID: {0}", Thread.CurrentThread.ManagedThreadId);

            for (int i = 0; i < 1; i++)
            {
                CreateProductCommand command = new CreateProductCommand("Pen", 5.0);

                eventManager.Subscribe(new CommandHandlerFactory(command, new SingletonEventHandlerFactory<CommandHandled>(new ActionEventHandler<CommandHandled>(OnCommandHandled))));
                commandDispatcher.Handle(command);
            }


            //int count = 1000;
            //TestCommandDispatcher(count, commandDispatcher);
            //TestDirect(count);
        }

        private static void OnCommandHandled(CommandHandled eventData)
        {
            Console.WriteLine(eventData);
        }

        static void TestCommandDispatcher(int count, ICommandDispatcher commandDispatcher)
        {
            DebugIteration("CommandDispatcher", count, () => commandDispatcher.Handle(new CreateProductCommand("Pen", 5.0)));
        }

        static void TestDirect(int count)
        {
            DebugIteration("Direct", count, () => new CreateProductCommandHandler().Handle(new CreateProductCommand("Pen", 5.0)));
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
            return this;
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
