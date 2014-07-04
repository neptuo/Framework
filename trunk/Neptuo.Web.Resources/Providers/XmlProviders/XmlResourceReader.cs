using Neptuo.FileSystems;
using Neptuo.Web.Resources.FileResources;
using Neptuo.Web.Resources.LazyResources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Neptuo.Web.Resources.Providers.XmlProviders
{
    /// <summary>
    /// Initializes resource collections from xml file.
    /// </summary>
    public class XmlResourceReader : IResourceCollectionInitializer
    {
        private IXmlElement rootElement;

        /// <summary>
        /// Creates new instance with <paramref name="file"/> as source xml file.
        /// </summary>
        /// <param name="file">Xml source file.</param>
        public XmlResourceReader(IReadOnlyFile file)
        {
            Guard.NotNull(file, "file");
            rootElement = CreateRootElement(file);
        }

        /// <summary>
        /// Creates xml root element from <paramref name="file"/>.
        /// </summary>
        /// <param name="file">Xml source file.</param>
        /// <returns>Root xml element of document created from <paramref name="file"/>.</returns>
        internal static IXmlElement CreateRootElement(IReadOnlyFile file)
        {
            Guard.NotNull(file, "file");

            XmlDocument document = new XmlDocument();
            using (Stream fileContent = file.GetContentAsStream())
            {
                document.Load(fileContent);
                return new XmlDocumentElement(document.DocumentElement);
            }
        }

        public void FillCollection(IResourceCollection collection)
        {
            Guard.NotNull(collection, "collection");
            foreach (IXmlElement resourceElement in rootElement.GetChildElements("Resource"))
            {
                FileResource resource = new FileResource(resourceElement.GetAttribute("Name"));

                foreach (IXmlElement javascriptElement in resourceElement.GetChildElements("Javascript"))
                    resource.AddJavascript(new FileJavascript(javascriptElement.GetAttribute("Source")));

                foreach (IXmlElement stylesheetElement in resourceElement.GetChildElements("Stylesheet"))
                    resource.AddStylesheet(new FileStylesheet(stylesheetElement.GetAttribute("Source")));

                foreach (IXmlElement dependencyElement in resourceElement.GetChildElements("Resource"))
                    resource.AddDependency(new LazyResource(collection, dependencyElement.GetAttribute("Name")));

                collection.Add(resource);
            }
        }

        public void Dispose()
        { }
    }
}
