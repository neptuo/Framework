using Neptuo.FileSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Neptuo.Web.Resources.Collections
{
    internal class XmlReader
    {
        public static IXmlElement CreateRootElement(IFile file)
        {
            Guard.NotNull(file, "file");

            XmlDocument document = new XmlDocument();
            document.Load(file.GetContentAsStream());
            return new XmlDocumentElement(document.DocumentElement);
        }
    }

    internal interface IXmlElement
    {
        string Name { get; }
        string GetAttribute(string attributeName);
        IEnumerable<IXmlElement> GetChildElements(string elementName);
    }

    internal class XmlDocumentElement : IXmlElement
    {
        private XmlElement element;

        public XmlDocumentElement(XmlElement element)
        {
            Guard.NotNull(element, "element");
            this.element = element;
        }

        public string Name
        {
            get { return element.Name; }
        }

        public string GetAttribute(string attributeName)
        {
            Guard.NotNullOrEmpty(attributeName, "attributeName");
            return element.GetAttribute(attributeName);
        }

        public IEnumerable<IXmlElement> GetChildElements(string elementName)
        {
            Guard.NotNullOrEmpty(elementName, "elementName");
            foreach (XmlNode node in element.ChildNodes)
            {
                if (node.NodeType == XmlNodeType.Element && node.Name == elementName)
                    yield return new XmlDocumentElement((XmlElement)node);
            }
        }
    }

}
