using Neptuo.ComponentModel;
using Neptuo.FileSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Neptuo.PresentationModels.Serialization
{
    /// <summary>
    /// Reads elements from root element of XML file and for those that match model definition identifier creates <see cref="IModelValueGetter"/>.
    /// Other XML elements are ignored.
    /// </summary>
    public class XmlModelValueGetterFactory : IEnumerable<IModelValueGetter>
    {
        private readonly IModelDefinition modelDefinition;
        private readonly IReadOnlyList<XElement> elements;

        public int Count
        {
            get { return elements.Count(); }
        }

        public XmlModelValueGetterFactory(IModelDefinition modelDefinition, IReadOnlyFile xmlFile)
        {
            Ensure.NotNull(modelDefinition, "modelDefinition");
            Ensure.NotNull(xmlFile, "xmlFile");

            if (xmlFile.Extension.ToLowerInvariant() != ".xml")
                Ensure.Exception.FileSystem("Only xml files are supported, but got file named '{0}{1}'.", xmlFile.Name, xmlFile.Extension);

            this.modelDefinition = modelDefinition;
            this.elements = BuildElements(xmlFile);
        }

        private IReadOnlyList<XElement> BuildElements(IReadOnlyFile xmlFile)
        {
            XDocument document = XDocument.Load(xmlFile.GetContentAsStream());
            return document.Root.Elements(modelDefinition.Identifier).ToList();
        }

        public IModelValueGetter Getter(int index)
        {
            return new XmlModelValueGetter(modelDefinition, elements[index]);
        }

        public IEnumerator<IModelValueGetter> GetEnumerator()
        {
            return elements.Select(e => new XmlModelValueGetter(modelDefinition, e)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
