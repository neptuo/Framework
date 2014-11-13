using Neptuo;
using Neptuo.Bootstrap;
using Neptuo.Bootstrap.Constraints;
using Neptuo.Bootstrap.Constraints.Providers;
using Neptuo.Bootstrap.Dependencies;
using Neptuo.Bootstrap.Dependencies.Providers;
using Neptuo.Unity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.BootstrapTasks
{
    class TestBootstrap
    {
        public static void Test()
        {
            //HelloService hello = Static();
            //HelloService hello = Sequence();
            HelloService hello = Hierarchical();
            hello.SayHello("Peter");
        }

        private static HelloService Static()
        {
            return new HelloService("Hi", new WriterService(Console.Out));
        }

        private static HelloService Sequence()
        {
            UnityDependencyContainer dependencyContainer = new UnityDependencyContainer();
            dependencyContainer
                .RegisterInstance("Hi")
                .RegisterInstance(Console.Out)
                .RegisterInstance(Engine.Environment);

            SequenceBootstrapper bootstrapper = new SequenceBootstrapper(task => (IBootstrapTask)dependencyContainer.Resolve(task));
            bootstrapper.Register<Sequence.WriterBootstrapTask>();
            bootstrapper.Register<Sequence.HelloBootstrapTask>();
            bootstrapper.Initialize();

            return Engine.Environment.With<HelloService>();
        }

        private static HelloService Hierarchical()
        {
            HierarchicalContext context = new HierarchicalContext(
                type => (IBootstrapTask)Activator.CreateInstance(type),
                new AttributeConstraintProvider(type => (IBootstrapConstraint)Activator.CreateInstance(type)),
                new TaskPropertyProvider(),
                new EnvironmentExporter()
            );
            HierarchicalBootstrapper bootstrapper = new HierarchicalBootstrapper(context);
            bootstrapper.Register(new Hierarchical.HelloBootstrapTask("Hi"));
            bootstrapper.Register(new Hierarchical.WriterBootstrapTask(Console.Out));
            bootstrapper.Initialize();

            return Engine.Environment.With<HelloService>();
        }
    }

    #region Services

    public class HelloService
    {
        private readonly string helloText;
        private readonly WriterService writerService;

        public HelloService(string helloText, WriterService writerService)
        {
            this.helloText = helloText;
            this.writerService = writerService;
        }

        public void SayHello(string name)
        {
            writerService.Write(String.Format("{0}, {1}!", helloText, name));
        }
    }

    public class WriterService
    {
        private readonly TextWriter writer;

        public WriterService(TextWriter writer)
        {
            this.writer = writer;
        }

        public void Write(string message)
        {
            writer.Write(message);
        }
    }

    #endregion

    #region Tasks

    namespace Sequence
    {
        public class HelloBootstrapTask : IBootstrapTask
        {
            private readonly EngineEnvironment environment;
            private readonly string helloText;
            private readonly WriterService writerService;

            public HelloBootstrapTask(EngineEnvironment environment, string helloText, WriterService writerService)
            {
                this.environment = environment;
                this.helloText = helloText;
                this.writerService = writerService;
            }

            public void Initialize()
            {
                environment.Use(new HelloService(helloText, writerService));
            }
        }

        public class WriterBootstrapTask : IBootstrapTask
        {
            private readonly EngineEnvironment environment;
            private readonly TextWriter writer;

            public WriterBootstrapTask(EngineEnvironment environment, TextWriter writer)
            {
                this.environment = environment;
                this.writer = writer;
            }

            public void Initialize()
            {
                environment.Use(new WriterService(writer));
            }
        }
    }

    namespace Hierarchical
    {
        public class HelloBootstrapTask : IBootstrapTask
        {
            private readonly string helloText;

            [Import]
            public WriterService WriterService { private get; set; }

            [Export]
            public HelloService HelloService { get; private set; }

            public HelloBootstrapTask(string helloText)
            {
                this.helloText = helloText;
            }

            public void Initialize()
            {
                HelloService = new HelloService(helloText, WriterService);
            }
        }

        public class WriterBootstrapTask : IBootstrapTask
        {
            private readonly TextWriter writer;

            [Export]
            public WriterService WriterService { get; private set; }

            public WriterBootstrapTask(TextWriter writer)
            {
                this.writer = writer;
            }

            public void Initialize()
            {
                WriterService = new WriterService(writer);
            }
        }
    }

    #endregion
}
