using Neptuo.Activators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels
{
    /// <summary>
    /// Reflection based factory for creating <see cref="IModelDefinition"/> from .NET types.
    /// - Fields are created from properties.
    /// - Metadata are created from attributes.
    /// </summary>
    public class ReflectionModelDefinitionFactory : IFactory<IModelDefinition, Type>
    {
        /// <summary>
        /// Collection attribute metadata readers.
        /// </summary>
        public AttributeMetadataReaderCollection MetadataReaderCollection { get; private set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="metadataReaderCollection">Collection attribute metadata readers.</param>
        public ReflectionModelDefinitionFactory(AttributeMetadataReaderCollection metadataReaderCollection)
        {
            Ensure.NotNull(metadataReaderCollection, "metadataReaderCollection");
            MetadataReaderCollection = metadataReaderCollection;
        }

        /// <summary>
        /// Creates model definition for <see cref="modelType"/>.
        /// </summary>
        /// <param name="modelType">Type to create model definition from.</param>
        /// <returns>Model definition for <see cref="modelType"/>.</returns>
        public IModelDefinition Create(Type modelType)
        {
            ReflectionModelDefinitionBuilder builder = new ReflectionModelDefinitionBuilder(modelType, MetadataReaderCollection);
            return builder.Create();
        }
    }
}
