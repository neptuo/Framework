using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators
{
    [TestClass]
    public class TestUnityDependencyContainer
    {
        private IDependencyContainer CreateContainer()
        {
            return new UnityDependencyContainer();
        }

        [TestMethod]
        public void Registration()
        {
            IDependencyContainer container = CreateContainer();
            container.Definitions
                .AddNameScoped<IMessageFormatter, StringMessageFormatter>(container.ScopeName)
                .AddTransient<IHelloService, HiService>()
                .AddScoped<IOutputWriter, StringOutputWriter>()
                .AddTransient<Presenter>();

            IDependencyDefinition definition;
            Assert.AreEqual(container.Definitions.TryGet(typeof(IMessageFormatter), out definition), true);


        }
    }

    public class Presenter
    {
        private readonly IHelloService helloService;
        private readonly IOutputWriter outputWriter;

        public Presenter(IHelloService helloService, IOutputWriter outputWriter)
        {
            this.helloService = helloService;
            this.outputWriter = outputWriter;
        }

        public void Execute(string name)
        {
            outputWriter.Write(helloService.SayHello(name));
        }
    }

    public interface IHelloService
    {
        string SayHello(string name);
    }

    public class HiService : IHelloService
    {
        private readonly IMessageFormatter formatter;

        public HiService(IMessageFormatter formatter)
        {
            Ensure.NotNull(formatter, "formatter");
            this.formatter = formatter;
        }

        public string SayHello(string name)
        {
            return formatter.Format("Hi, {0}!", name);
        }
    }

    public interface IOutputWriter
    {
        void Write(string text);
    }

    public class StringOutputWriter : IOutputWriter
    {
        public string Text { get; private set; }

        public StringOutputWriter()
        {
            Text = String.Empty;
        }

        public void Write(string text)
        {
            Text += text;
        }
    }

    public interface IMessageFormatter
    {
        string Format(string template, params object[] parameters);
    }

    public class StringMessageFormatter : IMessageFormatter
    {
        public string Format(string template, params object[] parameters)
        {
            return String.Format(template, parameters);
        }
    }

}
