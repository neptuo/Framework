using Neptuo.Bootstrap.Dependencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Hierarchies.Sorting
{
    /// <summary>
    /// Implementation of <see cref="ISortInputProvider"/> and <see cref="ISortOutputProvider"/> based on properties decorated 
    /// by <see cref="ImportAttribute"/> and <see cref="ExportAttribute"/>.
    /// </summary>
    public class PropertyImportExportProvider : ISortInputProvider, ISortOutputProvider
    {
        public IEnumerable<Type> GetInputs(Type type)
        {
            Ensure.NotNull(type, "type");
            List<Type> result = new List<Type>();
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                ImportAttribute attribute = propertyInfo.GetCustomAttribute<ImportAttribute>();
                if (attribute != null)
                    result.Add(propertyInfo.PropertyType);
            }

            return result;
        }

        public IEnumerable<Type> GetOutputs(Type type)
        {
            Ensure.NotNull(type, "type");
            List<Type> result = new List<Type>();
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                ExportAttribute attribute = propertyInfo.GetCustomAttribute<ExportAttribute>();
                if (attribute != null)
                    result.Add(propertyInfo.PropertyType);
            }

            return result;
        }
    }
}
