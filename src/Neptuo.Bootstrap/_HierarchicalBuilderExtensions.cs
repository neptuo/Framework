using Neptuo.Bootstrap.Processing.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Hierarchies
{
    /// <summary>
    /// Hierarchical extensions for <see cref="HierarchicalBuilder"/>.
    /// </summary>
    public static class _HierarchicalBuilderExtensions
    {
        /// <summary>
        /// Creates hierarchical buidler with <paramref name="importer"/> and <paramref name="exporter"/> for importing and exporting dependencies.
        /// </summary>
        /// <param name="builder">Bootstrapper builder.</param>
        /// <param name="importer">Value importer to be used for importing handler inputs.</param>
        /// <param name="exporter">Value exporter to be used for exporting handler outputs.</param>
        public static HierarchicalBuilder ToHierarchical(this Builder builder, IValueInputProvider importer, IValueOutputProvider exporter)
        {
            Ensure.NotNull(builder, "builder");
            return new HierarchicalBuilder(importer, exporter);
        }
    }
}
