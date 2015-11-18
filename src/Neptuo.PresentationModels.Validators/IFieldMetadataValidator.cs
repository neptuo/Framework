using Neptuo.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Validators
{
    /// <summary>
    /// Validator for single field of presentation model.
    /// </summary>
    public interface IFieldMetadataValidator
    {
        /// <summary>
        /// Validates value from <paramref name="getter"/> againts field definition in <paramref name="fieldDefinition"/>.
        /// Returns <c>true</c> if field is valid; false otherwise.
        /// </summary>
        /// <param name="fieldDefinition">Defines field to validate.</param>
        /// <param name="getter">Provides current values.</param>
        /// <param name="resultBuilder">Validation result builder.</param>
        void Validate(IFieldDefinition fieldDefinition, IModelValueGetter getter, IValidationResultBuilder resultBuilder);
    }
}
