using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    /// <summary>
    /// Implementation of <see cref="IModelDefinition"/>.
    /// </summary>
    public class ModelDefinition : IModelDefinition
    {
        public string Identifier { get; protected set; }
        public IEnumerable<IFieldDefinition> Fields { get; protected set; }
        public IReadOnlyKeyValueCollection Metadata { get; protected set; }

        public ModelDefinition(string identifier, IEnumerable<IFieldDefinition> fields, IKeyValueCollection metadata)
        {
            Ensure.NotNull(identifier, "identifier");
            Ensure.NotNull(fields, "fields");
            Ensure.NotNull(metadata, "metadata");
            Identifier = identifier;
            Fields = new List<IFieldDefinition>(fields);
            Metadata = metadata;
        }
    }
}
