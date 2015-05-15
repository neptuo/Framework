using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Serialization
{
    /// <summary>
    /// XML source based implementation of model definition builder.
    /// </summary>
    public class XmlModelDefinitionBuilder : ModelDefinitionBuilderBase
    {
        protected override string BuildModelIdentifier()
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<IFieldDefinition> BuildFieldDefinitions()
        {
            throw new NotImplementedException();
        }

        protected override IKeyValueCollection BuildModelMetadata()
        {
            throw new NotImplementedException();
        }
    }
}
