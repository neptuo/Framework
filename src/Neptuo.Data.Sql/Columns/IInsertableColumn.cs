using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Sql.Columns
{
    /// <summary>
    /// Marker interface for making column available for inserting value.
    /// </summary>
    /// <typeparam name="T">Value type of the column</typeparam>
    public interface IInsertableColumn<T> : IColumn<T>
    { }
}
