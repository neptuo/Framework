using Neptuo.Pipelines.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Validators
{
    /// <summary>
    /// Metadata based field validator.
    /// Uses <see cref="IFieldMetadataValidatorCollection"/> to get registered metadata validators and execute them.
    /// </summary>
    public class MetadataFieldValidator : IFieldValidator
    {
        /// <summary>
        /// Collection of filed validators.
        /// </summary>
        protected IFieldMetadataValidatorCollection Validators { get; private set; }

        /// <summary>
        /// Creates new instance for validating <paramref name="modelDefinition"/> with collection of metadata validators <paramref name="validators"/>.
        /// </summary>
        /// <param name="modelDefinition">Model definition.</param>
        /// <param name="validators">Collection of metadata validators.</param>
        public MetadataFieldValidator(IFieldMetadataValidatorCollection validators)
        {
            Ensure.NotNull(validators, "validators");
            Validators = validators;
        }

        public void Validate(IModelDefinition modelDefinition, IFieldDefinition fieldDefinition, IModelValueGetter getter, IValidationResultBuilder resultBuilder)
        {
            foreach (string key in fieldDefinition.Metadata.Keys)
            {
                IFieldMetadataValidator validator;
                if (Validators.TryGet(modelDefinition.Identifier, fieldDefinition.Identifier, key, out validator))
                    validator.Validate(fieldDefinition, getter, resultBuilder);
            }
        }
    }
}
