using Neptuo.Commands.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands
{
    public class C1
    { }

    public class C2
    { }

    public class C3
    { }

    class C1Handler : ICommandHandler<C1>, ICommandHandler<C2>, ICommandHandler<C3>
    {
        public Task HandleAsync(C1 command)
        {
            throw new NotImplementedException();
        }

        public Task HandleAsync(C2 command)
        {
            throw new NotImplementedException();
        }

        public Task HandleAsync(C3 command)
        {
            throw new NotImplementedException();
        }
    }

}
