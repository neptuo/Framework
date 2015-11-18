using Neptuo.Validators;
using Neptuo.Validators.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Data.Commands.Validators.Handlers
{
    public class CreateProductValidator : IValidationHandler<CreateProductCommand>
    {
        public Task<IValidationResult> HandleAsync(CreateProductCommand command)
        {
            return Task.FromResult<IValidationResult>(
                new ValidationResult(!String.IsNullOrEmpty(command.Name) && command.Category != null)
            );
        }
    }
}
