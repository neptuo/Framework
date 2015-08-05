using Neptuo.Activators;
using Neptuo.Collections.Specialized;
using Neptuo.FileSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Neptuo.PresentationModels.Serialization
{
    /// <summary>
    /// XML source based implementation of model definition builder.
    /// XML format must valid againts <see cref="XmlNameDefinition.XmlnsUri"/>.
    /// </summary>
    public class XmlModelDefinitionBuilder : IActivator<IModelDefinition>
    {
        private readonly XmlModelDefinitionFactory factory;
        private readonly IFile xmlFile;

        /// <summary>
        /// Creates new instance of model definition builder from XML file.
        /// </summary>
        /// <param name="typeMappings">Collection of type name mappings.</param>
        /// <param name="xmlFile">Source XML file.</param>
        public XmlModelDefinitionBuilder(XmlTypeMappingCollection typeMappings, IFile xmlFile)
        {
            Ensure.NotNull(xmlFile, "xmlFile");
            this.factory = new XmlModelDefinitionFactory(typeMappings);
            this.xmlFile = xmlFile;
        }

        /// <summary>
        /// Creates new instance of model definition builder from XML file.
        /// </summary>
        /// <param name="xmlFile">Source XML file.</param>
        public XmlModelDefinitionBuilder(IFile xmlFile)
            : this(new XmlTypeMappingCollection(), xmlFile)
        { }

        /// <summary>
        /// Creates new instance of model definition.
        /// </summary>
        /// <returns>New instance of model definition.</returns>
        public IModelDefinition Create()
        {
            return factory.Create(xmlFile);
        }
    }
}
