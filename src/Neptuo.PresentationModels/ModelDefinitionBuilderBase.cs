using Neptuo.Activators;
using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    /// <summary>
    /// Base implementation for building <see cref="IModelDefinition"/>.
    /// </summary>
    public abstract class ModelDefinitionBuilderBase : IFactory<IModelDefinition>
    {
        /// <summary>
        /// Provides model identifier.
        /// </summary>
        /// <returns>Model identifier.</returns>
        protected abstract string BuildModelIdentifier();

        /// <summary>
        /// Provides field definitions.
        /// </summary>
        /// <returns>Field definitions.</returns>
        protected abstract IEnumerable<IFieldDefinition> BuildFieldDefinitions();

        /// <summary>
        /// Provides model metadata.
        /// </summary>
        /// <returns>Model metadata.</returns>
        protected abstract IKeyValueCollection BuildModelMetadata();

        /// <summary>
        /// Builds model definition using <see cref="ModelDefinition"/>.
        /// </summary>
        /// <returns>Model definition.</returns>
        public IModelDefinition Create()
        {
            return new ModelDefinition(BuildModelIdentifier(), BuildFieldDefinitions(), BuildModelMetadata());
        }
    }
}
