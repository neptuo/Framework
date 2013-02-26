using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Globalization;

namespace Neptuo.Xml
{
    public class XmlHelper
    {
        public static void ElementsByName<T>(XmlElement root, string name, T model, Action<T, XmlElement> onMatch)
        {
            XmlNodeList elements = root.GetElementsByTagName(name);
            foreach (XmlNode element in root.ChildNodes)
            {
                if (element.NodeType == XmlNodeType.Element && element.Name.ToLowerInvariant() == name.ToLowerInvariant())
                    onMatch(model, (XmlElement)element);
            }
        }

        public static void ElementByName<T>(XmlElement root, string name, T model, Action<T, XmlElement> onMatch, Action<T> onNoMatch = null)
        {
            XmlNodeList elements = root.GetElementsByTagName(name);
            if (elements.Count > 0)
                onMatch(model, (XmlElement)elements[0]);
            else if (onNoMatch != null)
                onNoMatch(model);
        }

        #region GetAttribute***

        public static string GetAttributeValue(XmlElement el, string name, string defaultValue = null)
        {
            if (el.Attributes[name] != null && !String.IsNullOrEmpty(el.Attributes[name].Value))
                return el.Attributes[name].Value;

            return defaultValue;
        }

        public static int GetAttributeInt(XmlElement el, string name, int defaultValue)
        {
            int result;
            if (Int32.TryParse(GetAttributeValue(el, name), out result))
                return result;

            return defaultValue;
        }

        public static long GetAttributeLong(XmlElement el, string name, long defaultValue)
        {
            long result;
            if (Int64.TryParse(GetAttributeValue(el, name), out result))
                return result;

            return defaultValue;
        }

        public static decimal GetAttributeDecimal(XmlElement el, string name, decimal defaultValue)
        {
            decimal result;
            if (Decimal.TryParse(GetAttributeValue(el, name), out result))
                return result;

            return defaultValue;
        }

        public static double GetAttributeDouble(XmlElement el, string name, double defaultValue)
        {
            double result;
            if (Double.TryParse(GetAttributeValue(el, name), out result))
                return result;

            return defaultValue;
        }

        public static DateTime GetAttributeDateTime(XmlElement el, string name, DateTime? defaultValue = null)
        {
            string value = GetAttributeValue(el, name);

            DateTime dateTime;
            if (DateTime.TryParse(value, out dateTime))
                return dateTime;

            long ticks;
            if (Int64.TryParse(value, out ticks))
                return new DateTime(ticks);

            if (defaultValue == null)
                defaultValue = DateTime.Now;

            return defaultValue.Value;
        }

        public static DateTime? GetAttributeDateTimeNull(XmlElement el, string name)
        {
            string value = GetAttributeValue(el, name);

            DateTime dateTime;
            if (DateTime.TryParse(value, out dateTime))
                return dateTime;

            long ticks;
            if (Int64.TryParse(value, out ticks))
                return new DateTime(ticks);

            return null;
        }

        public static CultureInfo GetAttributeCulture(XmlElement el, string name, CultureInfo defaultValue)
        {
            string culture = GetAttributeValue(el, name);
            if (culture != null)
                return CultureInfo.GetCultureInfo(culture);
            return defaultValue;
        }

        public static string[] GetAttributeValue(XmlElement el, string name, char separator)
        {
            string value = GetAttributeValue(el, name);
            if (value != null)
            {
                string[] values = value.Split(separator);
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = values[i].Trim();
                }
                return values;
            }
            return null;
        }

        public static T GetAttributeEnum<T>(XmlElement el, string name, T defaultValue) where T : struct
        {
            string attr = GetAttributeValue(el, name);
            if (attr != null)
            {
                T val;
                if (Enum.TryParse<T>(attr, out val))
                {
                    return val;
                }
            }

            return defaultValue;
        }

        public static bool GetAttributeBool(XmlElement el, string name, bool defaultValue = false)
        {
            string attr = GetAttributeValue(el, name);
            if (attr != null)
            {
                bool result;
                if (Boolean.TryParse(attr, out result))
                    return result;
            }
            return defaultValue;
        }

        #endregion

        public static XmlDocument CreateDocument()
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(dec);
            return doc;
        }

        public static void SetAttribute<T>(XmlDocument doc, XmlElement el, string name, T value)
        {
            if (value != null)
            {
                XmlAttribute att = doc.CreateAttribute(name);
                att.Value = value.ToString();
                el.Attributes.Append(att);
            }
        }

        #region SetAttributes

        public static void SetAttributes<T>(XmlDocument doc, XmlElement parent, string collectionName, string itemName, IEnumerable<T> items, Func<T, XmlElement, XmlElement> itemWriter)
        {
            XmlElement collection = doc.CreateElement(collectionName);
            parent.AppendChild(collection);
            SetAttributes<T>(doc, collection, itemName, items, itemWriter);
        }

        public static void SetAttributes<T>(XmlDocument doc, XmlElement parent, string itemName, IEnumerable<T> items, Func<T, XmlElement, XmlElement> itemWriter)
        {
            foreach (T item in items)
                SetAttributes<T>(doc, parent, itemName, item, itemWriter);
        }

        public static XmlElement SetAttributes<T>(XmlDocument doc, XmlElement parent, string elementName, T item, Func<T, XmlElement, XmlElement> elementWriter)
        {
            XmlElement xitem = doc.CreateElement(elementName);
            parent.AppendChild(elementWriter(item, xitem));
            return xitem;
        }

        public static void SetAttributes<T>(XmlDocument doc, XmlElement parent, string collectionName, string itemName, IEnumerable<T> items, Action<T, XmlElement> itemWriter)
        {
            XmlElement collection = doc.CreateElement(collectionName);
            parent.AppendChild(collection);
            SetAttributes<T>(doc, collection, itemName, items, itemWriter);
        }

        public static void SetAttributes<T>(XmlDocument doc, XmlElement parent, string itemName, IEnumerable<T> items, Action<T, XmlElement> itemWriter)
        {
            foreach (T item in items)
                SetAttributes<T>(doc, parent, itemName, item, itemWriter);
        }

        public static XmlElement SetAttributes<T>(XmlDocument doc, XmlElement parent, string elementName, T item, Action<T, XmlElement> elementWriter)
        {
            XmlElement xitem = doc.CreateElement(elementName);
            elementWriter(item, xitem);
            parent.AppendChild(xitem);
            return xitem;
        }

        #endregion
    }
}
