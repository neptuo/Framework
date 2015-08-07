using Neptuo.Activators.AutoExports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators.Tests
{
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

    [Export(typeof(IOutputWriter))]
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

    public class ConsoleOutputWriter : IOutputWriter
    {
        public void Write(string text)
        {
            Console.Write(text);
        }
    }

    public abstract class OutputWriterBase : IOutputWriter
    {
        public void Write(string text)
        {
            throw new NotImplementedException();
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


    [Export(typeof(Counter))]
    [ExportNameScoped("S1")]
    public class Counter
    {
        public static int count;

        public int Count
        {
            get { return count; }
        }

        public Counter()
        {
            count++;
        }
    }

    public class Disposable : DisposableBase
    {
        public static int count;

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            count++;
        }
    }

    public class View
    {
        [Dependency]
        public IOutputWriter Writer { get; set; }

        [Dependency]
        public IHelloService HelloService { get; set; }

        public IMessageFormatter MessageFormatter { get; set; }
    }
}
