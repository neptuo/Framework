using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel.Behaviors.Providers
{
    /// <summary>
    /// Provider for behaviors.
    /// </summary>
    public interface IBehaviorProvider
    {
        /// <summary>
        /// Returns registered behaviors for handler of type <paramref name="handlerType"/>.
        /// </summary>
        /// <param name="handlerType">Handler type.</param>
        /// <returns>Registered behaviors for handler of type <paramref name="handlerType"/>.</returns>
        IEnumerable<Type> GetBehaviors(Type handlerType);
    }
}
