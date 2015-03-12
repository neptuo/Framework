using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Validators
{
    /// <summary>
    /// Implementation of <see cref="IFieldMetadataValidatorFactory"/> that uses single instance of validator for all validations.
    /// </summary>
    public class SingletonFieldMetadataValidatorFactory : IFieldMetadataValidatorFactory
    {
        /// <summary>
        /// Field validator instance.
        /// </summary>
        public IFieldMetadataValidator Validator { get; private set; }

        /// <summary>
        /// Creates new instance that uses <paramref name="validator"/> for all validations.
        /// </summary>
        /// <param name="validator">Field validator instance.</param>
        public SingletonFieldMetadataValidatorFactory(IFieldMetadataValidator validator)
        {
            Ensure.NotNull(validator, "validator");
            Validator = validator;
        }

        public IFieldMetadataValidator Create()
        {
            return Validator;
        }
    }
}
