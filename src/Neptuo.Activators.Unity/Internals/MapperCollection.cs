using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators.Internals
{
    /// <summary>
    /// Collection of definitions that should be passed to the child scopes.
    /// Contains only NAME SCOPED definitions.
    /// </summary>
    internal class MapperCollection
    {
        private readonly MapperCollection parentCollection;
        private readonly ConcurrentDictionary<string, List<DependencyDefinition>> storage = new ConcurrentDictionary<string, List<DependencyDefinition>>();

        /// <summary>
        /// Creates root collection.
        /// </summary>
        public MapperCollection()
        { }

        /// <summary>
        /// Creates collection that inherits from <paramref name="parentCollection"/>.
        /// </summary>
        /// <param name="parentCollection">Parent collectio of definitions.</param>
        public MapperCollection(MapperCollection parentCollection)
        { 
            Ensure.NotNull(parentCollection, "parentCollection");
            this.parentCollection = parentCollection;
        }

        /// <summary>
        /// Adds NAME SCOPED dependency definition.
        /// </summary>
        /// <param name="definition">NAME SCOPED definition to add.</param>
        /// <returns>Self (for fluency).</returns>
        /// <exception cref="InvalidOperationException">If <paramref name="definition"/> is not scoped.</exception>
        internal MapperCollection Add(DependencyDefinition definition)
        {
            string scopeName;
            if (definition.Lifetime.IsNamed)
                scopeName = definition.Lifetime.Name;
            else
                throw Ensure.Exception.InvalidOperation("MappingCollection supports only scoped or name-scoped registrations.");

            List<DependencyDefinition> models;
            if (!storage.TryGetValue(scopeName, out models))
                storage[scopeName] = models = new List<DependencyDefinition>();

            models.Add(definition);
            return this;
        }

        /// <summary>
        /// Tries to return all definitions bound to the <paramref name="scopeName"/>.
        /// </summary>
        /// <param name="scopeName">Scope name to find definitions for.</param>
        /// <param name="definitions">Enumerations for scoped definitions for <paramref name="scopeName"/>.</param>
        /// <returns><c>true</c> if such definitions exits (at least one); <c>false</c> otherwise.</returns>
        public bool TryGet(string scopeName, out IEnumerable<DependencyDefinition> definitions)
        {
            List<DependencyDefinition> result;
            if (storage.TryGetValue(scopeName, out result))
            {
                definitions = result;

                IEnumerable<DependencyDefinition> parentResult;
                if (parentCollection != null && parentCollection.TryGet(scopeName, out parentResult))
                    definitions = Enumerable.Concat(result, parentResult);

                return true;
            }

            if (parentCollection != null)
                return parentCollection.TryGet(scopeName, out definitions);

            definitions = null;
            return false;
        }
    }
}
