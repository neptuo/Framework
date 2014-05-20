using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Commands
{
    class CreateProductCommand
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public CreateProductCommand(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }
}
