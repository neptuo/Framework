using Neptuo.Behaviors;
using Neptuo.Bootstrap.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Processing.Behaviors
{
    public class InitializeBehavior : IBehavior<IBootstrapHandler>
    {
        public Task ExecuteAsync(IBootstrapHandler handler, IBehaviorContext context)
        {
            handler.Handle();
            return Task.FromResult(true);
        }
    }
}
