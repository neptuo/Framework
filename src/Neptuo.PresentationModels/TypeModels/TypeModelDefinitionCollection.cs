using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels
{
    /// <summary>
    /// Collection of model definitions by model type.
    /// This class is thread-safe.
    /// </summary>
    public class TypeModelDefinitionCollection
    {
        private readonly object storageLock = new object();

        private readonly Dictionary<Type, IModelDefinition> singletons = new Dictionary<Type, IModelDefinition>();
        private readonly OutFuncCollection<Type, IModelDefinition, bool> onSearchDefinition = new OutFuncCollection<Type, IModelDefinition, bool>();

        /// <summary>
        /// Registers instance of model definition to be mapped to <paramref name="modelType" />.
        /// </summary>
        /// <param name="modelType">Type to register model definition to.</param>
        /// <param name="modelDefinition">Model definition to register.</param>
        /// <returns>Self (for fluency).</returns>
        public TypeModelDefinitionCollection Add(Type modelType, IModelDefinition modelDefinition)
        {
            Ensure.NotNull(modelType, "modelType");
            Ensure.NotNull(modelDefinition, "modelDefinition");

            lock (storageLock)
            {
                singletons[modelType] = modelDefinition;
            }

            return this;
        }

        /// <summary>
        /// Adds <paramref name="searchHandler"/> to be executed when model definition was not found.
        /// </summary>
        /// <param name="searchHandler">Model definition provider method.</param>
        public TypeModelDefinitionCollection AddSearchHandler(OutFunc<Type, IModelDefinition, bool> searchHandler)
        {
            Ensure.NotNull(searchHandler, "searchHandler");

            lock (storageLock)
            {
                onSearchDefinition.Add(searchHandler);
            }

            return this;
        }

        /// <summary>
        /// Tries to get model definition for type <paramref name="modelType"/>.
        /// </summary>
        /// <param name="modelType">Type of requested model definition.</param>
        /// <param name="modelDefinition">Model definition.</param>
        /// <returns><c>true</c> if model was found; <c>false</c> otherwise.</returns>
        public bool TryGet(Type modelType, out IModelDefinition modelDefinition)
        {
            Ensure.NotNull(modelType, "modelType");

            // Search in singletons.
            if (singletons.TryGetValue(modelType, out modelDefinition))
                return true;

            lock (storageLock)
            {
                // Search using search handlers.
                if (onSearchDefinition.TryExecute(modelType, out modelDefinition))
                {
                    singletons[modelType] = modelDefinition;
                    return true;
                }
            }

            // Unnable to find model definition.
            modelDefinition = null;
            return false;
        }
    }
}
