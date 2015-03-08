using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neptuo.PresentationModels;
using Neptuo.PresentationModels.Validators;
using Neptuo.Pipelines.Validators;

namespace TestConsole.PresentationModels
{
    public class MatchPropertyMetadataValidator : FieldMetadataValidatorBase<string, object>
    {
        public MatchPropertyMetadataValidator()
            : base("MatchProperty")
        { }

        protected override bool Validate(object fieldValue, string metadataValue, FieldMetadataValidatorContext context)
        {
            object otherProperty = null;
            if (fieldValue == null || !context.Getter.TryGetValue(metadataValue, out otherProperty))
            {
                if (otherProperty == null)
                    return true;

                context.ResultBuilder.AddMessage(new TextValidationMessage(
                    context.FieldDefinition.Identifier,
                    String.Format("'{0}' and '{1}' must match!", context.FieldDefinition.Identifier, metadataValue)
                ));
                return false;
            }

            if (!fieldValue.Equals(otherProperty))
            {
                context.ResultBuilder.AddMessage(new TextValidationMessage(
                    context.FieldDefinition.Identifier,
                    String.Format("'{0}' and '{1}' must match!", context.FieldDefinition.Identifier, metadataValue)
                ));
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
