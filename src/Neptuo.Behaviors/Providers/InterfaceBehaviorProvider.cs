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
            Ensure.NotNull(behaviorContract, "behaviorContract");
            Ensure.NotNull(behaviorImplementation, "behaviorImplementation");
            Add(behaviorContract, behaviorImplementation);
        }

        /// <summary>
        /// Adds mapping with <paramref name="behaviorContract"/> as contract and <paramref name="behaviorImplementation"/> as implementation type.
        /// </summary>
        /// <param name="behaviorContract">Behavior interface contract.</param>
        /// <param name="behaviorImplementation">Behavior contract implementor.</param>
        /// <returns>Self (for fluency).</returns>
        public InterfaceBehaviorProvider Add(Type behaviorContract, Type behaviorImplementation)
        {
            base.InsertOrUpdateMappingInternal(null, behaviorContract, behaviorImplementation);
            return this;
        }

        /// <summary>
        /// Adds mapping with <paramref name="behaviorContract"/> as contract and <paramref name="behaviorImplementation"/> as implementation type.
        /// </summary>
        /// <param name="index">Index at which to insert behavior mapping.</param>
        /// <param name="behaviorContract">Behavior interface contract.</param>
        /// <param name="behaviorImplementation">Behavior contract implementor.</param>
        /// <returns>Self (for fluency).</returns>
        public InterfaceBehaviorProvider Insert(int index, Type behaviorContract, Type behaviorImplementation)
        {
            base.InsertOrUpdateMappingInternal(index, behaviorContract, behaviorImplementation);
            return this;
        }

        /// <summary>
        /// Returns <see cref="behaviorImplementation"/> if <paramref name="handlerType"/> implements <see cref="behaviorContract"/>; empty enumeration otherwise.
        /// </summary>
        /// <param name="handlerType">Handler type.</param>
        /// <param name="storage">Contract-implementation mappings.</param>
        /// <returns><see cref="behaviorImplementation"/> if <paramref name="handlerType"/> implements <see cref="behaviorContract"/>; empty enumeration otherwise.</returns>
        protected override IEnumerable<Type> GetBehaviorInternal(Type handlerType, List<BehaviorMappingModel> storage)
        {
            List<Type> result = new List<Type>();

            IEnumerable<Type> usedInterfaces = handlerType.GetInterfaces();
            foreach (BehaviorMappingModel model in storage)
            {
                // If handler's interfaces contains contract, add it.
                if(usedInterfaces.Contains(model.Contract))
                {
                    result.Add(model.Implementation);
                }
                else if (model.Contract.IsGenericType)
                {
                    // Try to find all uses of generic contract.
                    IEnumerable<Type> usedGeneric = usedInterfaces
                        .Where(i => i.IsGenericType)
                        .Where(i => i.GetGenericTypeDefinition() == model.Contract.GetGenericTypeDefinition());

                    // If implementation is generic, add for each used contract implmenetation type bound to used generic arguments.
                    // If not, add implementation type once.
                    if (model.Implementation.IsGenericType)
                        result.AddRange(usedGeneric.Select(i => model.Implementation.MakeGenericType(i.GetGenericArguments())));
                    else if(usedGeneric.Any())
                        result.Add(model.Implementation);
                }
            }

            return result;
        }
    }
}
