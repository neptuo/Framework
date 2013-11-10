using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    public class ProxyModelDefinition : ProxyModelDefinitionBase
    {
        protected IModelDefinition ModelDefinition { get; private set; }

        public ProxyModelDefinition(IModelDefinition modelDefinition)
            : base()
        {
            if (modelDefinition == null)
                throw new ArgumentNullException("modelDefinition");

            ModelDefinition = modelDefinition;
        }

        protected override string RefreshIdentifier()
        {
            return ModelDefinition.Identifier;
        }

        protected override IEnumerable<IFieldDefinition> RefreshFields()
        {
            return ModelDefinition.Fields;
        }

        protected override IModelMetadataCollection RefreshMetadata()
        {
            return ModelDefinition.Metadata;
        }
    }
}
