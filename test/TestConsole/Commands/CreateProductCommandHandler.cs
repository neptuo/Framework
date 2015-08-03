using Neptuo;
using Neptuo.Services.Commands.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestConsole.Commands
{
    //[DiscardException(typeof(NullReferenceException))]
    class CreateProductCommandHandler : ICommandHandler<CreateProductCommand>
    {
        static int count = 0;

        public Task HandleAsync(CreateProductCommand command)
        {
            //throw new NullReferenceException();
            Ensure.NotNull(command, "command");

            Console.WriteLine("Handler ThreadID: {0}", Thread.CurrentThread.ManagedThreadId);
            //Thread.Sleep(5000);
            count++;

            Console.WriteLine("Creating product {2}: '{0}' with price {1}.", command.Name, command.Price, count);
            return Task.FromResult(true);
        }
    }
}
