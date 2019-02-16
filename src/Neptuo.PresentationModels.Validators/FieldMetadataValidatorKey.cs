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
    public class FieldMetadataValidatorKey : IEquatable<FieldMetadataValidatorKey>
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
            var hashCode = -469800716;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ModelIdentifier);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FieldIdentifier);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(MetadataKey);
            return hashCode;
        }

        public override bool Equals(object obj) => Equals(obj as FieldMetadataValidatorKey);

        public bool Equals(FieldMetadataValidatorKey other)
        {
            return other != null &&
                ModelIdentifier == other.ModelIdentifier &&
                FieldIdentifier == other.FieldIdentifier &&
                MetadataKey == other.MetadataKey;
        }
    }
}
