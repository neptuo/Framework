using Neptuo.FileSystems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Resources.Collections
{
    public class XmlResourceCollection : ResourceCollectionBase
    {
        public XmlResourceCollection(IFile file)
        {
            Guard.NotNull(file, "file");
            LoadFromXml(XmlReader.CreateRootElement(file));
        }

        private void LoadFromXml(IXmlElement element)
        {
            foreach (IXmlElement resource in element.GetChildElements("Resource"))
            {
                //TODO: Load from XML.
            }
        }
    }
}
