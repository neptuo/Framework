using Neptuo.Collections.Specialized;
using Neptuo.Pipelines.Validators;
using Neptuo.Pipelines.Validators.Messages;
using Neptuo.PresentationModels;
using Neptuo.PresentationModels.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels.DataAnnotations.Validators
{
    /// <summary>
    /// Uses these keys from metadata: 
    /// <c>MatchProperty</c>
    /// <c>MatchProperty.ErrorMessage</c>
    /// </summary>
    public class MatchPropertyMetadataValidator : FieldMetadataValidatorBase<string, object>
    {
        public MatchPropertyMetadataValidator()
            : base("MatchProperty")
        { }

        protected override void Validate(object fieldValue, string metadataValue, FieldMetadataValidatorContext context)
        {
            string errorMessage = context.FieldDefinition.Metadata.Get(
                "MatchProperty.ErrorMessage",
                String.Format("'{0}' and '{1}' must match!", context.FieldDefinition.Identifier, metadataValue)
            );

            object otherProperty = null;
            if(context.Getter.TryGetValue(metadataValue, out otherProperty)) 
            {
                if ((fieldValue == null && otherProperty != null) || !fieldValue.Equals(otherProperty))
                    context.ResultBuilder.AddTextMessage(context.FieldDefinition.Identifier, errorMessage);
            }
            else
            {
                if (fieldValue != null)
                    context.ResultBuilder.AddTextMessage(context.FieldDefinition.Identifier, errorMessage);
            }
        }
    }
}
