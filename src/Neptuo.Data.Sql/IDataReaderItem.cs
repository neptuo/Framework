using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Sql
{
    /// <summary>
    /// Describes single row from database.
    /// </summary>
    public interface IDataReaderItem
    {
        /// <summary>
        /// Returns value of <paramref name="column"/>.
        /// </summary>
        /// <typeparam name="T">Value type of the column.</typeparam>
        /// <param name="column">The column to return value of.</param>
        /// <returns>The value of <paramref name="column"/>.</returns>
        T GetValue<T>(IReadableColumn<T> column);
    }
}
