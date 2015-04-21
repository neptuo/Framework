using Neptuo.Pipelines.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Validators.Handlers
{
    /// <summary>
    /// Validates model using collection of metadata validators.
    /// </summary>
    public class FieldMetadataModelValidator : ModelValidator
    {
        public FieldMetadataModelValidator(IFieldMetadataValidatorCollection validators)
            : base(new MetadataFieldValidator(validators))
        { }
    }
}
