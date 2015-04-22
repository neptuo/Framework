using Neptuo.Activators;
using Neptuo.ComponentModel;
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
        private readonly Dictionary<FieldMetadataValidatorKey, IFieldMetadataValidator> singletons
            = new Dictionary<FieldMetadataValidatorKey, IFieldMetadataValidator>();

        private readonly Dictionary<FieldMetadataValidatorKey, IActivator<IFieldMetadataValidator>> builders
            = new Dictionary<FieldMetadataValidatorKey, IActivator<IFieldMetadataValidator>>();

        private readonly OutFuncCollection<FieldMetadataValidatorKey, IFieldMetadataValidator, bool> onSearchValidator
            = new OutFuncCollection<FieldMetadataValidatorKey, IFieldMetadataValidator, bool>();

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
            singletons[new FieldMetadataValidatorKey(modelIdentifier, fieldIdentifier, metadataKey)] = validator;
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
            builders[new FieldMetadataValidatorKey(modelIdentifier, fieldIdentifier, metadataKey)] = validator;
            return this;
        }

        /// <summary>
        /// Adds <paramref name="searchHandler"/> to be executed when field metadata validator was not found.
        /// </summary>
        /// <param name="searchHandler">Field metadata validator method.</param>
        public FieldMetadataValidatorCollection AddSearchHandler(OutFunc<FieldMetadataValidatorKey, IFieldMetadataValidator, bool> searchHandler)
        {
            Ensure.NotNull(searchHandler, "searchHandler");
            onSearchValidator.Add(searchHandler);
            return this;
        }

        public bool TryGet(string modelIdentifier, string fieldIdentifier, string metadataKey, out IFieldMetadataValidator validator)
        {
            foreach (FieldMetadataValidatorKey key in LazyEnumerateKeys(modelIdentifier, fieldIdentifier, metadataKey))
            {
                if (singletons.TryGetValue(key, out validator))
                    return true;

                IActivator<IFieldMetadataValidator> builder;
                if (builders.TryGetValue(key, out builder))
                {
                    validator = builder.Create();
                    return true;
                }

                if (onSearchValidator.TryExecute(key, out validator))
                    return true;
            }

            validator = null;
            return false;
        }

        private IEnumerable<FieldMetadataValidatorKey> LazyEnumerateKeys(string modelIdentifier, string fieldIdentifier, string metadataKey)
        {
            yield return new FieldMetadataValidatorKey(modelIdentifier, fieldIdentifier, metadataKey);
            yield return new FieldMetadataValidatorKey(null, fieldIdentifier, metadataKey);
            yield return new FieldMetadataValidatorKey(null, null, metadataKey);
        }
    }
}
