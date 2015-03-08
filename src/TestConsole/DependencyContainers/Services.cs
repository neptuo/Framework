using Neptuo;
using Neptuo.Activators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.DependencyContainers
{
    public interface IHelloService
    {
        string SayHello(string name);
    }

    public class HiService : IHelloService
    {
        public HiService()
        {
            Console.WriteLine("ctor:HiService");
        }

        public string SayHello(string name)
        {
            return String.Format("Hi, {0}!", name);
        }
    }

    public interface IMessageWriter
    {
        void Write(string message);
    }

    public class TextMessageWriter : IMessageWriter
    {
        private readonly TextWriter output;

        public TextMessageWriter(TextWriter output)
        {
            Guard.NotNull(output, "output");
            this.output = output;

            Console.WriteLine("ctor:TextMessageWriter");
        }

        public void Write(string message)
        {
            output.WriteLine(message);
        }
    }

    public class ConsoleWriterActivator : IActivator<TextMessageWriter>
    {
        public TextMessageWriter Create()
        {
            return new TextMessageWriter(Console.Out);
        }
    }



    public class Presenter
    {
        private readonly IMessageWriter writer;
        private readonly IHelloService helloService;

        public Presenter(IMessageWriter writer, IHelloService helloService)
        {
            Guard.NotNull(writer, "writer");
            Guard.NotNull(helloService, "helloService");
            this.writer = writer;
            this.helloService = helloService;

            Console.WriteLine("ctor:Presenter");
        }

        public void Execute()
        {
            string message = helloService.SayHello("Peter");
            writer.Write(message);
        }
    }
}
