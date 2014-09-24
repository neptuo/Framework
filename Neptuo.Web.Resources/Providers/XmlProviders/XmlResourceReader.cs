using Neptuo.ComponentModel;
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
    public class XmlResourceReader : DisposableBase, IResourceCollectionInitializer
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
            using (Stream fileContent = file.GetContentAsStreamAsync().Result)
            {
                document.Load(fileContent);
                return new XmlDocumentElement(document.DocumentElement);
            }
        }

        public void FillCollection(IResourceCollection collection)
        {
            Guard.NotNull(collection, "collection");
            foreach (IXmlElement resourceElement in rootElement.EnumerateChildElements("Resource"))
            {
                FileResource resource = new FileResource(resourceElement.GetAttributeValue("Name"), GetAttributesWithout(resourceElement, "Name"));

                foreach (IXmlElement javascriptElement in resourceElement.EnumerateChildElements("Javascript"))
                    resource.AddJavascript(new FileJavascript(javascriptElement.GetAttributeValue("Source"), GetAttributesWithout(javascriptElement, "Source")));

                foreach (IXmlElement stylesheetElement in resourceElement.EnumerateChildElements("Stylesheet"))
                    resource.AddStylesheet(new FileStylesheet(stylesheetElement.GetAttributeValue("Source"), GetAttributesWithout(stylesheetElement, "Source")));

                foreach (IXmlElement dependencyElement in resourceElement.EnumerateChildElements("Resource"))
                    resource.AddDependency(new LazyResource(collection, dependencyElement.GetAttributeValue("Name")));

                collection.Add(resource);
            }
        }

        /// <summary>
        /// Returns key-value collection of all attributes on <paramref name="element"/> excluding those named in <paramref name="attributeNamesToRemove"/>.
        /// </summary>
        /// <param name="element">Source xml element to read attributes on.</param>
        /// <param name="attributeNamesToRemove">Enumeration of attribute names to exclude from result.</param>
        /// <returns>Key-value collection of attributes on <paramref name="element"/>.</returns>
        private IDictionary<string, string> GetAttributesWithout(IXmlElement element, params string[] attributeNamesToRemove)
        {
            Dictionary<string, string> result = new Dictionary<string,string>();
            foreach (string attributeName in element.EnumerateAttributeNames().Where(name => !attributeNamesToRemove.Contains(name)))
                result[attributeName] = element.GetAttributeValue(attributeName);

            return result;
        }
    }
}
