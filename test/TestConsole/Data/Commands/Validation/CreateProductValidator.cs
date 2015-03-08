using Neptuo.Pipelines.Validators;
using Neptuo.Pipelines.Validators.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Data.Commands.Validation
{
    public class CreateProductValidator : IValidationHandler<CreateProductCommand>
    {
        public IValidationResult Handle(CreateProductCommand command)
        {
            return new ValidationResultBase(!String.IsNullOrEmpty(command.Name) && command.Category != null);
        }
    }
}
