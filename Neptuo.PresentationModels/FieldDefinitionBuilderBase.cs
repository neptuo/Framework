using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    public abstract class FieldDefinitionBuilderBase : IFieldDefinitionBuilder
    {
        protected abstract string BuildFieldIdentifier();
        protected abstract Type BuildFieldType();
        protected abstract IFieldMetadataCollection BuildFieldMetadata();

        public IFieldDefinition Build()
        {
            return new FieldDefinition(BuildFieldIdentifier(), BuildFieldType(), BuildFieldMetadata());
        }
    }
}
