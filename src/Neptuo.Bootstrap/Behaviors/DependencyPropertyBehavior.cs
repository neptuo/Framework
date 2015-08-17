using Neptuo.Behaviors;
using Neptuo.Bootstrap.Dependencies;
using Neptuo.Bootstrap.Dependencies.Handlers;
using Neptuo.Bootstrap.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Behaviors
{
    public class DependencyPropertyBehavior : IBehavior<IBootstrapHandler>
    {
        private readonly IDependencyImporter importer;
        private readonly IDependencyExporter exporter;

        public DependencyPropertyBehavior(IDependencyImporter importer, IDependencyExporter exporter)
        {
            Ensure.NotNull(importer, "importer");
            Ensure.NotNull(exporter, "exporter");
            this.importer = importer;
            this.exporter = exporter;
        }

        public async Task ExecuteAsync(IBootstrapHandler handler, IBehaviorContext context)
        {
            foreach (PropertyInfo propertyInfo in handler.GetType().GetProperties())
            {
                if (propertyInfo.GetCustomAttribute<ImportAttribute>() != null)
                    propertyInfo.SetValue(handler, importer.Import(propertyInfo.PropertyType));
            }

            await context.NextAsync();

            foreach (PropertyInfo propertyInfo in handler.GetType().GetProperties())
            {
                if (propertyInfo.GetCustomAttribute<ExportAttribute>() != null)
                    exporter.Export(propertyInfo.PropertyType, propertyInfo.GetValue(handler));
            }
        }
    }
}
