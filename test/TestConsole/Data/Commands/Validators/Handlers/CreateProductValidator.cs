using Neptuo.Pipelines.Validators;
using Neptuo.Pipelines.Validators.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Data.Commands.Validators.Handlers
{
    public class CreateProductValidator : IValidationHandler<CreateProductCommand>
    {
        public IValidationResult Handle(CreateProductCommand command)
        {
            return new ValidationResult(!String.IsNullOrEmpty(command.Name) && command.Category != null);
        }
    }
}
