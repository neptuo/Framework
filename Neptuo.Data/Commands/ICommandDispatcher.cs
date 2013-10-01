using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Commands
{
    public interface ICommandDispatcher
    {
        void Handle<TCommand>(TCommand command) 
            where TCommand : ICommand;


    }
}
