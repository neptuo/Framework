using Neptuo.Activators;
using Neptuo.FileSystems;
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
    /// Factory for reading and writing XML files and creating model value getters and setters from it.
    /// </summary>
    public class XmlModelValueGetterFactory : IActivator<XmlModelValueGetterCollection, IModelDefinition>
    {
        private readonly XDocument document;

        /// <summary>
        /// Creates new instance that reads from <paramref name="stream"/>.
        /// </summary>
        /// <param name="stream">XML document content to be used as value source</param>
        public XmlModelValueGetterFactory(Stream stream)
        {
            Ensure.NotNull(stream, "stream");
            document = XDocument.Load(stream);
        }

        /// <summary>
        /// Creates new instance that reads from <paramref name="xmlFile"/>.
        /// </summary>
        /// <param name="xmlFile">XML document to be used as value source.</param>
        public XmlModelValueGetterFactory(IReadOnlyFile xmlFile)
        {
            Ensure.Condition.XmlFile(xmlFile, "xmlFile");
            document = XDocument.Load(xmlFile.GetContentAsStream());
        }

        /// <summary>
        /// Creates collection of model value getters found in the XML document for <paramref name="modelDefinition"/>.
        /// </summary>
        /// <param name="modelDefinition">Model definition.</param>
        /// <returns>Collection of model value getters found in the XML document for <paramref name="modelDefinition"/>.</returns>
        public XmlModelValueGetterCollection Create(IModelDefinition modelDefinition)
        {
            return CreateGetterCollection(modelDefinition, document.Root.Elements());
        }

        /// <summary>
        /// Creates collection of model value getters for <paramref name="modelDefinition"/>.
        /// </summary>
        /// <param name="modelDefinition">Model definition.</param>
        /// <param name="elements">Enumeration of XML all available elements.</param>
        /// <returns>Collection of model value getters for <paramref name="modelDefinition"/>.</returns>
        protected XmlModelValueGetterCollection CreateGetterCollection(IModelDefinition modelDefinition, IEnumerable<XElement> elements)
        {
            return new XmlModelValueGetterCollection(modelDefinition, elements);
        }

        /// <summary>
        /// Returns enumeration of model definitions found at root.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> EnumerateRootModelDefinitionIdentifiers()
        {
            return document.Root.Elements().Select(e => e.Name.LocalName);
        }
    }
}
