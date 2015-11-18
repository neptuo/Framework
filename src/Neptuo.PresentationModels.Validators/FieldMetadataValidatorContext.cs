using Neptuo.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Validators
{
    /// <summary>
    /// Provides context information for validator of type <see cref="FieldMetadataValidatorBase<,>"/>.
    /// </summary>
    public class FieldMetadataValidatorContext
    {
        public IFieldDefinition FieldDefinition { get; private set; }
        public IModelValueGetter Getter { get; private set; }
        public IValidationResultBuilder ResultBuilder { get; private set; }

        public FieldMetadataValidatorContext(IFieldDefinition fieldDefinition, IModelValueGetter getter, IValidationResultBuilder resultBuilder)
        {
            Ensure.NotNull(fieldDefinition, "fieldDefinition");
            Ensure.NotNull(getter, "getter");
            Ensure.NotNull(resultBuilder, "resultBuilder");
            FieldDefinition = fieldDefinition;
            Getter = getter;
            ResultBuilder = resultBuilder;
        }
    }
}
