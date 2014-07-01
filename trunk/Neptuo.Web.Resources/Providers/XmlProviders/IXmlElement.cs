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
        string GetAttribute(string attributeName);
        IEnumerable<IXmlElement> GetChildElements(string elementName);
    }
}
