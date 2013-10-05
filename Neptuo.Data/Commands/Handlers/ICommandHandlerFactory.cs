using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Commands.Handlers
{
    public interface ICommandHandlerFactory<T>
    {
        ICommandHandler<T> CreateHandler();
    }
}
