using Neptuo.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Data.Commands.Validation
{
    public class CreateProductValidator : IValidator<CreateProductCommand>
    {
        public IValidationResult Validate(CreateProductCommand command)
        {
            return new ValidationResultBase(!String.IsNullOrEmpty(command.Name) && command.Category != null);
        }
    }
}
