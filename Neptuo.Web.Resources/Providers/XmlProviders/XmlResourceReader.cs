using Neptuo.FileSystems;
using Neptuo.Web.Resources.FileResources;
using Neptuo.Web.Resources.LazyResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Neptuo.Web.Resources.Providers.XmlProviders
{
    public class XmlResourceReader : IResourceCollectionInitializer
    {
        private IXmlElement rootElement;

        public XmlResourceReader(IReadOnlyFile file)
        {
            Guard.NotNull(file, "file");
            rootElement = CreateRootElement(file);
        }

        internal static IXmlElement CreateRootElement(IReadOnlyFile file)
        {
            Guard.NotNull(file, "file");

            XmlDocument document = new XmlDocument();
            document.Load(file.GetContentAsStream());
            return new XmlDocumentElement(document.DocumentElement);
        }

        public void FillCollection(IResourceCollection collection)
        {
            Guard.NotNull(collection, "collection");
            foreach (IXmlElement resourceElement in rootElement.GetChildElements("Resource"))
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

        public void Dispose()
        { }
    }
}
