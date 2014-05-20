using Neptuo;
using Neptuo.Commands.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Commands
{
    class CreateProductCommandHandler : ICommandHandler<CreateProductCommand>
    {
        public void Handle(CreateProductCommand command)
        {
            Guard.NotNull(command, "command");
            Console.WriteLine("Creating product '{0}' with price {1}.", command.Name, command.Price);
        }
    }
}
