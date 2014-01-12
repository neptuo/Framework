using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.NRefactory.TypeSystem;
using SharpKit.Compiler;
using SharpKit.JavaScript;
using Mirrored.SharpKit.JavaScript;

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
                NamespaceRegistry registry = CreateRegistry(assembly);
                if (registry != null)
                    ProcessRegistry(assembly, attributeProvider, registry);
            }
        }

        /// <summary>
        /// Vytvoří registr pro <paramref name="assembly"/>.
        /// </summary>
        /// <param name="assembly">Assembly.</param>
        /// <returns>Registr namespaců a soborů k exportu.</returns>
        private NamespaceRegistry CreateRegistry(IAssembly assembly)
        {
            bool useExport = false;
            string defaultFilename = "{0}";
            string filenameFormat = null;
            Dictionary<string, string> mappings = new Dictionary<string, string>();

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
                NamespaceRegistry registry = new NamespaceRegistry(false, defaultFilename, filenameFormat);
                foreach (KeyValuePair<string, string> item in mappings)
                    registry.Add(item.Key, item.Value);

                return registry;
            }

            return null;
        }

        /// <summary>
        /// Zpracuje registr.
        /// </summary>
        /// <param name="assembly">Assembly.</param>
        /// <param name="attributeProvider">Poskytovatel konfiguračních atributů pro Sharpkit.</param>
        /// <param name="registry">Registr.</param>
        private void ProcessRegistry(IAssembly assembly, ICustomAttributeProvider attributeProvider, NamespaceRegistry registry)
        {
            foreach (ITypeDefinition type in assembly.GetAllTypeDefinitions())
            {
                string filename = registry.GetFilename(type.Namespace);

                IEnumerable<JsTypeAttribute> attributes = attributeProvider.GetCustomAttributes<JsTypeAttribute>(type);
                if (attributes.Any())
                {
                    attributes.First().Filename = filename;
                    LogDebug("Modifying jstype attribute for {0} to filename {1}", type.FullName, filename);
                }
                else
                {
                    attributeProvider.AddCustomAttribute(type, new JsTypeAttribute(JsMode.Clr, filename) { _Export = true });
                    LogDebug("Adding jstype attribute for {0} to filename {1}", type.FullName, filename);
                }
            }
        }
    }
}
