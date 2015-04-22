using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels
{
    /// <summary>
    /// Common extensions for <see cref="TypeModelDefinitionCollection"/>.
    /// </summary>
    public static class _TypeModelDefinitionCollectionExtensions
    {
        /// <summary>
        /// Registers default search handler that for all types creates model definition by <see cref="ReflectionModelDefinitionBuilder"/>.
        /// </summary>
        /// <param name="collection">Model definition collection to register search handler to.</param>
        /// <param name="metadataReaders">Collection of metadata readers for <see cref="ReflectionModelDefinitionBuilder"/>.</param>
        /// <returns></returns>
        public static TypeModelDefinitionCollection AddReflectionSearchHandler(this TypeModelDefinitionCollection collection, AttributeMetadataReaderCollection metadataReaders)
        {
            Ensure.NotNull(collection, "collection");
            Ensure.NotNull(metadataReaders, "metadataReaders");
            collection.AddSearchHandler((Type type, out IModelDefinition model) =>
            {
                model = new ReflectionModelDefinitionBuilder(type, metadataReaders).Create();
                return true;
            });
            return collection;
        }

        /// <summary>
        /// Returns model definition for type <paramref name="modelType"/>.
        /// If <paramref name="collection"/> doesn't contain such definition, throws <see cref="ArgumentOutOfRangeException"/>.
        /// </summary>
        /// <param name="collection">Model definition collection to get model definition from.</param>
        /// <param name="modelType">Associated model type to requested model definition.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">When <paramref name="collection"/> doesn't contain model definition for <see cref="modelType"/>.</exception>
        public static IModelDefinition Get(this TypeModelDefinitionCollection collection, Type modelType)
        {
            Ensure.NotNull(collection, "collection");
            Ensure.NotNull(modelType, "modelType");

            IModelDefinition modelDefinition;
            if (collection.TryGet(modelType, out modelDefinition))
                return modelDefinition;

            throw Ensure.Exception.ArgumentOutOfRange("modelType", "Unnable to get model definition for type '{0}'.", modelType.FullName);
        }

        /// <summary>
        /// Returns model definition for type <typeparamref name="TModelType"/>.
        /// If <paramref name="collection"/> doesn't contain such definition, throws <see cref="ArgumentOutOfRangeException"/>.
        /// </summary>
        /// <typeparam name="TModelType">Associated model type to requested model definition.</typeparam>
        /// <param name="collection">Model definition collection to get model definition from.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">When <paramref name="collection"/> doesn't contain model definition for <typeparamref name="TModelType"/>.</exception>
        public static IModelDefinition Get<TModelType>(this TypeModelDefinitionCollection collection)
        {
            return Get(collection, typeof(TModelType));
        }
    }
}
