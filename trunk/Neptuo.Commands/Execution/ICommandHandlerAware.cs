using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands.Execution
{
    public interface ICommandHandlerAware
    {
        object CommandHandler { get; }
        object Command { get; }
    }
}
