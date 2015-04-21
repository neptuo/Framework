using Neptuo.Collections.Specialized;
using Neptuo.PresentationModels.Validators;
using Neptuo.Pipelines.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neptuo.Pipelines.Validators.Messages;

namespace Neptuo.PresentationModels.TypeModels.DataAnnotations.Validators
{
    /// <summary>
    /// Uses these keys from metadata: 
    /// <c>Required</c>
    /// <c>Required.ErrorMessage</c>
    /// <c>Required.AllowEmptyStrings</c>
    /// </summary>
    public class RequiredMetadataValidator : FieldMetadataValidatorBase<bool, object>
    {
        public RequiredMetadataValidator()
            : base("Required")
        { }

        protected override void Validate(object fieldValue, bool metadataValue, FieldMetadataValidatorContext context)
        {
            // Null value is always invalid.
            if (fieldValue == null)
            {
                context.ResultBuilder.Add(
                    new TextValidationMessage(
                        context.FieldDefinition.Identifier,
                        context.FieldDefinition.Metadata.Get(
                            "Required.ErrorMessage",
                            String.Format("Missing value for required field '{0}'!", context.FieldDefinition.Identifier)
                        )
                    )
                );
                return;
            }

            // For string values, test whether empty strings are allowed.
            string targetValue = fieldValue.ToString();
            if (!context.FieldDefinition.Metadata.Get("Required.AllowEmptyStrings", false) && String.IsNullOrEmpty(targetValue))
            {
                context.ResultBuilder.Add(
                    new TextValidationMessage(
                        context.FieldDefinition.Identifier,
                        context.FieldDefinition.Metadata.Get(
                            "Required.ErrorMessage",
                            String.Format("Missing value for required field '{0}'!", context.FieldDefinition.Identifier)
                        )
                    )
                );
            }
        }
    }
}
