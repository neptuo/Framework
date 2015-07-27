using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors.Providers
{
    /// <summary>
    /// Collection of behaviors, that will be added to all handlers (aka static behaviors).
    /// </summary>
    public class GlobalBehaviorCollection : IBehaviorProvider
    {
        private readonly object storageLock = new object();
        private readonly List<Type> storage = new List<Type>();

        /// <summary>
        /// Adds <paramref name="behaviorType"/> to be added to all handlers.
        /// </summary>
        /// <param name="behaviorType">Type of behavior implementation.</param>
        /// <returns>Self (for fluency).</returns>
        public GlobalBehaviorCollection Add(Type behaviorType)
        {
            Ensure.NotNull(behaviorType, "behaviorType");

            lock (storageLock)
                storage.Add(behaviorType);

            return this;
        }

        public IEnumerable<Type> GetBehaviors(Type handlerType)
        {
            return storage.ToArray();
        }
    }
}
