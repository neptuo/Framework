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
        private readonly Dictionary<string, ExportRegistry> folderCache = new Dictionary<string, ExportRegistry>();

        private ExportRegistry CreateInstance(IAssembly assembly)
        {
            Stack<string> applicableFiles = new Stack<string>();
            string directoryPath = Environment.CurrentDirectory;
            while (!String.IsNullOrEmpty(directoryPath))
            {
                string filePath = Path.Combine(directoryPath, "SharpKit.Exugin.xml"); // TODO: Filename in the configuration.
                if (File.Exists(filePath))
                    applicableFiles.Push(filePath);

                directoryPath = Path.GetDirectoryName(directoryPath);
            }

            ExportRegistry parent = null;
            while (applicableFiles.Count > 0)
            {
                string filePath = applicableFiles.Pop();
                ExportRegistry item;
                if (!folderCache.TryGetValue(filePath, out item))
                    item = LoadFile(filePath, parent);

                parent = item;
            }

            ExportRegistry result = LoadFile(GetConfigurationFilename(assembly), parent);
            result.IsExportAssembly = true;
            result.Assembly = assembly;
            return result;
        }

        private ExportRegistry LoadFile(string filePath, ExportRegistry parent)
        {
            ExportRegistry result = new ExportRegistry(parent);

            XmlDocument document = new XmlDocument();
            document.Load(filePath);

            foreach (XmlElement element in document.GetElementsByTagName("ExternalTypes"))
            {
                foreach (XmlElement typeElement in element.GetElementsByTagName("Type"))
                    result.AddExternalType(typeElement.GetAttribute("Target"));
            }

            foreach (XmlElement element in document.GetElementsByTagName("Export"))
                result.DefaultExport = LoadExport(element);

            foreach (XmlElement element in document.GetElementsByTagName("Namespace"))
                result.AddNamespace(LoadNamespace(element, result));

            foreach (XmlElement element in document.GetElementsByTagName("Type"))
                result.AddType(LoadType(element, result));

            foreach (XmlElement element in document.GetElementsByTagName("Merge"))
                result.AddMerge(LoadMerge(element));

            result.BuildUp();
            return result;
        }

        /// <summary>
        /// Loads export registration for assembly.
        /// </summary>
        /// <param name="assembly">Assembly definition.</param>
        /// <returns>Export registration for assembly</returns>
        public ExportRegistry Load(IAssembly assembly)
        {
            if (!IsConfigurationFile(assembly))
            {
                return new ExportRegistry()
                {
                    IsExportAssembly = false,
                    Assembly = assembly
                };
            }

            ExportRegistry result = CreateInstance(assembly);
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
            item.AutomaticPropertiesAsFields = XmlUtil.GetAttributeBool(element, "AutomaticPropertiesAsFields") ?? registry.GetType(item.Target).AutomaticPropertiesAsFields;
            item.Export = XmlUtil.GetAttributeBool(element, "Export") ?? registry.GetType(item.Target).Export;
            item.Mode = XmlUtil.GetAttributeEnum<JsMode>(element, "Mode") ?? registry.GetType(item.Target).Mode;
            item.PropertiesAsFields = XmlUtil.GetAttributeBool(element, "PropertiesAsFields") ?? registry.GetType(item.Target).PropertiesAsFields;
            item.Filename = registry.ApplyFilenameFormat(XmlUtil.GetAttributeString(element, "Filename")) ?? registry.GetType(item.Target).Filename;
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
