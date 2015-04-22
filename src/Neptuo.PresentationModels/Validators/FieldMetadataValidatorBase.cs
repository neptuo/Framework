using Neptuo.Collections.Specialized;
using Neptuo.Pipelines.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Validators
{
    /// <summary>
    /// Base field validator for meta data of type <typeparamref name="TMetadataValue"/> and field value of type <typeparamref name="TFieldValue"/>.
    /// Uses <see cref="MetadataKey"/> as key to metadata collection of field definition <see cref="IFieldDefinition"/>, 
    /// read value is passed with value from <see cref="IModelValueGetter"/> to abstract <see cref="IFieldMetadataValidator.Validate"/> metod.
    /// </summary>
    /// <typeparam name="TMetadataValue">Type of meta data value.</typeparam>
    /// <typeparam name="TFieldValue">Type of field value.</typeparam>
    public abstract class FieldMetadataValidatorBase<TMetadataValue, TFieldValue> : IFieldMetadataValidator
    {
        /// <summary>
        /// Meta data name (key) to validate.
        /// </summary>
        protected string MetadataKey { get; private set; }

        /// <summary>
        /// Creates new instance using <paramref name="metadataKey"/>.
        /// </summary>
        /// <param name="metadataKey">Meta data name (key) to validate.</param>
        public FieldMetadataValidatorBase(string metadataKey)
        {
            Ensure.NotNullOrEmpty(metadataKey, "metadataKey");
            MetadataKey = metadataKey;
        }

        /// <summary>
        /// Reads metadata value from  <paramref name="fieldDefinition"/> and current value from <paramref name="getter"/> and passes these values to <see cref="Validate"/>.
        /// If metadata key is not found, <see cref="MissingMetadataKey"/> is called.
        /// Returns <c>true</c> if field is valid; false otherwise.
        /// </summary>
        /// <param name="fieldDefinition">Defines field to validate.</param>
        /// <param name="getter">Provides current values.</param>
        /// <param name="resultBuilder">Validation result builder.</param>
        /// <returns><c>true</c> if field is valid; false otherwise.</returns>
        public void Validate(IFieldDefinition fieldDefinition, IModelValueGetter getter, IValidationResultBuilder resultBuilder)
        {
            Ensure.NotNull(fieldDefinition, "fieldDefinition");
            Ensure.NotNull(getter, "getter");
            Ensure.NotNull(resultBuilder, "resultBuilder");

            object metadataValue;
            if (fieldDefinition.Metadata.TryGet(MetadataKey, out metadataValue))
            {
                TFieldValue fieldValue = getter.GetValueOrDefault(fieldDefinition.Identifier, default(TFieldValue));
                TMetadataValue metadata = (TMetadataValue)metadataValue;

                Validate(fieldValue, metadata, new FieldMetadataValidatorContext(fieldDefinition, getter, resultBuilder));
            }
            else
            {
                MissingMetadataKey(fieldDefinition, getter, resultBuilder);
            }
        }

        /// <summary>
        /// Provides validation logic for validating <paramref name="fieldValue"/> againts <paramref name="metadatValue"/>.
        /// Validation result builder is in <paramref name="context" />
        /// </summary>
        /// <param name="fieldValue">Current field value.</param>
        /// <param name="metadataValue">Metadata value.</param>
        /// <param name="context">Validation context.</param>
        protected abstract void Validate(TFieldValue fieldValue, TMetadataValue metadataValue, FieldMetadataValidatorContext context);

        /// <summary>
        /// Provides behavior when metadata key is not found in field definition.
        /// By default, does nothing.
        /// </summary>
        /// <param name="fieldDefinition">Defines field to validate.</param>
        /// <param name="getter">Provides current values.</param>
        /// <param name="resultBuilder">Validation result builder.</param>
        protected virtual void MissingMetadataKey(IFieldDefinition fieldDefinition, IModelValueGetter getter, IValidationResultBuilder resultBuilder)
        { }
    }
}
