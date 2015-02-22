using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel.Behaviors.Providers
{
    /// <summary>
    /// Base implementation of <see cref="IBehaviorProvider"/> based on static mapping 
    /// between behavior contract and its implementation.
    /// </summary>
    public abstract class MappingBehaviorProviderBase : IBehaviorProvider
    {
        private Dictionary<Type, Type> storage;

        /// <summary>
        /// Creates new instance with empty mappings.
        /// </summary>
        protected MappingBehaviorProviderBase()
            : this(new Dictionary<Type, Type>())
        { }

        /// <summary>
        /// Creates new instance from <paramref name="storage"/>.
        /// </summary>
        /// <param name="storage">Mapping between constracts and implementations.</param>
        protected MappingBehaviorProviderBase(Dictionary<Type, Type> storage)
        {
            Guard.NotNull(storage, "storage");
            this.storage = storage;
        }

        /// <summary>
        /// Maps <paramref name="behaviorContract"/> to implementation <paramref name="behaviorImplementation"/>.
        /// </summary>
        /// <param name="behaviorContract">Behavior definition type.</param>
        /// <param name="behaviorImplementation">Behavior implementation type.</param>
        protected void AddMappingInternal(Type behaviorContract, Type behaviorImplementation)
        {
            Guard.NotNull(behaviorContract, "behaviorContract");
            Guard.NotNull(behaviorImplementation, "behaviorImplementation");
            storage[behaviorContract] = behaviorImplementation;
        }

        /// <summary>
        /// Calls <see cref="MappingBehaviorProviderBase.GetBehaviors"/> to get behavior implementations for <paramref name="handlerType"/>.
        /// </summary>
        /// <param name="handlerType">Service handler type.</param>
        /// <returns>Enumeration of behavior implementations for <paramref name="handlerType"/>.</returns>
        public IEnumerable<Type> GetBehaviors(Type handlerType)
        {
            Guard.NotNull(handlerType, "handlerType");
            return GetBehaviorInternal(handlerType, storage);
        }

        /// <summary>
        /// Calls <see cref="MappingBehaviorProviderBase.FindBehaviors"/> and than enumerates registrations for finding implementations.
        /// If result of <see cref="MappingBehaviorProviderBase.FindBehaviors"/> contains any unregistered behavior, these will be skipped.
        /// </summary>
        /// <param name="handlerType">Service handler type.</param>
        /// <param name="storage">Behavior mapping registrations.</param>
        /// <returns>Enumeration of behavior implementations for <paramref name="handlerType"/>.</returns>
        protected virtual IEnumerable<Type> GetBehaviorInternal(Type handlerType, Dictionary<Type, Type> storage)
        {
            List<Type> behaviors = new List<Type>();
            IEnumerable<Type> behaviorContracts = FindBehaviors(handlerType);
            IEnumerable<Type> behaviorImplementations = behaviorContracts
                .Where(b => storage.ContainsKey(b))
                .Select(b => storage[b]);

            return behaviorImplementations;
        }

        /// <summary>
        /// In derivered class should return list of behavior definition types for <paramref name="handlerType"/>.
        /// </summary>
        /// <param name="handlerType">Service handler type.</param>
        /// <returns>List of behavior definition types for <paramref name="handlerType"/>.</returns>
        protected virtual IEnumerable<Type> FindBehaviors(Type handlerType)
        {
            return Enumerable.Empty<Type>();
        }
    }
}
