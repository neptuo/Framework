using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Validation
{
    /// <summary>
    /// Provides context information for validator of type <see cref="FieldMetadataValidatorBase<,>"/>.
    /// </summary>
    public class FieldMetadataValidatorContext
    {
        public IFieldDefinition FieldDefinition { get; private set; }
        public IModelValueGetter Getter { get; private set; }
        public IModelValidationBuilder ResultBuilder { get; private set; }

        public FieldMetadataValidatorContext(IFieldDefinition fieldDefinition, IModelValueGetter getter, IModelValidationBuilder resultBuilder)
        {
            Guard.NotNull(fieldDefinition, "fieldDefinition");
            Guard.NotNull(getter, "getter");
            Guard.NotNull(resultBuilder, "resultBuilder");
            FieldDefinition = fieldDefinition;
            Getter = getter;
            ResultBuilder = resultBuilder;
        }
    }
}
