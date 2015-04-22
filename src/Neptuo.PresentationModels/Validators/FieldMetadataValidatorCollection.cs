using Neptuo.Activators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Validators
{
    /// <summary>
    /// Implementation of <see cref="IFieldMetadataValidatorCollection"/>.
    /// </summary>
    public class FieldMetadataValidatorCollection : IFieldMetadataValidatorCollection
    {
        private readonly Dictionary<string, Dictionary<string, Dictionary<string, IFieldMetadataValidator>>> singletons
            = new Dictionary<string, Dictionary<string, Dictionary<string, IFieldMetadataValidator>>>();

        private readonly Dictionary<string, Dictionary<string, Dictionary<string, IActivator<IFieldMetadataValidator>>>> builders
            = new Dictionary<string, Dictionary<string, Dictionary<string, IActivator<IFieldMetadataValidator>>>>();

        private void AddInternal<T>(Dictionary<string, Dictionary<string, Dictionary<string, T>>> storage, string modelIdentifier, string fieldIdentifier, string metadataKey, T validator)
        {
            if (modelIdentifier == null)
                modelIdentifier = String.Empty;

            if (fieldIdentifier == null)
                fieldIdentifier = String.Empty;

            if (!singletons.ContainsKey(modelIdentifier))
                storage[modelIdentifier] = new Dictionary<string, Dictionary<string, T>>();

            if (!singletons[modelIdentifier].ContainsKey(fieldIdentifier))
                storage[modelIdentifier][fieldIdentifier] = new Dictionary<string, T>();

            storage[modelIdentifier][fieldIdentifier][metadataKey] = validator;
        }

        /// <summary>
        /// Add validator for <paramref name="metadataKey"/> on field <paramref name="fieldIdentifier"/> of model <paramref name="modelIdentifier"/>.
        /// </summary>
        /// <param name="modelIdentifier">Identifier of model definition.</param>
        /// <param name="fieldIdentifier">Identifier of field definition.</param>
        /// <param name="metadataKey">Field metadata validator key.</param>
        /// <param name="validator">Validator.</param>
        /// <returns>Self (for fluency).</returns>
        public FieldMetadataValidatorCollection Add(string modelIdentifier, string fieldIdentifier, string metadataKey, IFieldMetadataValidator validator)
        {
            AddInternal(singletons, modelIdentifier, fieldIdentifier, metadataKey, validator);
            return this;
        }

        /// <summary>
        /// Add validator factory for <paramref name="metadataKey"/> on field <paramref name="fieldIdentifier"/> of model <paramref name="modelIdentifier"/>.
        /// </summary>
        /// <param name="modelIdentifier">Identifier of model definition.</param>
        /// <param name="fieldIdentifier">Identifier of field definition.</param>
        /// <param name="metadataKey">Field metadata validator key.</param>
        /// <param name="validator">Validator factory.</param>
        /// <returns>Self (for fluency).</returns>
        public FieldMetadataValidatorCollection Add(string modelIdentifier, string fieldIdentifier, string metadataKey, IActivator<IFieldMetadataValidator> validator)
        {
            AddInternal(builders, modelIdentifier, fieldIdentifier, metadataKey, validator);
            return this;
        }

        private bool TryGetInternal<T>(Dictionary<string, Dictionary<string, Dictionary<string, T>>> storage, string modelIdentifier, string fieldIdentifier, string metadataKey, out T validator)
        {
            Dictionary<string, Dictionary<string, T>> modelValidators;
            if (storage.TryGetValue(modelIdentifier, out modelValidators) || storage.TryGetValue(String.Empty, out modelValidators))
            {
                Dictionary<string, T> fieldValidators;
                if (modelValidators.TryGetValue(fieldIdentifier, out fieldValidators) || modelValidators.TryGetValue(String.Empty, out fieldValidators))
                {
                    if (fieldValidators.TryGetValue(metadataKey, out validator) || fieldValidators.TryGetValue(String.Empty, out validator))
                        return true;
                }
            }

            validator = default(T);
            return false;
        }

        public bool TryGet(string modelIdentifier, string fieldIdentifier, string metadataKey, out IFieldMetadataValidator validator)
        {
            if (TryGetInternal(singletons, modelIdentifier, fieldIdentifier, metadataKey, out validator))
                return true;

            IActivator<IFieldMetadataValidator> builder;
            if (TryGetInternal(builders, modelIdentifier, fieldIdentifier, metadataKey, out builder))
            {
                validator = builder.Create();
                return true;
            }

            validator = null;
            return false;
        }
    }
}
