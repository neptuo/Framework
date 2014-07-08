using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Behaviors.Providers
{
    /// <summary>
    /// Provides behavior based on implemented interface.
    /// </summary>
    public class InterfaceBehaviorProvider : IBehaviorProvider
    {
        /// <summary>
        /// Behavior interface contract.
        /// </summary>
        private Type behaviorContract;

        /// <summary>
        /// Behavior contract implementor.
        /// </summary>
        private Type behaviorImplementation;

        /// <summary>
        /// Creates new instance with <paramref name="behaviorContract"/> as contract and <paramref name="behaviorImplementation"/> as implementation type.
        /// </summary>
        /// <param name="behaviorContract">Behavior interface contract.</param>
        /// <param name="behaviorImplementation">Behavior contract implementor.</param>
        public InterfaceBehaviorProvider(Type behaviorContract, Type behaviorImplementation)
        {
            Guard.NotNull(behaviorContract, "behaviorContract");
            Guard.NotNull(behaviorImplementation, "behaviorImplementation");
            this.behaviorContract = behaviorContract;
            this.behaviorImplementation = behaviorImplementation;
        }

        /// <summary>
        /// Returns <see cref="behaviorImplementation"/> if <paramref name="handlerType"/> implements <see cref="behaviorContract"/>; empty enumeration otherwise.
        /// </summary>
        /// <param name="handlerType">Handler type.</param>
        /// <returns><see cref="behaviorImplementation"/> if <paramref name="handlerType"/> implements <see cref="behaviorContract"/>; empty enumeration otherwise.</returns>
        public IEnumerable<Type> GetBehaviors(Type handlerType)
        {
            Guard.NotNull(handlerType, "handlerType");
            foreach (Type interfaceType in handlerType.GetInterfaces())
            {
                if(interfaceType == behaviorContract)
                {
                    yield return behaviorImplementation;
                    break;
                }

                // If contract is generic, try to find generic registration.
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == behaviorContract)
                {
                    // If implementation type is generic, pass generic arguments from contract to implementation.
                    if (behaviorImplementation.IsGenericType)
                        yield return behaviorImplementation.MakeGenericType(interfaceType.GetGenericArguments());
                    else
                        yield return behaviorImplementation;

                    break;
                }
            }
        }
    }
}
