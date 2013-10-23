using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Validation
{
    public abstract class FieldMetadataValidatorBase<TMetadataValue, TFieldValue> : IFieldMetadataValidator
    {
        protected string MetadataKey { get; private set; }

        public FieldMetadataValidatorBase(string metadataKey)
        {
            if (metadataKey == null)
                throw new ArgumentNullException("metadataKey");

            MetadataKey = metadataKey;
        }

        public bool Validate(IFieldDefinition fieldDefinition, IModelValueGetter getter, IModelValidationBuilder resultBuilder)
        {
            object metadataValue;
            if (!fieldDefinition.Metadata.TryGet(MetadataKey, out metadataValue))
                return MissingMetadataKey(fieldDefinition, getter, resultBuilder);

            TFieldValue fieldValue = (TFieldValue)getter.GetValue(fieldDefinition.Identifier);
            TMetadataValue metadata = (TMetadataValue)metadataValue;

            return Validate(fieldValue, metadata, new FieldMetadataValidatorContext(fieldDefinition, getter, resultBuilder));
        }

        protected abstract bool Validate(TFieldValue fieldValue, TMetadataValue metadataValue, FieldMetadataValidatorContext context);

        protected abstract bool MissingMetadataKey(IFieldDefinition fieldDefinition, IModelValueGetter getter, IModelValidationBuilder resultBuilder);
    }
}
