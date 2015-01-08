using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Neptuo.Xml
{
    public static class XmlDocumentExtensions
    {
        public static XmlAttribute CreateAttribute(this XmlDocument document, string name, string value)
        {
            XmlAttribute attribute = document.CreateAttribute(name);
            attribute.Value = value;
            return attribute;
        }
    }
}
