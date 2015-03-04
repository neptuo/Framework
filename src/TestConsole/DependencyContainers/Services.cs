using Neptuo;
using System;
using System.Collections.Generic;
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
        public string SayHello(string name)
        {
            return String.Format("Hi, {0}!", name);
        }
    }

    public interface IMessageWriter
    {
        void Write(string message);
    }

    public class ConsoleWriter : IMessageWriter
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
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

        }

        public void Execute()
        {
            string message = helloService.SayHello("Peter");
            writer.Write(message);
        }
    }
}
