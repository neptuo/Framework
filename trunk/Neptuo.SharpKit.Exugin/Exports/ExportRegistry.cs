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
        private Dictionary<string, TypeRegistryItem> typeItems;
        private List<NamespaceRegistryItem> namespaceItems;
        private List<MergeFileItem> mergeFiles;

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
        /// Adds definition for merging files.
        /// </summary>
        /// <param name="item">Merge file definition.</param>
        public void AddItem(MergeFileItem item)
        {
            if (mergeFiles == null)
                mergeFiles = new List<MergeFileItem>();

            mergeFiles.Add(item);
        }

        /// <summary>
        /// Builds registration structures after items are added.
        /// </summary>
        public void BuildUp()
        {
            if (namespaceItems != null)
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
                foreach (NamespaceRegistryItem item in namespaceItems)
                {
                    if (IsInNamespace(targetTypeName, item.Target))
                    {
                        TypeRegistryItem typeItem = new TypeRegistryItem
                        {
                            AutomaticPropertiesAsFields = item.AutomaticPropertiesAsFields,
                            Export = item.Export,
                            Filename = item.Filename,
                            Mode = item.Mode,
                            PropertiesAsFields = item.PropertiesAsFields
                        };

                        if (typeItems == null)
                            typeItems = new Dictionary<string, TypeRegistryItem>();

                        typeItems[targetTypeName] = typeItem;
                        return typeItem;
                    }
                }
            }

            // Applied also when only DefaultExport is used.
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
        /// Determins whether <paramref name="targetTypeName"/> is in <paramref name="namespaceName"/>.
        /// If <paramref name="namespaceName"/> ends with dot ('.'), <paramref name="targetTypeName"/> can be in that namespace or any subnamespace.
        /// </summary>
        /// <param name="targetTypeName"></param>
        /// <param name="namespaceName"></param>
        /// <returns></returns>
        private bool IsInNamespace(string targetTypeName, string namespaceName)
        {
            if (targetTypeName == null)
                throw new ArgumentNullException("targetTypeName");

            if (namespaceName == null)
                throw new ArgumentNullException("namespaceName");

            if (namespaceName.EndsWith(".") && targetTypeName.StartsWith(namespaceName.Substring(0, namespaceName.Length - 1)))
                return true;

            int lastIndexOfDot = targetTypeName.LastIndexOf('.');
            if (lastIndexOfDot == -1)
                return namespaceName == String.Empty;

            string typeNamespaceName = targetTypeName.Substring(0, lastIndexOfDot);
            return typeNamespaceName == namespaceName;
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

            return ApplyFilenameFormat(filename);
        }

        /// <summary>
        /// Applies file name format from DefaultExport if used.
        /// </summary>
        /// <param name="filename">File name.</param>
        /// <returns>File name format from DefaultExport if used.</returns>
        public string ApplyFilenameFormat(string filename)
        {
            if (filename == null)
                return null;

            if (DefaultExport != null && !String.IsNullOrEmpty(DefaultExport.FilenameFormat))
                return String.Format(DefaultExport.FilenameFormat, filename);

            return filename;
        }

        /// <summary>
        /// Returns definitions for merging files.
        /// </summary>
        /// <returns>Definitions for merging files.</returns>
        public IEnumerable<MergeFileItem> GetMergeItems()
        {
            return mergeFiles ?? new List<MergeFileItem>();
        }
    }
}
