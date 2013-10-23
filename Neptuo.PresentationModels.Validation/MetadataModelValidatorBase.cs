using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Validation
{
    public class MetadataModelValidatorBase : ModelValidatorBase
    {
        protected IMetadataValidatorCollection Validators { get; private set; }

        public MetadataModelValidatorBase(IModelDefinition modelDefinition, IMetadataValidatorCollection validators)
            : base(modelDefinition)
        {
            if (validators == null)
                throw new ArgumentNullException("validators");

            Validators = validators;
        }

        protected override void ValidateField(IFieldDefinition fieldDefinition, IModelValueGetter getter, IModelValidationBuilder resultBuilder)
        {
            foreach (string key in fieldDefinition.Metadata.Keys)
            {
                IFiedMetadataValidator validator;
                if (Validators.TryGet(ModelDefinition.Identifier, fieldDefinition.Identifier, key, out validator))
                    validator.Validate(fieldDefinition, getter, resultBuilder);
            }
        }
    }
}
