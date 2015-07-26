using Neptuo.ComponentModel.Behaviors.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel.Behaviors
{
    /// <summary>
    /// Provides mappings between behavior interface contract and actual implementation type.
    /// </summary>
    public interface IBehaviorCollection
    {
        /// <summary>
        /// Adds provider for behaviors.
        /// </summary>
        /// <param name="provider">Behavior provider.</param>
        /// <returns>Self (for fluency).</returns>
        IBehaviorCollection Add(IBehaviorProvider provider);

        /// <summary>
        /// Gets registered behaviors for handler of type <paramref name="handlerType"/>.
        /// </summary>
        /// <param name="handlerType">Handler type.</param>
        /// <returns>Registered behaviors for handler of type <paramref name="handlerType"/>.</returns>
        IEnumerable<Type> GetBehaviors(Type handlerType);
    }
}
