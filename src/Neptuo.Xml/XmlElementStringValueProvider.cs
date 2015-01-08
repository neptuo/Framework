using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Neptuo.Xml
{
    public class XmlElementStringValueProvider : IStringValueProvider<XmlElement>
    {
        public string GetValue(XmlElement model, string key)
        {
            string result = model.GetAttribute(key);
            return String.IsNullOrEmpty(result) ? null : result;
        }
    }
}
