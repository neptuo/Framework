using ICSharpCode.NRefactory.TypeSystem;
using Mirrored.SharpKit.JavaScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.SharpKit.Exugin.Exports
{
    public class ExportRegistry
    {
        Dictionary<string, TypeRegistryItem> typeItems;
        List<NamespaceRegistryItem> namespaceItems;

        /// <summary>
        /// Bound assembly definition.
        /// </summary>
        public IAssembly Assembly { get; set; }

        /// <summary>
        /// Identifiers whether anything is exporting from this assembly.
        /// </summary>
        public bool IsExportAssembly { get; set; }

        /// <summary>
        /// Default export settings.
        /// </summary>
        public ExportItem DefaultExport { get; set; }

        /// <summary>
        /// Adds registration for namespace.
        /// </summary>
        /// <param name="item">Namespace registration item.</param>
        public void AddItem(NamespaceRegistryItem item)
        {
            if (item.Target == null)
                throw new ArgumentNullException("item.Target");

            if (String.IsNullOrEmpty(item.Target))
                throw new ArgumentOutOfRangeException("item.Target", "Namespace name can't be empty.");

            if (item.Target.EndsWith("."))
                throw new ArgumentOutOfRangeException("item.Target", "Namespace name can't ends with '.' (letter, number or * is expected).");

            if (item.Target.EndsWith("*"))
                item.Target = item.Target.Substring(0, item.Target.Length - 1);

            if (namespaceItems == null)
                namespaceItems = new List<NamespaceRegistryItem>();

            namespaceItems.Add(item);
        }

        /// <summary>
        /// Adds registration for type.
        /// </summary>
        /// <param name="item">Type registration item.</param>
        public void AddItem(TypeRegistryItem item)
        {
            if (typeItems == null)
                typeItems = new Dictionary<string, TypeRegistryItem>();

            if (item.Target == null)
                item.Target = String.Empty;

            typeItems[item.Target] = item;
        }

        /// <summary>
        /// Builds registration structures after items are added.
        /// </summary>
        public void BuildUp()
        {
            namespaceItems = new List<NamespaceRegistryItem>(namespaceItems.OrderByDescending(item => item.Target.Length));
        }

        /// <summary>
        /// Returns registration for <paramref name="targetType"/>.
        /// </summary>
        /// <param name="targetType">Type definition.</param>
        /// <returns>Registration for <paramref name="targetType"/>.</returns>
        public TypeRegistryItem GetItem(ITypeDefinition targetType)
        {
            return GetItem(targetType.FullTypeName.ReflectionName);
        }

        /// <summary>
        /// Returns registration for <paramref name="targetTypeName"/>.
        /// </summary>
        /// <param name="targetTypeName">Type name.</param>
        /// <returns>Registration for <paramref name="targetTypeName"/>.</returns>
        public TypeRegistryItem GetItem(string targetTypeName)
        {
            if (targetTypeName == null)
                targetTypeName = String.Empty;

            if (typeItems != null)
            {
                TypeRegistryItem item;
                if (typeItems.TryGetValue(targetTypeName, out item))
                    return item;
            }

            if (namespaceItems != null)
            {
                //TODO: Get by applying namespaces.
            }

            if(IsExportAssembly)
            {
                return new TypeRegistryItem
                {
                    Export = true,
                    Filename = GetFilenameWithDefaultExport(null),
                    Mode = JsMode.Clr,
                };
            }

            // Return empty item.
            return new TypeRegistryItem();
        }

        /// <summary>
        /// Returns filename with DefaultExport applied to it.
        /// </summary>
        /// <param name="filename">File name.</param>
        /// <returns>Filename with DefaultExport applied to it.</returns>
        private string GetFilenameWithDefaultExport(string filename)
        {
            if(String.IsNullOrEmpty(filename))
            {
                if (DefaultExport == null)
                    return null;

                filename = DefaultExport.Filename;
            }

            if (DefaultExport != null && !String.IsNullOrEmpty(DefaultExport.FilenameFormat))
                return String.Format(DefaultExport.FilenameFormat, filename);

            return filename;
        }
    }
}
