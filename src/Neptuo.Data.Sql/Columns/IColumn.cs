using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Sql.Columns
{
    /// <summary>
    /// Describes column.
    /// </summary>
    public interface IColumn
    {
        /// <summary>
        /// Column path.
        /// </summary>
        IColumnPath Path { get; }
    }

    /// <summary>
    /// Describes column of concrete type.
    /// </summary>
    /// <typeparam name="T">Value type of the column.</typeparam>
    public interface IColumn<T> : IColumn
    { }
}
