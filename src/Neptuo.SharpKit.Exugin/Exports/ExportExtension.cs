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
    public class ExportExtension : BaseExtension
    {
        private ExportService service;

        public ExportExtension(string[] additionalNameFileNames, bool debug = false)
            : base("Export", debug)
        {
            service = new ExportService(additionalNameFileNames);
        }

        public void Process(IEnumerable<IAssembly> assemblies, ICustomAttributeProvider attributeProvider)
        {
            if (assemblies == null)
                throw new ArgumentNullException("assemblies");

            if (attributeProvider == null)
                throw new ArgumentNullException("attributeProvider");

            foreach (IAssembly assembly in assemblies)
            {
                ExportRegistry registry = CreateRegistry(assembly);
                if (registry.IsExportAssembly)
                    ProcessRegistry(assembly, attributeProvider, registry);
            }
        }

        private ExportRegistry CreateRegistry(IAssembly assembly)
        {
            return service.Load(assembly);
        }

        private void ProcessRegistry(IAssembly assembly, ICustomAttributeProvider attributeProvider, ExportRegistry registry)
        {
            foreach (string typeName in registry.GetExternalTypes())
                attributeProvider.AddCustomAttribute(assembly, new JsTypeAttribute { TargetTypeName = typeName, Export = true });

            foreach (ITypeDefinition type in assembly.GetAllTypeDefinitions())
            {
                LogDebug("Processing type '{0}'", type.FullName);
                TypeRegistryItem typeItem = registry.GetType(type);
                ProcessType(type, attributeProvider, typeItem);
            }

            foreach (MergeFileItem item in registry.GetMergeItems())
                attributeProvider.AddCustomAttribute(assembly, new JsMergedFileAttribute { Filename = item.FileName, Sources = item.Sources, Minify = item.Minify });
        }

        private void ProcessType(ITypeDefinition type, ICustomAttributeProvider attributeProvider, TypeRegistryItem typeItem)
        {
            JsTypeAttribute typeAttribute = attributeProvider.GetCustomAttributes<JsTypeAttribute>(type).FirstOrDefault();
            if (typeAttribute == null)
            {
                typeAttribute = new JsTypeAttribute();
                attributeProvider.AddCustomAttribute(type, typeAttribute);
            }

            typeAttribute.Filename = typeItem.Filename;
            typeAttribute.Export = typeItem.Export;
            typeAttribute.Mode = typeItem.Mode;

            if (typeItem.AutomaticPropertiesAsFields != null)
                typeAttribute.AutomaticPropertiesAsFields = typeItem.AutomaticPropertiesAsFields.Value;

            if (typeItem.Name != null)
                typeAttribute.Name = typeItem.Name;

            if (typeItem.PropertiesAsFields != null)
                typeAttribute.PropertiesAsFields = typeItem.PropertiesAsFields.Value;

            //TODO: Methods...
        }
    }
}
