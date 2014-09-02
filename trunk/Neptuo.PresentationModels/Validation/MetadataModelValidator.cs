using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Validators
{
    /// <summary>
    /// Validates model using collection of metadata validators.
    /// </summary>
    public class MetadataModelValidator : ModelValidatorBase
    {
        /// <summary>
        /// Collection of filed validators.
        /// </summary>
        protected IMetadataValidatorCollection Validators { get; private set; }

        /// <summary>
        /// Creates new instance for validating <paramref name="modelDefinition"/> with collection of metadata validators <paramref name="validators"/>.
        /// </summary>
        /// <param name="modelDefinition">Model definition.</param>
        /// <param name="validators">Collection of metadata validators.</param>
        public MetadataModelValidator(IModelDefinition modelDefinition, IMetadataValidatorCollection validators)
            : base(modelDefinition)
        {
            Guard.NotNull(validators, "validators");
            Validators = validators;
        }

        /// <summary>
        /// Validates field using registered metadata validators.
        /// </summary>
        /// <param name="fieldDefinition">Field definition to validate.</param>
        /// <param name="getter">Current value provider.</param>
        /// <param name="resultBuilder">Validation result builder.</param>
        protected override void ValidateField(IFieldDefinition fieldDefinition, IModelValueGetter getter, IModelValidationBuilder resultBuilder)
        {
            foreach (string key in fieldDefinition.Metadata.Keys)
            {
                IFieldMetadataValidator validator;
                if (Validators.TryGet(ModelDefinition.Identifier, fieldDefinition.Identifier, key, out validator))
                    validator.Validate(fieldDefinition, getter, resultBuilder);
            }
        }
    }
}
