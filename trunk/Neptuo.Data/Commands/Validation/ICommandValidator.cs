using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Commands.Validation
{
    public interface ICommandValidator<TCommand, TResult>
        where TCommand : ICommand
        where TResult : IValidationResult
    {
        TResult Validate(TCommand command);
    }
}
