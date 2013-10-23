using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Validation
{
    public class FieldMetadataValidatorContext
    {
        public IFieldDefinition FieldDefinition { get; private set; }
        public IModelValueGetter Getter { get; private set; }
        public IModelValidationBuilder ResultBuilder { get; private set; }

        public FieldMetadataValidatorContext(IFieldDefinition fieldDefinition, IModelValueGetter getter, IModelValidationBuilder resultBuilder)
        {
            if (fieldDefinition == null)
                throw new ArgumentNullException("fieldDefinition");

            if (getter == null)
                throw new ArgumentNullException("getter");

            if (resultBuilder == null)
                throw new ArgumentNullException("resultBuilder");

            FieldDefinition = fieldDefinition;
            Getter = getter;
            ResultBuilder = resultBuilder;
        }
    }
}
