﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands.Handlers
{
    public interface ICommandHandlerFactory<TCommand>
    {
        ICommandHandler<TCommand> CreateHandler();
    }
}
