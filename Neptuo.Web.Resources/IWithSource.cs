using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Resources
{
    /// <summary>
    /// Base source with source property.
    /// </summary>
    public interface IWithSource
    {
        /// <summary>
        /// Source path.
        /// </summary>
        string Source { get; }
    }
}
