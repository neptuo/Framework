using Neptuo.Bootstrap.Handlers.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Processing.Metadata.Sorting
{
    public class TypeImportExportProvider : ISortInputProvider, ISortOutputProvider
    {
        public IEnumerable<Type> GetInputs(Type type)
        {
            Ensure.NotNull(type, "type");
            HashSet<Type> result = new HashSet<Type>();
            foreach (ImportTypeAttribute attribute in type.GetCustomAttributes<ImportTypeAttribute>())
                result.Add(attribute.Type);

            return result;
        }

        public IEnumerable<Type> GetOutputs(Type type)
        {
            Ensure.NotNull(type, "type");
            HashSet<Type> result = new HashSet<Type>();
            foreach (ExportTypeAttribute attribute in type.GetCustomAttributes<ExportTypeAttribute>())
                result.Add(attribute.Type);

            return result;
        }
    }
}
