using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    public abstract class ModelDefinitionBuilderBase : IModelDefinitionBuilder
    {
        protected abstract string BuildModelIdentifier();
        protected abstract IEnumerable<IFieldDefinition> BuildFieldDefinitions();
        protected abstract IModelMetadataCollection BuildModelMetadata();

        public IModelDefinition Build()
        {
            return new ModelDefinition(BuildModelIdentifier(), BuildFieldDefinitions(), BuildModelMetadata());
        }
    }
}
