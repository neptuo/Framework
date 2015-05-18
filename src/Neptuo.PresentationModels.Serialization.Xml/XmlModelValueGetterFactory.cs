using Neptuo.Activators;
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
    /// Factory for reading and writing XML files and creating model value getters and setters from it.
    /// </summary>
    public class XmlModelValueGetterFactory : IActivator<XmlModelValueGetterCollection, IModelDefinition>
    {
        private readonly XDocument document;

        public XmlModelValueGetterFactory(IReadOnlyFile xmlFile)
        {
            Ensure.NotNull(xmlFile, "xmlFile");

            if (xmlFile.Extension.ToLowerInvariant() != ".xml")
                Ensure.Exception.FileSystem("Only xml files are supported, but got file named '{0}{1}'.", xmlFile.Name, xmlFile.Extension);

            document = XDocument.Load(xmlFile.GetContentAsStream());
        }

        public XmlModelValueGetterCollection Create(IModelDefinition modelDefinition)
        {
            return new XmlModelValueGetterCollection(modelDefinition, document.Root.Elements());
        }
    }
}
