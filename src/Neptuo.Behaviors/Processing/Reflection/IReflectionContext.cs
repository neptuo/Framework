using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors.Processing.Reflection
{
    /// <summary>
    /// Base context for reflection providers.
    /// </summary>
    public interface IReflectionContext
    {
        /// <summary>
        /// Type of handler to wrap.
        /// </summary>
        Type HandlerType { get; }
    }
}
