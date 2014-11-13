using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Dependencies.Providers
{
    public class TaskPropertyProvider : ITaskDependencyProvider
    {
        public IEnumerable<ITaskImportDescriptor> GetImports(Type taskType)
        {
            List<ITaskImportDescriptor> result = new List<ITaskImportDescriptor>();
            foreach (PropertyInfo propertyInfo in taskType.GetProperties())
            {
                ImportAttribute attribute = propertyInfo.GetCustomAttribute<ImportAttribute>();
                if (attribute != null)
                    result.Add(new TaskPropertyImportDescriptor(new ImportAttributeTarget(propertyInfo, attribute), propertyInfo));
            }

            return result;
        }

        public IEnumerable<ITaskExportDescriptor> GetExports(Type taskType)
        {
            List<ITaskExportDescriptor> result = new List<ITaskExportDescriptor>();
            foreach (PropertyInfo propertyInfo in taskType.GetProperties())
            {
                ExportAttribute attribute = propertyInfo.GetCustomAttribute<ExportAttribute>();
                if (attribute != null)
                    result.Add(new TaskPropertyExportDescriptor(new ExportAttributeTarget(propertyInfo, attribute), propertyInfo));
            }

            return result;
        }
    }
}
