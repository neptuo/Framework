﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Behaviors
{
    /// <summary>
    /// Base implementation using dictionary.
    /// </summary>
    public class BehaviorCollectionBase : IBehaviorCollection, IBehaviorContractSorter, IComparer<Type>
    {
        /// <summary>
        /// Internal mapping storage.
        /// </summary>
        private readonly Dictionary<Type, Type> storage = new Dictionary<Type, Type>();

        /// <summary>
        /// Adds mapping between <paramref name="behaviorContract"/> and <paramref name="behaviorImplementation"/>.
        /// If one already exists, it is overriden.
        /// </summary>
        public void Add(Type behaviorContract, Type behaviorImplementation)
        {
            Guard.NotNull(behaviorContract, "behaviorContract");
            Guard.NotNull(behaviorImplementation, "behaviorImplementation");
            storage[behaviorContract] = behaviorImplementation;
        }

        /// <summary>
        /// Tries to find <paramref name="behaviorImplementation"/> based on <paramref name="behaviorContract"/> or its generic type.
        /// </summary>
        public bool TryGet(Type behaviorContract, out Type behaviorImplementation)
        {
            Guard.NotNull(behaviorContract, "behaviorContract");
            if (storage.TryGetValue(behaviorContract, out behaviorImplementation))
                return true;

            // If contract is generic, try to find generic registration.
            if (behaviorContract.IsGenericType)
            {
                if (storage.TryGetValue(behaviorContract.GetGenericTypeDefinition(), out behaviorImplementation))
                {
                    // If implementation type is generic, pass generic arguments from contract to implementation.
                    if (behaviorImplementation.IsGenericType)
                        behaviorImplementation.MakeGenericType(behaviorContract.GetGenericArguments());

                    return true;
                }
            }

            // No mapping for contract.
            behaviorImplementation = null;
            return false;
        }

        /// <summary>
        /// Compares behavior contracts according to registration order.
        /// </summary>
        public int Compare(Type x, Type y)
        {
            int indexX = -1;
            int indexY = -1;
            int index = 0;
            foreach (Type behavior in storage.Keys)
            {
                if (behavior == x)
                    indexX = index;

                if (behavior == y)
                    indexY = index;

                if (indexX > 0 && indexY > 0)
                    return indexX > indexY ? 1 : -1;

                index++;
            }

            return 0;
        }

        public IEnumerable<Type> SortBehaviors(IEnumerable<Type> behaviorContracts)
        {
            List<Type> sorted = new List<Type>(behaviorContracts);
            sorted.Sort(this);
            return sorted;
        }
    }
}