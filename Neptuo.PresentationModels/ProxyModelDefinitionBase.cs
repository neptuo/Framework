using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    public abstract class ProxyModelDefinitionBase : IModelDefinition
    {
        private string identifier;
        private IEnumerable<IFieldDefinition> fields;
        private IModelMetadataCollection metadata;

        protected bool RequiresRefresh { get; set; }

        public string Identifier
        {
            get
            {
                if (RequiresRefresh)
                {
                    identifier = RefreshIdentifier();
                    RequiresRefresh = false;
                }
                return identifier;
            }
        }

        public IEnumerable<IFieldDefinition> Fields
        {
            get
            {
                if (RequiresRefresh)
                {
                    fields = RefreshFields();
                    RequiresRefresh = false;
                }
                return fields;
            }
        }

        public IModelMetadataCollection Metadata
        {
            get
            {
                if (RequiresRefresh)
                {
                    metadata = RefreshMetadata();
                    RequiresRefresh = false;
                }
                return metadata;
            }
        }

        public ProxyModelDefinitionBase()
        {
            RequiresRefresh = true;
        }

        protected abstract string RefreshIdentifier();

        protected abstract IEnumerable<IFieldDefinition> RefreshFields();

        protected abstract IModelMetadataCollection RefreshMetadata();
    }
}
