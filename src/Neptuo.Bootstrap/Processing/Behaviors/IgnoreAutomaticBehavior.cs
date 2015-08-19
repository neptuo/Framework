using Neptuo.Behaviors;
using Neptuo.Bootstrap.Handlers;
using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Processing.Behaviors
{
    /// <summary>
    /// Behavior implementation for ignoring handlers that should be executed in automatic bootstrappers.
    /// Should included only in those handlers.
    /// Reads <c>IsAutomatic</c> from context values to see whether is automatic bootstrap or not.
    /// </summary>
    public class IgnoreAutomaticBehavior : IBehavior<IBootstrapHandler>
    {
        public Task ExecuteAsync(IBootstrapHandler handler, IBehaviorContext context)
        {
            if (context.CustomValues.Get<bool>("IsAutomatic", false))
                return Task.FromResult(false);

            return context.NextAsync();
        }
    }
}
