using Neptuo.Activators;
using Neptuo.ComponentModel;
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
        private readonly string rootElementName;
        private XDocument document;

        /// <summary>
        /// Creates new instance with XML document root named as <paramref name="rootElementName"/>.
        /// </summary>
        /// <param name="rootElementName"></param>
        public XmlModelValueSetterFactory(string rootElementName)
        {
            Ensure.NotNullOrEmpty(rootElementName, "rootElementName");
            this.rootElementName = rootElementName;
        }

        private void EnsureDocument()
        {
            if (document == null)
                document = CreateDocument(rootElementName);
        }

        /// <summary>
        /// Creates new instance of target <see cref="XDocument"/> with root element named as <paramref name="rootElementName"/>.
        /// </summary>
        /// <param name="rootElementName">Name of the root element to insert into document.</param>
        /// <returns>New instance of <see cref="XDocument"/> wit root element.</returns>
        protected virtual XDocument CreateDocument(string rootElementName)
        {
            XDocument document = new XDocument();
            document.Add(new XElement(rootElementName));
            return document;
        }

        /// <summary>
        /// Creates model value setter for <paramref name="modelDefinition"/>.
        /// </summary>
        /// <param name="modelDefinition">Model definition.</param>
        /// <returns>Model value setter for <paramref name="modelDefinition"/>.</returns>
        public IModelValueSetter Create(IModelDefinition modelDefinition)
        {
            EnsureDocument();
            XElement element = new XElement(modelDefinition.Identifier);
            document.Root.Add(element);
            return new XmlModelValueSetter(modelDefinition, element);
        }

        /// <summary>
        /// Saves XML document to stream.
        /// </summary>
        /// <param name="stream">Target stream.</param>
        public void SaveToStream(Stream stream)
        {
            Ensure.NotNull(stream, "stream");
            EnsureDocument();
            document.Save(stream);
        }
    }
}
