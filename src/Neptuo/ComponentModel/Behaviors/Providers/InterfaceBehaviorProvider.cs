using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel.Behaviors.Providers
{
    /// <summary>
    /// Provides behavior based on implemented interfaces.
    /// </summary>
    public class InterfaceBehaviorProvider : MappingBehaviorProviderBase
    {
        /// <summary>
        /// Creates empty instance.
        /// </summary>
        public InterfaceBehaviorProvider()
        { }

        /// <summary>
        /// Creates new instance with <paramref name="behaviorContract"/> as contract and <paramref name="behaviorImplementation"/> as implementation type.
        /// </summary>
        /// <param name="behaviorContract">Behavior interface contract.</param>
        /// <param name="behaviorImplementation">Behavior contract implementor.</param>
        public InterfaceBehaviorProvider(Type behaviorContract, Type behaviorImplementation)
        {
            Guard.NotNull(behaviorContract, "behaviorContract");
            Guard.NotNull(behaviorImplementation, "behaviorImplementation");
            AddMapping(behaviorContract, behaviorImplementation);
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
            base.AddMappingInternal(behaviorContract, behaviorImplementation);
            return this;
        }

        /// <summary>
        /// Returns <see cref="behaviorImplementation"/> if <paramref name="handlerType"/> implements <see cref="behaviorContract"/>; empty enumeration otherwise.
        /// </summary>
        /// <param name="handlerType">Handler type.</param>
        /// <returns><see cref="behaviorImplementation"/> if <paramref name="handlerType"/> implements <see cref="behaviorContract"/>; empty enumeration otherwise.</returns>
        protected override IEnumerable<Type> GetBehaviorInternal(Type handlerType, Dictionary<Type, Type> storage)
        {
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
