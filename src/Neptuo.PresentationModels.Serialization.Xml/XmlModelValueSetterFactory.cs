using Neptuo.Activators;
using Neptuo.ComponentModel;
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
    /// Factory for creating XML documents.
    /// </summary>
    public class XmlModelValueSetterFactory : IActivator<IModelValueSetter, IModelDefinition>
    {
        private readonly XDocument document;

        /// <summary>
        /// Creates new instance with XML document root named as <paramref name="rootElementName"/>.
        /// </summary>
        /// <param name="rootElementName"></param>
        public XmlModelValueSetterFactory(string rootElementName)
        {
            Ensure.NotNullOrEmpty(rootElementName, "rootElementName");
            this.document = new XDocument();
            this.document.Add(new XElement(rootElementName));
        }

        /// <summary>
        /// Creates model value setter for <paramref name="modelDefinition"/>.
        /// </summary>
        /// <param name="modelDefinition">Model definition.</param>
        /// <returns>Model value setter for <paramref name="modelDefinition"/>.</returns>
        public IModelValueSetter Create(IModelDefinition modelDefinition)
        {
            XElement element = new XElement(modelDefinition.Identifier);
            document.Root.Add(element);
            return new XmlModelValueSetter(modelDefinition, element);
        }

        /// <summary>
        /// Saves XML document to file.
        /// </summary>
        /// <param name="xmlFile">Target file.</param>
        public void SaveToFile(IFile xmlFile)
        {
            Ensure.NotNull(xmlFile, "xmlFile");

            if (xmlFile.Extension.ToLowerInvariant() != ".xml")
                Ensure.Exception.FileSystem("Only xml files are supported, but got file named '{0}{1}'.", xmlFile.Name, xmlFile.Extension);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                memoryStream.Seek(0, SeekOrigin.Begin);
                xmlFile.SetContentFromStream(memoryStream);
            }
        }

        /// <summary>
        /// Saves XML document to stream.
        /// </summary>
        /// <param name="stream">Target stream.</param>
        public void SaveToStream(Stream stream)
        {
            Ensure.NotNull(stream, "stream");
            document.Save(stream);
        }
    }
}
