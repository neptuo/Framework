using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands
{
    public class ScalingCommandDispatcher : ICommandDispatcher
    {
        public Task HandleAsync<TCommand>(TCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
