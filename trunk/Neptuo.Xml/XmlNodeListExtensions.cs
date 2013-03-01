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
    }
}
