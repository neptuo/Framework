using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SharpKit.UnobtrusiveFeatures.Exports
{
    public static class XmlUtil
    {
        public static string GetAttributeString(XmlElement element, string attributeName)
        {
            if (element.HasAttribute(attributeName))
                return element.GetAttribute(attributeName);

            return null;
        }

        public static bool? GetAttributeBool(XmlElement element, string attributeName, bool? defaultValue = null)
        {
            string value = GetAttributeString(element, attributeName);
            bool targetValue;
            if (Boolean.TryParse(value, out targetValue))
                return targetValue;

            return defaultValue;
        }

        public static int? GetAttributeInt(XmlElement element, string attributeName)
        {
            string value = GetAttributeString(element, attributeName);
            int targetValue;
            if (Int32.TryParse(value, out targetValue))
                return targetValue;

            return null;
        }

        public static T? GetAttributeEnum<T>(XmlElement element, string attributeName)
            where T : struct
        {
            string value = GetAttributeString(element, attributeName);
            T targetValue;
            if (Enum.TryParse<T>(value, out targetValue))
                return targetValue;

            return null;
        }

    }
}
