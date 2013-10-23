using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Validation
{
    public interface IFiedMetadataValidator
    {
        bool Validate(IFieldDefinition fieldDefinition, IModelValueGetter getter, IModelValidationBuilder resultBulder);
    }
}
