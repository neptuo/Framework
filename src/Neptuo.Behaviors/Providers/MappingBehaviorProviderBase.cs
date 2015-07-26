using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors.Providers
{
    /// <summary>
    /// Base implementation of <see cref="IBehaviorProvider"/> based on static mapping 
    /// between behavior contract and its implementation.
    /// </summary>
    public abstract class MappingBehaviorProviderBase : IBehaviorProvider
    {
        private readonly object storageLock = new object();
        private readonly List<BehaviorMappingModel> storage = new List<BehaviorMappingModel>();

        /// <summary>
        /// If uderlaying storage doesn't contain <paramref name="behaviorContract"/>, new mapping is inserted at index <paramref name="index"/>;
        /// otherwise existing registration is updated and re-inserted at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">Intex to insert mapping at (if <c>null</c>, mapping will be insterted at last index).</param>
        /// <param name="behaviorContract">Behavior contract type.</param>
        /// <param name="behaviorImplementation">Behavior implementation type.</param>
        protected void InsertOrUpdateMappingInternal(int? index, Type behaviorContract, Type behaviorImplementation)
        {
            Ensure.NotNull(behaviorContract, "behaviorContract");
            Ensure.NotNull(behaviorImplementation, "behaviorImplementation");

            BehaviorMappingModel model = storage.FirstOrDefault(m => m.Contract == behaviorContract);
            if (model != null)
                storage.Remove(model);

            model = new BehaviorMappingModel(behaviorContract, behaviorImplementation);

            lock (storageLock)
            {
                if (index != null)
                    storage.Insert(index.Value, model);
                else
                    storage.Add(model);
            }
        }

        /// <summary>
        /// Calls <see cref="MappingBehaviorProviderBase.GetBehaviors"/> to get behavior implementations for <paramref name="handlerType"/>.
        /// </summary>
        /// <param name="handlerType">Service handler type.</param>
        /// <returns>Enumeration of behavior implementations for <paramref name="handlerType"/>.</returns>
        public IEnumerable<Type> GetBehaviors(Type handlerType)
        {
            Ensure.NotNull(handlerType, "handlerType");
            return GetBehaviorInternal(handlerType, storage);
        }

        /// <summary>
        /// Calls <see cref="MappingBehaviorProviderBase.FindBehaviorContracts"/> and than enumerates registrations for finding implementations.
        /// If result of <see cref="MappingBehaviorProviderBase.FindBehaviorContracts"/> contains any unregistered behavior, these will be skipped.
        /// </summary>
        /// <param name="handlerType">Service handler type.</param>
        /// <param name="storage">Behavior mapping registrations.</param>
        /// <returns>Enumeration of behavior implementations for <paramref name="handlerType"/>.</returns>
        protected virtual IEnumerable<Type> GetBehaviorInternal(Type handlerType, List<BehaviorMappingModel> storage)
        {
            List<Type> behaviors = new List<Type>();
            IEnumerable<Type> behaviorContracts = FindBehaviorContracts(handlerType);
            IEnumerable<Type> behaviorImplementations = storage
                .Where(m => behaviorContracts.Contains(m.Contract))
                .Select(m => m.Implementation);

            return behaviorImplementations;
        }

        /// <summary>
        /// In derivered class should return list of behavior definition types for <paramref name="handlerType"/>.
        /// </summary>
        /// <param name="handlerType">Service handler type.</param>
        /// <returns>List of behavior definition types for <paramref name="handlerType"/>.</returns>
        protected virtual IEnumerable<Type> FindBehaviorContracts(Type handlerType)
        {
            return Enumerable.Empty<Type>();
        }

        /// <summary>
        /// Single mapping item.
        /// </summary>
        public class BehaviorMappingModel
        {
            /// <summary>
            /// Behavior contract type.
            /// </summary>
            public Type Contract { get; private set; }

            /// <summary>
            /// Behavior implementation type.
            /// </summary>
            public Type Implementation { get; private set; }

            /// <summary>
            /// Creates new instance.
            /// </summary>
            /// <param name="contract">Behavior contract type.</param>
            /// <param name="implementation">Behavior implementation type.</param>
            public BehaviorMappingModel(Type contract, Type implementation)
            {
                Ensure.NotNull(contract, "contract");
                Ensure.NotNull(implementation, "implementation");
                Contract = contract;
                Implementation = implementation;
            }

            /// <summary>
            /// Updates implementation type to <paramref name="implementation"/>.
            /// </summary>
            /// <param name="implementation">Behavior implementation type.</param>
            /// <returns>Self (for fluency).</returns>
            public BehaviorMappingModel UpdateImplementation(Type implementation)
            {
                Ensure.NotNull(implementation, "implementation");
                Implementation = implementation;
                return this;
            }
        }
    }
}
