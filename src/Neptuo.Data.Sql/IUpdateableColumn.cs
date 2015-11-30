using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Sql
{
    /// <summary>
    /// Marker interface for making column available for updating value.
    /// </summary>
    /// <typeparam name="T">Value type of the column</typeparam>
    public interface IUpdateableColumn<T> : IColumn<T>
    { }
}
