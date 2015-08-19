using Neptuo.Behaviors;
using Neptuo.Bootstrap.Handlers;
using Neptuo.Bootstrap.Handlers.Metadata;
using Neptuo.Bootstrap.Processing.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Processing.Behaviors
{
    public class DependencyPropertyBehavior : IBehavior<IBootstrapHandler>
    {
        private readonly IValueInputProvider importer;
        private readonly IValueOutputProvider exporter;

        public DependencyPropertyBehavior(IValueInputProvider importer, IValueOutputProvider exporter)
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
