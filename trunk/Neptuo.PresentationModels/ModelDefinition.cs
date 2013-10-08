using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    public class ModelDefinition : IModelDefinition
    {
        public string Identifier { get; protected set; }
        public IEnumerable<IFieldDefinition> Fields { get; protected set; }
        public IModelMetadataCollection Metadata { get; protected set; }

        public ModelDefinition(string identifier, IEnumerable<IFieldDefinition> fields, IModelMetadataCollection metadata)
        {
            if (identifier == null)
                throw new ArgumentNullException("identifier");

            if (fields == null)
                throw new ArgumentNullException("fields");

            if (metadata == null)
                throw new ArgumentNullException("metadata");

            Identifier = identifier;
            Fields = new List<IFieldDefinition>(fields);
            Metadata = metadata;
        }
    }
}
