using Neptuo.Bootstrap.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Processing
{
    public interface IBootstrapHandlerExecutor
    {
        void Execute(IBootstrapHandler handler);
    }
}
