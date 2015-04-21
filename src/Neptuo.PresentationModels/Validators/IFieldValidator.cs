using Neptuo.Pipelines.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Validators
{
    /// <summary>
    /// Validator of single field.
    /// </summary>
    public interface IFieldValidator
    {
        /// <summary>
        /// Validates field defined by <paramref name="fieldDefinition"/> on model <paramref name="modelDefinition"/> with current values in <paramref name="getter"/>.
        /// </summary>
        /// <param name="modelDefinition">Model definition that contains <paramref name="fieldDefinition"/>.</param>
        /// <param name="fieldDefinition">Field definition to validate.</param>
        /// <param name="getter">Current values.</param>
        /// <param name="resultBuilder">Validation result builder.</param>
        void Validate(IModelDefinition modelDefinition, IFieldDefinition fieldDefinition, IModelValueGetter getter, IValidationResultBuilder resultBuilder);
    }
}
