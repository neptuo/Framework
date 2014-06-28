using Neptuo.FileSystems;
using Neptuo.Web.Resources.FileResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Neptuo.Web.Resources.Collections
{
    public class XmlReader
    {
        public static void LoadFromXml(IResourceCollection collection, IFile file)
        {
            Guard.NotNull(collection, "collection");
            Guard.NotNull(file, "file");
            foreach (IXmlElement resourceElement in CreateRootElement(file).GetChildElements("Resource"))
            {
                FileResource resource = new FileResource(resourceElement.GetAttribute("Name"));
                collection.Add(resource);

                foreach (IXmlElement javascriptElement in resourceElement.GetChildElements("Javascript"))
                    resource.AddJavascript(new FileJavascript(javascriptElement.GetAttribute("Source")));

                foreach (IXmlElement stylesheetElement in resourceElement.GetChildElements("Stylesheet"))
                    resource.AddStylesheet(new FileStylesheet(stylesheetElement.GetAttribute("Source")));

                foreach (IXmlElement dependencyElement in resourceElement.GetChildElements("Resource"))
                    resource.AddDependency(new LazyResource(collection, dependencyElement.GetAttribute("Name")));
            }
        }


        internal static IXmlElement CreateRootElement(IFile file)
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
