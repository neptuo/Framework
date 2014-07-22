using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Behaviors.Providers
{
    /// <summary>
    /// Provides behavior based on implemented interfaces.
    /// </summary>
    public class InterfaceBehaviorProvider : IBehaviorProvider
    {
        private Dictionary<Type, Type> storage;

        /// <summary>
        /// Creates new instance with <paramref name="behaviorContract"/> as contract and <paramref name="behaviorImplementation"/> as implementation type.
        /// </summary>
        /// <param name="behaviorContract">Behavior interface contract.</param>
        /// <param name="behaviorImplementation">Behavior contract implementor.</param>
        public InterfaceBehaviorProvider(Type behaviorContract, Type behaviorImplementation)
        {
            Guard.NotNull(behaviorContract, "behaviorContract");
            Guard.NotNull(behaviorImplementation, "behaviorImplementation");

            storage = new Dictionary<Type, Type>();
            storage[behaviorContract] = behaviorImplementation;
        }

        /// <summary>
        /// Creates new instance from <paramref name="storage"/>.
        /// </summary>
        /// <param name="storage">Mapping between constracts and implementations.</param>
        public InterfaceBehaviorProvider(Dictionary<Type, Type> storage)
        {
            Guard.NotNull(storage, "storage");
            this.storage = storage;
        }

        /// <summary>
        /// Adds mapping with <paramref name="behaviorContract"/> as contract and <paramref name="behaviorImplementation"/> as implementation type.
        /// </summary>
        /// <param name="behaviorContract">Behavior interface contract.</param>
        /// <param name="behaviorImplementation">Behavior contract implementor.</param>
        /// <returns>Self (for fluency).</returns>
        public InterfaceBehaviorProvider AddMapping(Type behaviorContract, Type behaviorImplementation)
        {
            Guard.NotNull(behaviorContract, "behaviorContract");
            Guard.NotNull(behaviorImplementation, "behaviorImplementation");
            storage[behaviorContract] = behaviorImplementation;
            return this;
        }

        /// <summary>
        /// Returns <see cref="behaviorImplementation"/> if <paramref name="handlerType"/> implements <see cref="behaviorContract"/>; empty enumeration otherwise.
        /// </summary>
        /// <param name="handlerType">Handler type.</param>
        /// <returns><see cref="behaviorImplementation"/> if <paramref name="handlerType"/> implements <see cref="behaviorContract"/>; empty enumeration otherwise.</returns>
        public IEnumerable<Type> GetBehaviors(Type handlerType)
        {
            Guard.NotNull(handlerType, "handlerType");
            Type behaviorImplementation;
            foreach (Type interfaceType in handlerType.GetInterfaces())
            {
                if (storage.TryGetValue(interfaceType, out behaviorImplementation))
                    yield return behaviorImplementation;

                // If contract is generic, try to find generic registration.
                if (interfaceType.IsGenericType)
                {
                    if (storage.TryGetValue(interfaceType.GetGenericTypeDefinition(), out behaviorImplementation))
                    {
                        // If implementation type is generic, pass generic arguments from contract to implementation.
                        if (behaviorImplementation.IsGenericType)
                            behaviorImplementation = behaviorImplementation.MakeGenericType(interfaceType.GetGenericArguments());

                        yield return behaviorImplementation;
                    }
                }
            }
        }
    }
}
