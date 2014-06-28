using Neptuo.FileSystems;
using Neptuo.Web.Resources.FileResources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Resources.Collections
{
    /// <summary>
    /// Reads resources from xml file.
    /// </summary>
    /// <remarks>
    /// - Resource Name="..."
    /// -- Javascript Source="..."
    /// -- Stylesheet Source="..."
    /// -- Resource Name="..." (dependency)
    /// </remarks>
    public class XmlResourceCollection : ResourceCollectionBase
    {
        public XmlResourceCollection(IFile file)
        {
            Guard.NotNull(file, "file");
            LoadFromXml(XmlReader.CreateRootElement(file));
        }

        private void LoadFromXml(IXmlElement rootElement)
        {
            foreach (IXmlElement resourceElement in rootElement.GetChildElements("Resource"))
            {
                FileResource resource = new FileResource(resourceElement.GetAttribute("Name"));
                Add(resource);

                foreach (IXmlElement javascriptElement in resourceElement.GetChildElements("Javascript"))
                    resource.AddJavascript(new FileJavascript(javascriptElement.GetAttribute("Source")));

                foreach (IXmlElement stylesheetElement in resourceElement.GetChildElements("Stylesheet"))
                    resource.AddStylesheet(new FileStylesheet(stylesheetElement.GetAttribute("Source")));

                foreach (IXmlElement dependencyElement in resourceElement.GetChildElements("Resource"))
                    resource.AddDependency(new LazyResource(this, dependencyElement.GetAttribute("Name")));
            }
        }
    }
}
