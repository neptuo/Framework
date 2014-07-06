using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Resources.Providers.XmlProviders
{
    internal interface IXmlElement
    {
        string Name { get; }
        string GetAttributeValue(string attributeName);
        IEnumerable<string> EnumerateAttributeNames();
        IEnumerable<IXmlElement> EnumerateChildElements(string elementName);
    }
}
