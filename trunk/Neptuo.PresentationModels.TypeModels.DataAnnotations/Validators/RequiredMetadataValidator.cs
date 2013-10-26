using Neptuo.PresentationModels.Validation;
using Neptuo.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels.DataAnnotations.Validators
{
    public class RequiredMetadataValidator : FieldMetadataValidatorBase<bool, object>
    {
        public RequiredMetadataValidator()
            : base("Required")
        { }

        protected override bool Validate(object fieldValue, bool metadataValue, FieldMetadataValidatorContext context)
        {
            if (fieldValue == null)
            {
                context.ResultBuilder.AddMessage(
                    new TextValidationMessage(
                        context.FieldDefinition.Identifier,
                        context.FieldDefinition.Metadata.GetOrDefault(
                            "Required.ErrorMessage",
                            String.Format("Missing value for required field '{0}'!", context.FieldDefinition.Identifier)
                        )
                    )
                );
                return false;
            }

            string targetValue = fieldValue.ToString();
            if (!context.FieldDefinition.Metadata.GetOrDefault("Required.AllowEmptyStrings", false) && String.IsNullOrEmpty(targetValue))
            {
                context.ResultBuilder.AddMessage(
                    new TextValidationMessage(
                        context.FieldDefinition.Identifier,
                        context.FieldDefinition.Metadata.GetOrDefault(
                            "Required.ErrorMessage",
                            String.Format("Missing value for required field '{0}'!", context.FieldDefinition.Identifier)
                        )
                    )
                );
                return false;
            }

            return true;
        }

        protected override bool MissingMetadataKey(IFieldDefinition fieldDefinition, IModelValueGetter getter, IModelValidationBuilder resultBuilder)
        {
            return true;
        }
    }
}
