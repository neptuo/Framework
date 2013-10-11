using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    public class ProxyModelDefinition : IModelDefinition
    {
        private string identifier;
        private IEnumerable<IFieldDefinition> fields;
        private IModelMetadataCollection metadata;

        protected bool RequiresRefresh { get; set; }
        protected IModelDefinition ModelDefinition { get; private set; }

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

        public ProxyModelDefinition(IModelDefinition modelDefinition)
        {
            if (modelDefinition == null)
                throw new ArgumentNullException("modelDefinition");

            ModelDefinition = modelDefinition;
            RequiresRefresh = true;
        }

        protected virtual string RefreshIdentifier()
        {
            return ModelDefinition.Identifier;
        }

        protected virtual IEnumerable<IFieldDefinition> RefreshFields()
        {
            return ModelDefinition.Fields;
        }

        protected virtual IModelMetadataCollection RefreshMetadata()
        {
            return ModelDefinition.Metadata;
        }
    }
}
