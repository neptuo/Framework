using Neptuo.Data.Sql.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Sql
{
    /// <summary>
    /// Describes single row to be inserted to the database.
    /// </summary>
    public interface IDataInserterItem
    {
        /// <summary>
        /// Sets column to be inserted to the database.
        /// </summary>
        /// <typeparam name="T">Value type of the column.</typeparam>
        /// <param name="column">The column insert value of.</param>
        /// <param name="value">The value to be inserted.</param>
        /// <returns>Self (for fluency).</returns>
        IDataInserterItem SetValue<T>(IInsertableColumn<T> column, T value);
    }
}
