using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    /// <summary>
    /// Wraps inner model definition into proxy model definition.
    /// </summary>
    public class ProxyModelDefinition : ProxyModelDefinitionBase
    {
        /// <summary>
        /// Inner model definition.
        /// </summary>
        protected IModelDefinition ModelDefinition { get; private set; }

        /// <summary>
        /// Creates new instance with <paramref name="modelDefinition"/> as inner model definition.
        /// </summary>
        /// <param name="modelDefinition">Inner model definition.</param>
        public ProxyModelDefinition(IModelDefinition modelDefinition)
            : base()
        {
            Ensure.NotNull(modelDefinition, "modelDefinition");
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
