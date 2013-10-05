using Neptuo.Data.Commands.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Commands
{
    public interface ICommandDispatcher
    {
        void Handle<TCommand>(TCommand command);

        TValidationResult Validate<TCommand, TValidationResult>(TCommand command)
            where TCommand : ICommandValidationResult<TValidationResult>
            where TValidationResult : IValidationResult;
    }
}
