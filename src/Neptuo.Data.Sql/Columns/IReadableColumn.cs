using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Sql.Columns
{
    /// <summary>
    /// Marker interface for making column available for reading value.
    /// </summary>
    /// <typeparam name="T">Value type of the column</typeparam>
    public interface IReadableColumn<T> : IColumn<T>
    { }
}
