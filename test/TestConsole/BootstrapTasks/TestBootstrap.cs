using Neptuo;
using Neptuo.Activators;
using Neptuo.Bootstrap;
using Neptuo.Bootstrap.Handlers;
using Neptuo.Bootstrap.Handlers.Metadata;
using Neptuo.Bootstrap.Hierarchies;
using Neptuo.Bootstrap.Processing;
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



            ManualLoaderBuilder builder = new Builder()
                .ToSimple()
                .ToManual();

            builder
                .Add<Sequence.HelloBootstrapTask>();

            builder
                .ToBootstrapper()
                .Initialize();
        }

        private static HelloService Static()
        {
            return new HelloService("Hi", new WriterService(Console.Out));
        }

        private static HelloService Sequence()
        {
            IDependencyContainer dependencyContainer = new UnityDependencyContainer();
            dependencyContainer.Definitions
                .AddScoped<string>(dependencyContainer.ScopeName, "Hi")
                .AddScoped<TextWriter>(dependencyContainer.ScopeName, Console.Out);

            Neptuo.Bootstrap.Sequences.ManualBootstrapper bootstrapper = new Neptuo.Bootstrap.Sequences.ManualBootstrapper();
            bootstrapper.Add(new DependencyFactory<Sequence.WriterBootstrapTask>(dependencyContainer));
            bootstrapper.Add(new DependencyFactory<Sequence.HelloBootstrapTask>(dependencyContainer));
            bootstrapper.Initialize();

            //return Engine.Environment.With<HelloService>();
            throw new NotImplementedException();
        }

        private static HelloService Hierarchical()
        {
            IDependencyContainer dependencyContainer = new UnityDependencyContainer();

            //Neptuo.Bootstrap.Hierarchies.ManualBootstrapper bootstrapper = new Neptuo.Bootstrap.Hierarchies.DependencyValueProvider()
            //    .AddDependencyImporter(new DependencyValueProvider(dependencyContainer))
            //    .AddDependencyImporter(new DependencyValueProvider(dependencyContainer))
            //    .ToManual();

            //bootstrapper.Add(new Hierarchical.HelloBootstrapTask("Hi"));
            //bootstrapper.Add(new Hierarchical.WriterBootstrapTask(Console.Out));
            //bootstrapper.Initialize();

            //return Engine.Environment.With<HelloService>();
            throw new NotImplementedException();
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
        public class HelloBootstrapTask : IBootstrapHandler
        {
            //private readonly EngineEnvironment environment;
            //private readonly string helloText;
            //private readonly WriterService writerService;

            //public HelloBootstrapTask(EngineEnvironment environment, string helloText, WriterService writerService)
            //{
            //    this.environment = environment;
            //    this.helloText = helloText;
            //    this.writerService = writerService;
            //}

            public void Handle()
            {
            //    environment.Use(new HelloService(helloText, writerService));
            }
        }

        public class WriterBootstrapTask : IBootstrapHandler
        {
            //private readonly EngineEnvironment environment;
            //private readonly TextWriter writer;

            //public WriterBootstrapTask(EngineEnvironment environment, TextWriter writer)
            //{
            //    this.environment = environment;
            //    this.writer = writer;
            //}

            public void Handle()
            {
            //    environment.Use(new WriterService(writer));
            }
        }
    }

    namespace Hierarchical
    {
        public class HelloBootstrapTask : IBootstrapHandler
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

            public void Handle()
            {
                HelloService = new HelloService(helloText, WriterService);
            }
        }

        public class WriterBootstrapTask : IBootstrapHandler
        {
            private readonly TextWriter writer;

            [Export]
            public WriterService WriterService { get; private set; }

            public WriterBootstrapTask(TextWriter writer)
            {
                this.writer = writer;
            }

            public void Handle()
            {
                WriterService = new WriterService(writer);
            }
        }
    }

    #endregion
}
