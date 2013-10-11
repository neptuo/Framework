using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    public class FieldDefinition : IFieldDefinition
    {
        public string Identifier { get; protected set; }
        public IFieldType FieldType { get; protected set; }
        public IFieldMetadataCollection Metadata { get; protected set; }

        public FieldDefinition(string identifier, IFieldType fieldType, IFieldMetadataCollection metadata)
        {
            if (identifier == null)
                throw new ArgumentNullException("identifier");

            if (fieldType == null)
                throw new ArgumentNullException("fieldType");

            if(metadata == null)
                throw new ArgumentNullException("metadata");

            Identifier = identifier;
            FieldType = fieldType;
            Metadata = metadata;
        }
    }
}
