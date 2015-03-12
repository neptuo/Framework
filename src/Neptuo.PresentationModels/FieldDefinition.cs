using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    /// <summary>
    /// Implementation of <see cref="IFieldDefinition"/>.
    /// </summary>
    public class FieldDefinition : IFieldDefinition
    {
        public string Identifier { get; protected set; }
        public IFieldType FieldType { get; protected set; }
        public IFieldMetadataCollection Metadata { get; protected set; }

        public FieldDefinition(string identifier, IFieldType fieldType, IFieldMetadataCollection metadata)
        {
            Ensure.NotNull(identifier ,"identifier");
            Ensure.NotNull(fieldType ,"fieldType");
            Ensure.NotNull(metadata, "metadata");
            Identifier = identifier;
            FieldType = fieldType;
            Metadata = metadata;
        }
    }
}
