using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Neptuo.Xml
{
    public static class XmlNodeListExtensions
    {
        public static IEnumerable<XmlNode> ToEnumerable(this XmlNodeList list)
        {
            foreach (XmlNode node in list)
                yield return node;
        }

        public static XmlElement FirstOrDefault(this XmlNodeList list)
        {
            if (list.Count > 0)
                return list[0] as XmlElement;

            return default(XmlElement);
        }
    }
}
