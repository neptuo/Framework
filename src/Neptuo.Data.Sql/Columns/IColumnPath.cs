using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Sql.Columns
{
    /// <summary>
    /// Describes column path (including joins and etc).
    /// </summary>
    public interface IColumnPath
    {
        /// <summary>
        /// Current name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Full name including parent path.
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// Parent column path.
        /// </summary>
        IColumnPath Parent { get; }
    }
}
