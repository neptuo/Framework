using Neptuo;
using Neptuo.Pipelines.Commands;
using Neptuo.Pipelines.Commands.Events;
using Neptuo.Pipelines.Commands.Events.Handlers;
using Neptuo.Pipelines.Commands.Execution;
using Neptuo.Pipelines.Commands.Handlers;
using Neptuo.Pipelines.Commands.Interception;
using Neptuo.ComponentModel;
using Neptuo.Pipelines.Events;
using Neptuo.Pipelines.Events.Handlers;
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

            ManualInterceptorProvider interceptorProvider = new ManualInterceptorProvider(DependencyContainer);
                //.AddInterceptorFactory(typeof(CreateProductCommandHandler), provider => new DiscardExceptionAttribute(typeof(NullReferenceException)));

            DispatchingCommandExecutorFactory commandExecutorFactory = new DispatchingCommandExecutorFactory()
                .AddFactory(typeof(CreateProductCommand), new ThreadPoolCommandExecutorFactory(new DependencyCommandExecutorFactory(DependencyContainer, interceptorProvider)));

            commandExecutorFactory.OnSearchFactory += OnSearchFactory;

            DependencyContainer
                .Map(typeof(IEventDispatcher), DependencyLifetime.AnyScope, eventManager)
                .Map(typeof(IEventRegistry), DependencyLifetime.AnyScope, eventManager)

                .Map(typeof(ICommandHandler<CreateProductCommand>), DependencyLifetime.Transient, typeof(CreateProductCommandHandler))
                .Map(typeof(ICommandExecutorFactory), DependencyLifetime.AnyScope, commandExecutorFactory);

            ICommandDispatcher commandDispatcher = new DependencyCommandDispatcher(DependencyContainer, eventManager);

            Console.WriteLine("Console ThreadID: {0}", Thread.CurrentThread.ManagedThreadId);

            for (int i = 0; i < 1; i++)
            {
                CreateProductCommand command = new CreateProductCommand("Pen", 5.0);

                eventManager.Subscribe(new CommandEventHandler(command, DelegateEventHandler.FromAction<CommandHandled>(OnCommandHandled)));
                eventManager.Subscribe(new CommandEventHandler(command, new WeakEventHandler<CommandHandled>(new CreateProductEventHandler())));
                commandDispatcher.Handle(command);
                GC.Collect();
            }


            //int count = 1000;
            //TestCommandDispatcher(count, commandDispatcher);
            //TestDirect(count);
        }

        private static void OnCommandHandled(CommandHandled payload)
        {
            Console.WriteLine(payload);
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

    class CreateProductEventHandler : IEventHandler<CommandHandled>
    {
        public CreateProductEventHandler()
        {
            Console.WriteLine("Constructing CreateProductEventHandler.");
        }

        ~CreateProductEventHandler()
        {
            Console.WriteLine("Destructing CreateProductEventHandler.");
        }

        public Task HandleAsync(CommandHandled payload)
        {
            CreateProductCommand command = (CreateProductCommand)payload.Command;
            Console.WriteLine("Created product: {0}", command.Name);
            return Task.FromResult(true);
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
