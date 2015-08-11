using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Validators
{
    /// <summary>
    /// Describes key for selecting <see cref="IFieldMetadataValidator"/>.
    /// </summary>
    public class FieldMetadataValidatorKey
    {
        /// <summary>
        /// Identifier of model definition.
        /// </summary>
        public string ModelIdentifier { get; private set; }

        /// <summary>
        /// Identifier of field definition.
        /// </summary>
        public string FieldIdentifier { get; private set; }

        /// <summary>
        /// Field metadata validator key.
        /// </summary>
        public string MetadataKey { get; private set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="modelIdentifier">Identifier of model definition.</param>
        /// <param name="fieldIdentifier">Identifier of field definition.</param>
        /// <param name="metadataKey">Field metadata validator key.</param>
        public FieldMetadataValidatorKey(string modelIdentifier, string fieldIdentifier, string metadataKey)
        {
            ModelIdentifier = modelIdentifier;
            FieldIdentifier = fieldIdentifier;
            MetadataKey = metadataKey;
        }

        public override int GetHashCode()
        {
            int value = 13;
            if (ModelIdentifier != null)
                value ^= ModelIdentifier.GetHashCode();

            if (FieldIdentifier != null)
                value ^= FieldIdentifier.GetHashCode();

            if(MetadataKey != null)
                value ^= MetadataKey.GetHashCode();

            return value;
        }

        public override bool Equals(object obj)
        {
            FieldMetadataValidatorKey other = obj as FieldMetadataValidatorKey;
            if (other == null)
                return false;

            if (ModelIdentifier != other.ModelIdentifier)
                return false;

            if (FieldIdentifier != other.FieldIdentifier)
                return false;

            if (MetadataKey != other.MetadataKey)
                return false;

            return true;
        }
    }
}
