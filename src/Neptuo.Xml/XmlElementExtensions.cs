using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Neptuo.Xml
{
    public static class XmlElementExtensions
    {
        public static XmlElement GetElementByTagName(this XmlElement element, string name)
        {
            return element.GetElementsByTagName(name).ToEnumerable().FirstOrDefault() as XmlElement;
        }
    }
}
