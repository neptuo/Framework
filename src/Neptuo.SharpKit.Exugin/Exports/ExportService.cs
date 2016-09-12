using ICSharpCode.NRefactory.TypeSystem;
using Mirrored.SharpKit.JavaScript;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Neptuo.SharpKit.Exugin.Exports
{
    public class ExportService
    {
        /// <summary>
        /// Loads export registration for assembly.
        /// </summary>
        /// <param name="assembly">Assembly definition.</param>
        /// <returns>Export registration for assembly</returns>
        public ExportRegistry Load(IAssembly assembly)
        {
            ExportRegistry result = new ExportRegistry
            {
                IsExportAssembly = true,
                Assembly = assembly
            };

            if (!IsConfigurationFile(assembly))
            {
                result.IsExportAssembly = false;
                return result;
            }

            XmlDocument document = new XmlDocument();
            document.Load(GetConfigurationFilename(assembly));

            if (result.ExternalAttributes == null)
                result.ExternalAttributes = new HashSet<string>();

            foreach (XmlElement element in document.GetElementsByTagName("ExternalAttributes"))
            {
                foreach (XmlElement typeElement in element.GetElementsByTagName("Type"))
                    result.ExternalAttributes.Add(typeElement.GetAttribute("Target"));
            }

            foreach (XmlElement element in document.GetElementsByTagName("Export"))
                result.DefaultExport = LoadExport(element);

            foreach (XmlElement element in document.GetElementsByTagName("Namespace"))
                result.AddItem(LoadNamespace(element, result));

            foreach (XmlElement element in document.GetElementsByTagName("Type"))
                result.AddItem(LoadType(element, result));

            foreach (XmlElement element in document.GetElementsByTagName("Merge"))
                result.AddItem(LoadMerge(element));

            result.BuildUp();
            return result;
        }

        #region Reading and preparing file

        /// <summary>
        /// Tests whether exists configuration file.
        /// </summary>
        /// <param name="assembly">Assembly definition.</param>
        /// <returns>True if file exists, false otherwise.</returns>
        public bool IsConfigurationFile(IAssembly assembly)
        {
            return File.Exists(GetConfigurationFilename(assembly));
        }

        /// <summary>
        /// Returns name of configuration file.
        /// </summary>
        /// <param name="assembly">Assembly definition.</param>
        /// <returns>Name of configuration file.</returns>
        public string GetConfigurationFilename(IAssembly assembly)
        {
            string assemblyName = assembly.AssemblyName + ".xml";
            return assemblyName;
        }

        #endregion

        #region Loading from xml

        /// <summary>
        /// Loads default export settings.
        /// </summary>
        /// <param name="element">Xml element.</param>
        /// <returns>Default export settings.</returns>
        private ExportItem LoadExport(XmlElement element)
        {
            return new ExportItem
            {
                Filename = XmlUtil.GetAttributeString(element, "Filename"),
                FilenameFormat = XmlUtil.GetAttributeString(element, "FilenameFormat")
            };
        }

        /// <summary>
        /// Loads namespace export settings.
        /// </summary>
        /// <param name="element">Xml element.</param>
        /// <param name="registry">Current export registry.</param>
        /// <returns>Namespace export settings.</returns>
        private NamespaceRegistryItem LoadNamespace(XmlElement element, ExportRegistry registry)
        {
            NamespaceRegistryItem item = new NamespaceRegistryItem();
            LoadItemBase(element, item, registry);
            return item;
        }

        /// <summary>
        /// Loads type export settings.
        /// </summary>
        /// <param name="element">Xml element.</param>
        /// <param name="registry">Current export registry.</param>
        /// <returns>Type export settings.</returns>
        private TypeRegistryItem LoadType(XmlElement element, ExportRegistry registry)
        {
            TypeRegistryItem item = new TypeRegistryItem();
            item.Name = XmlUtil.GetAttributeString(element, "Name");
            item.OrderInFile = XmlUtil.GetAttributeInt(element, "OrderInFile");
            //TODO: Load methods...
            LoadItemBase(element, item, registry);
            return item;
        }

        /// <summary>
        /// Loads export settings for shared base class.
        /// </summary>
        /// <param name="element">Xml element.</param>
        /// <param name="item">Currently loading item.</param>
        /// <param name="registry">Current export registry.</param>
        private void LoadItemBase(XmlElement element, RegistryItemBase item, ExportRegistry registry)
        {
            item.Target = XmlUtil.GetAttributeString(element, "Target") ?? String.Empty;
            item.AutomaticPropertiesAsFields = XmlUtil.GetAttributeBool(element, "AutomaticPropertiesAsFields") ?? registry.GetItem(item.Target).AutomaticPropertiesAsFields;
            item.Export = XmlUtil.GetAttributeBool(element, "Export") ?? registry.GetItem(item.Target).Export;
            item.Mode = XmlUtil.GetAttributeEnum<JsMode>(element, "Mode") ?? registry.GetItem(item.Target).Mode;
            item.PropertiesAsFields = XmlUtil.GetAttributeBool(element, "PropertiesAsFields") ?? registry.GetItem(item.Target).PropertiesAsFields;
            item.Filename = registry.ApplyFilenameFormat(XmlUtil.GetAttributeString(element, "Filename")) ?? registry.GetItem(item.Target).Filename;
        }

        /// <summary>
        /// Loads merge file definition.
        /// </summary>
        /// <param name="element">Xml element.</param>
        /// <returns>Merge file definition.</returns>
        private MergeFileItem LoadMerge(XmlElement element)
        {
            MergeFileItem item = new MergeFileItem();
            item.FileName = XmlUtil.GetAttributeString(element, "Filename");
            item.Sources = (XmlUtil.GetAttributeString(element, "Sources") ?? "").Split(',');
            item.Minify = XmlUtil.GetAttributeBool(element, "Minify") ?? false;
            return item;
        }

        #endregion
    }
}
