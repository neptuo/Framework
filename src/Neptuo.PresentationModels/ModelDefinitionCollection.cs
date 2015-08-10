using Neptuo.Activators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Activation;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    /// <summary>
    /// Collection of registered model definitions.
    /// This class is thread-safe.
    /// </summary>
    public class ModelDefinitionCollection
    {
        private readonly object storageLock = new object();

        private readonly Dictionary<string, IModelDefinition> singletons = new Dictionary<string, IModelDefinition>();
        private readonly Dictionary<string, IFactory<IModelDefinition>> builders = new Dictionary<string, IFactory<IModelDefinition>>();
        private readonly OutFuncCollection<string, IModelDefinition, bool> onSearchDefinition = new OutFuncCollection<string, IModelDefinition, bool>();

        /// <summary>
        /// Registers instance of model definition.
        /// </summary>
        /// <param name="modelDefinition">Model definition to register.</param>
        /// <returns>Self (for fluency).</returns>
        public ModelDefinitionCollection Add(IModelDefinition modelDefinition)
        {
            Ensure.NotNull(modelDefinition, "modelDefinition");

            lock (storageLock)
            {
                singletons[modelDefinition.Identifier] = modelDefinition;
            }

            return this;
        }

        /// <summary>
        /// Registers builder for model definition with identifier <paramref name="modelIdentifier" />.
        /// </summary>
        /// <param name="modelIdentifier">Identifier to register <paramref name="modelDefinitionBuilder"/> with.</param>
        /// <param name="modelDefinitionBuilder">Model definition builder used to create singleton instance.</param>
        /// <returns>Self (for fluency).</returns>
        public ModelDefinitionCollection Add(string modelIdentifier, IFactory<IModelDefinition> modelDefinitionBuilder)
        {
            Ensure.NotNullOrEmpty(modelIdentifier, "modelIdentifier");
            Ensure.NotNull(modelDefinitionBuilder, "modelDefinitionBuilder");
            
            lock (storageLock)
            {
                builders[modelIdentifier] = modelDefinitionBuilder;
            }
            
            return this;
        }

        /// <summary>
        /// Adds <paramref name="searchHandler"/> to be executed when model definition was not found.
        /// </summary>
        /// <param name="searchHandler">Model definition provider method.</param>
        public ModelDefinitionCollection AddSearchHandler(OutFunc<string, IModelDefinition, bool> searchHandler)
        {
            Ensure.NotNull(searchHandler, "searchHandler");

            lock (storageLock)
            {
                onSearchDefinition.Add(searchHandler);
            }

            return this;
        }

        /// <summary>
        /// Tries to get model definition for identifier <paramref name="modelIdentifier"/>.
        /// </summary>
        /// <param name="modelIdentifier">Identifier of requested model definition.</param>
        /// <param name="modelDefinition">Model definition.</param>
        /// <returns><c>true</c> if model was found; <c>false</c> otherwise.</returns>
        public bool TryGet(string modelIdentifier, out IModelDefinition modelDefinition)
        {
            Ensure.NotNullOrEmpty(modelIdentifier, "modelIdentifier");

            // Search in singletons.
            if (singletons.TryGetValue(modelIdentifier, out modelDefinition))
                return true;

            lock (storageLock)
            {
                // Search in builders.
                IFactory<IModelDefinition> builder;
                if (builders.TryGetValue(modelIdentifier, out builder))
                {
                    singletons[modelIdentifier] = modelDefinition = builder.Create();
                    return true;
                }

                // Search using search handlers.
                if (onSearchDefinition.TryExecute(modelIdentifier, out modelDefinition))
                {
                    singletons[modelIdentifier] = modelDefinition;
                    return true;
                }
            }

            // Unnable to find model definition.
            modelDefinition = null;
            return true;
        }
    }
}
