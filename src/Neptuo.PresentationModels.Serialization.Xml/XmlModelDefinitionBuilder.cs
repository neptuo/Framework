using Neptuo.Activators;
using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Neptuo.PresentationModels.Serialization
{
    /// <summary>
    /// XML source based implementation of model definition builder.
    /// XML format must valid against <see cref="XmlNameDefinition.XmlnsUri"/>.
    /// </summary>
    public class XmlModelDefinitionBuilder : IFactory<IModelDefinition>
    {
        private readonly XmlModelDefinitionFactory factory;
        private readonly IFactory<Stream> contentFactory;

        /// <summary>
        /// Creates new instance of model definition builder from XML content.
        /// </summary>
        /// <param name="typeMappings">Collection of type name mappings.</param>
        /// <param name="contentFactory">Source XML activator.</param>
        public XmlModelDefinitionBuilder(XmlTypeMappingCollection typeMappings, IFactory<Stream> contentFactory)
        {
            Ensure.NotNull(contentFactory, "contentFactory");
            this.factory = new XmlModelDefinitionFactory(typeMappings);
            this.contentFactory = contentFactory;
        }

        /// <summary>
        /// Creates new instance of model definition builder from XML content.
        /// </summary>
        /// <param name="contentFactory">Source XML activator.</param>
        public XmlModelDefinitionBuilder(IFactory<Stream> contentFactory)
            : this(new XmlTypeMappingCollection(), contentFactory)
        { }

        /// <summary>
        /// Creates new instance of model definition.
        /// </summary>
        /// <returns>New instance of model definition.</returns>
        public IModelDefinition Create()
        {
            using (Stream content = contentFactory.Create())
            {
                return factory.Create(content);
            }
        }
    }
}
