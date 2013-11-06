using Neptuo.Commands;
using Neptuo.Commands.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Data.Commands
{
    public class CreateProductCommand
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
    }
}
