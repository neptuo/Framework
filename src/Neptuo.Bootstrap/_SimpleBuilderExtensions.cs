using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap
{
    /// <summary>
    /// Simple extensions for <see cref="Builder"/>
    /// </summary>
    public static class _SimpleBuilderExtensions
    {
        /// <summary>
        /// Creates simple buidler.
        /// </summary>
        /// <param name="builder">Bootstrapper builder.</param>
        public static SimpleBuilder ToSimple(this Builder builder)
        {
            Ensure.NotNull(builder, "builder");
            return new SimpleBuilder();
        }
    }
}
