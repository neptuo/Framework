using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Validators
{
    /// <summary>
    /// Registry between models and their validators.
    /// </summary>
    public interface IFieldMetadataValidatorProvider
    {
        /// <summary>
        /// Tries to find validator for <paramref name="metadataKey"/> on field <paramref name="fieldIdentifier"/> of model <paramref name="modelIdentifier"/>.
        /// Returns <c>true</c> if validator is found; false otherwise.
        /// </summary>
        /// <param name="modelIdentifier">Identifier of model definition.</param>
        /// <param name="fieldIdentifier">Identifier of field definition.</param>
        /// <param name="metadataKey">Field metadata validator key.</param>
        /// <param name="validator">Registered validator.</param>
        /// <returns><c>true</c> if validator is found; false otherwise.</returns>
        bool TryGet(string modelIdentifier, string fieldIdentifier, string metadataKey, out IFieldMetadataValidator validator);
    }
}
