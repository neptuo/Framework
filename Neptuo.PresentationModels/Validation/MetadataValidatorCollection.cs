using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Validation
{
    /// <summary>
    /// Implementation of <see cref="IMetadataValidatorCollection"/>.
    /// </summary>
    public class MetadataValidatorCollection : IMetadataValidatorCollection
    {
        protected Dictionary<string, Dictionary<string, Dictionary<string, IFieldMetadataValidatorFactory>>> Validators { get; private set; }

        public MetadataValidatorCollection()
        {
            Validators = new Dictionary<string, Dictionary<string, Dictionary<string, IFieldMetadataValidatorFactory>>>();
        }

        /// <summary>
        /// Add validator factory for <paramref name="metadataKey"/> on field <paramref name="fieldIdentifier"/> of model <paramref name="modelIdentifier"/>.
        /// </summary>
        /// <param name="modelIdentifier">Identifier of model definition.</param>
        /// <param name="fieldIdentifier">Identifier of field definition.</param>
        /// <param name="metadataKey">Field metadata validator key.</param>
        /// <param name="validatorFactory">Factory for validators.</param>
        /// <returns>Self (for fluency).</returns>
        public MetadataValidatorCollection Add(string modelIdentifier, string fieldIdentifier, string metadataKey, IFieldMetadataValidatorFactory validatorFactory)
        {
            if (modelIdentifier == null)
                modelIdentifier = String.Empty;

            if (fieldIdentifier == null)
                fieldIdentifier = String.Empty;

            if (!Validators.ContainsKey(modelIdentifier))
                Validators[modelIdentifier] = new Dictionary<string, Dictionary<string, IFieldMetadataValidatorFactory>>();

            if (!Validators[modelIdentifier].ContainsKey(fieldIdentifier))
                Validators[modelIdentifier][fieldIdentifier] = new Dictionary<string, IFieldMetadataValidatorFactory>();

            Validators[modelIdentifier][fieldIdentifier][metadataKey] = validatorFactory;
            return this;
        }

        public bool TryGet(string modelIdentifier, string fieldIdentifier, string metadataKey, out IFieldMetadataValidator validator)
        {
            Dictionary<string, Dictionary<string, IFieldMetadataValidatorFactory>> modelValidators;
            if (!Validators.TryGetValue(modelIdentifier, out modelValidators) && !Validators.TryGetValue(String.Empty, out modelValidators))
            {
                validator = null;
                return false;
            }

            Dictionary<string, IFieldMetadataValidatorFactory> fieldValidators;
            if (!modelValidators.TryGetValue(fieldIdentifier, out fieldValidators) && !modelValidators.TryGetValue(String.Empty, out fieldValidators))
            {
                validator = null;
                return false;
            }

            IFieldMetadataValidatorFactory factory;
            if (!fieldValidators.TryGetValue(metadataKey, out factory) && !fieldValidators.TryGetValue(String.Empty, out factory))
            {
                validator = null;
                return false;
            }

            validator = factory.Create();
            return true;
        }
    }

    /// <summary>
    /// Extensions methods for <see cref="MetadataValidatorCollection"/>.
    /// </summary>
    public static class MetadataValidatorCollectionExtensions
    {
        /// <summary>
        /// Add validator for <paramref name="metadataKey"/> on field <paramref name="fieldIdentifier"/> of model <paramref name="modelIdentifier"/>.
        /// </summary>
        /// <param name="modelIdentifier">Identifier of model definition.</param>
        /// <param name="fieldIdentifier">Identifier of field definition.</param>
        /// <param name="metadataKey">Field metadata validator key.</param>
        /// <param name="validatorFactory">Factory for validators.</param>
        /// <returns>Self (for fluency).</returns>
        public static MetadataValidatorCollection Add(this MetadataValidatorCollection collection, string modelIdentifier, string fieldIdentifier, string metadataKey, IFieldMetadataValidator validator)
        {
            return collection.Add(modelIdentifier, fieldIdentifier, metadataKey, new SingletonFieldMetadataValidatorFactory(validator));
        }
    }
}
