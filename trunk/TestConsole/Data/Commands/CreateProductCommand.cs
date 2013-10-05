using Neptuo.Data.Commands;
using Neptuo.Data.Commands.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Data.Commands
{
    public class CreateProductCommand : ICommandValidationResult<IValidationResult>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
    }
}
