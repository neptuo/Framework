using Neptuo.Collections.Specialized;
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
        public Type FieldType { get; protected set; }
        public IReadOnlyKeyValueCollection Metadata { get; protected set; }

        public FieldDefinition(string identifier, Type fieldType, IKeyValueCollection metadata)
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
