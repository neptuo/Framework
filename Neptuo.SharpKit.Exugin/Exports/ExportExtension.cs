using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.NRefactory.TypeSystem;
using SharpKit.Compiler;
using SharpKit.JavaScript;
using Mirrored.SharpKit.JavaScript;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Neptuo.SharpKit.Exugin.Exports
{
    /// <summary>
    /// Extension pro exportování do js souborů.
    /// Podporuje export podle namespace a export ostatních soborů do jednoho souboru.
    /// Využívá jména atributů, které dostává v ctoru.
    /// Oba tyto atributy mají dva parametry.
    /// U <code>ExportAttribute</code> je to výchozí jméno souboru a formátovací řetězec aplikovaný na jména.
    /// U <code>ExportNamespaceAttribute</code> je to jméno namespacu a jména kam se má exportovat (na který je aplikovaný formátovací řetězec).
    /// </summary>
    public class ExportExtension : BaseExtension
    {
        private string defaultAttributeName;
        private string namespaceAttributeName;

        public ExportExtension(string defaultAttributeName, string namespaceAttributeName, bool debug = false)
            : base("Export", debug)
        {
            this.defaultAttributeName = defaultAttributeName;
            this.namespaceAttributeName = namespaceAttributeName;
        }

        /// <summary>
        /// Spustí nastavení exportů.
        /// </summary>
        /// <param name="assemblies">Assembly ke zpracování.</param>
        /// <param name="attributeProvider">Poskytovatel konfiguračních atributů pro Sharpkit.</param>
        public void Process(IEnumerable<IAssembly> assemblies, ICustomAttributeProvider attributeProvider)
        {
            if (assemblies == null)
                throw new ArgumentNullException("assemblies");

            if (attributeProvider == null)
                throw new ArgumentNullException("attributeProvider");

            foreach (IAssembly assembly in assemblies)
            {
                ExportRegistry registry = CreateRegistry(assembly);
                if (registry != null)
                    ProcessRegistry(assembly, attributeProvider, registry);
            }
        }

        /// <summary>
        /// Vytvoří registr pro <paramref name="assembly"/>.
        /// </summary>
        /// <param name="assembly">Assembly.</param>
        /// <returns>Registr namespaců a soborů k exportu.</returns>
        private ExportRegistry CreateRegistry(IAssembly assembly)
        {
            bool useExport = false;
            string defaultFilename = "{0}";
            string filenameFormat = null;
            Dictionary<string, string> mappings = new Dictionary<string, string>();

            ExportRegistry registry = new ExportRegistry();
            TryReadConfigurationXml(assembly, registry);

            foreach (IAttribute attribute in assembly.AssemblyAttributes)
            {
                if (attribute.AttributeType.Name == namespaceAttributeName)
                {
                    if (attribute.PositionalArguments.Count != 2)
                    {
                        LogDebug("Missing argument int namespace export, attribute must has 2 arguments, current has {0}", attribute.PositionalArguments.Count);
                        continue;
                    }

                    if (attribute.PositionalArguments[1].ConstantValue != null && attribute.PositionalArguments[0].ConstantValue != null)
                        mappings[attribute.PositionalArguments[1].ConstantValue.ToString()] = attribute.PositionalArguments[0].ConstantValue.ToString();
                    else
                        LogDebug("Missing argument value in namespace export, first: {0}; second: {1}", attribute.PositionalArguments[0].ConstantValue, attribute.PositionalArguments[1].ConstantValue);

                    useExport = true;
                }
                else if (attribute.AttributeType.Name == defaultAttributeName)
                {
                    if (attribute.PositionalArguments[0].ConstantValue != null)
                        defaultFilename = attribute.PositionalArguments[0].ConstantValue.ToString();

                    if (attribute.PositionalArguments[1].ConstantValue != null)
                        filenameFormat = attribute.PositionalArguments[1].ConstantValue.ToString();

                    LogDebug("Default filename format: {0}; Default filename: {1}", filenameFormat, defaultFilename);
                    useExport = true;
                }
            }

            if (useExport)
            {
                registry.Namespaces = new NamespaceRegistry(false, defaultFilename, filenameFormat);

                foreach (KeyValuePair<string, string> item in mappings)
                    registry.Namespaces.Add(item.Key, item.Value);
            }

            if (registry.Namespaces != null || registry.Types != null)
                return registry;

            return null;
        }

        #region Read configuration

        private void TryReadConfigurationXml(IAssembly assembly, ExportRegistry registry)
        {
            if (IsConfigurationXml(assembly))
            {
                XmlDocument document = new XmlDocument();
                document.Load(GetConfigurationXmlFileName(assembly));

                string defaultFilename = null;
                string filenameFormat = null;
                
                foreach (XmlElement element in document.GetElementsByTagName(defaultAttributeName.Replace("Attribute", "")))
                {
                    defaultFilename = element.GetAttribute("DefaultFilename");
                    filenameFormat = element.GetAttribute("FilenameFormat") ?? "{0}";
                }

                if (defaultFilename != null || filenameFormat != null || registry.Namespaces != null)
                {
                    if (registry.Namespaces == null)
                    {
                        registry.Namespaces = new NamespaceRegistry(false, defaultFilename, filenameFormat);
                    }
                    else
                    {
                        if (defaultFilename != null)
                            registry.Namespaces.DefaultFilename = defaultFilename;

                        if (filenameFormat != null)
                            registry.Namespaces.FilenameFormat = filenameFormat;
                    }

                    foreach (XmlElement element in document.GetElementsByTagName(namespaceAttributeName.Replace("Attribute", "")))
                        registry.Namespaces.Add(element.GetAttribute("namespace"), element.GetAttribute("filename"));
                }

                registry.Types = ReadTypeConfiguration(document);
            }
        }

        private TypeRegistry ReadTypeConfiguration(XmlDocument document)
        {
            TypeRegistry registry = new TypeRegistry();
            foreach (XmlElement element in document.GetElementsByTagName("JsType"))
            {
                TypeRegistryItem item = new TypeRegistryItem
                {
                    AutomaticPropertiesAsFields = GetAttributeBool(element, "AutomaticPropertiesAsFields"),
                    Export = GetAttributeBool(element, "Export", true).Value,
                    Mode = GetAttributeEnum<JsMode>(element, "Mode"),
                    Name = GetAttributeString(element, "Name"),
                    OrderInFile = GetAttributeInt(element, "OrderInFile"),
                    PropertiesAsFields = GetAttributeBool(element, "PropertiesAsFields"),
                    TargetType = GetAttributeString(element, "TargetType")
                };
                
                //TODO: Methods

                if (String.IsNullOrEmpty(item.TargetType))
                    registry.AddDefault(item);
                else
                    registry.Add(item.TargetType, item);
            }
            return registry;
        }

        private string GetAttributeString(XmlElement element, string attributeName)
        {
            if (element.HasAttribute(attributeName))
                return element.GetAttribute(attributeName);

            return null;
        }

        private bool? GetAttributeBool(XmlElement element, string attributeName, bool? defaultValue = null)
        {
            string value = GetAttributeString(element, attributeName);
            bool targetValue;
            if (Boolean.TryParse(value, out targetValue))
                return targetValue;

            return defaultValue;
        }

        private int? GetAttributeInt(XmlElement element, string attributeName)
        {
            string value = GetAttributeString(element, attributeName);
            int targetValue;
            if (Int32.TryParse(value, out targetValue))
                return targetValue;

            return null;
        }

        private T? GetAttributeEnum<T>(XmlElement element, string attributeName)
            where T : struct
        {
            string value = GetAttributeString(element, attributeName);
            T targetValue;
            if (Enum.TryParse<T>(value, out targetValue))
                return targetValue;

            return null;
        }

        #endregion

        private bool IsConfigurationXml(IAssembly assembly)
        {
            return File.Exists(GetConfigurationXmlFileName(assembly));
        }

        private string GetConfigurationXmlFileName(IAssembly assembly)
        {
            string assemblyName = assembly.AssemblyName + ".xml";
            return assemblyName;
        }

        /// <summary>
        /// Zpracuje registr.
        /// </summary>
        /// <param name="assembly">Assembly.</param>
        /// <param name="attributeProvider">Poskytovatel konfiguračních atributů pro Sharpkit.</param>
        /// <param name="registry">Registr.</param>
        private void ProcessRegistry(IAssembly assembly, ICustomAttributeProvider attributeProvider, ExportRegistry registry)
        {
            foreach (ITypeDefinition type in assembly.GetAllTypeDefinitions())
            {
                LogDebug("Processing type '{0}'", type.FullName);
                TypeRegistryItem typeItem = registry.Types.Get(type.FullName);
                string filename = registry.Namespaces.GetFilename(type.Namespace);
                ProcessType(type, attributeProvider, typeItem, filename);
            }
        }

        private void ProcessType(ITypeDefinition type, ICustomAttributeProvider attributeProvider, TypeRegistryItem typeItem, string filename)
        {
            JsTypeAttribute typeAttribute = attributeProvider.GetCustomAttributes<JsTypeAttribute>(type).FirstOrDefault();
            if (typeAttribute == null)
            {
                typeAttribute = new JsTypeAttribute();
                attributeProvider.AddCustomAttribute(type, typeAttribute);
            }

            if (filename != null || typeItem != null)
            {
                typeAttribute.Filename = filename;

                if (typeItem != null)
                {
                    typeAttribute._Export = typeItem.Export;
                    typeAttribute.Mode = typeItem.Mode ?? JsMode.Clr;

                    if (typeItem.AutomaticPropertiesAsFields != null)
                        typeAttribute.AutomaticPropertiesAsFields = typeItem.AutomaticPropertiesAsFields.Value;

                    if (typeItem.Name != null)
                        typeAttribute.Name = typeItem.Name;

                    if (typeItem.PropertiesAsFields != null)
                        typeAttribute.PropertiesAsFields = typeItem.PropertiesAsFields.Value;
                    
                    //TODO: Methods
                }
                //else
                //{
                //    typeAttribute.Export = true;
                //}
            }
        }
    }
}
