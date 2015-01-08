using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Queries
{
    public interface IMultiValueQuerySearch<T> : IQuerySearch
    {
        IReadOnlyList<T> Value { get; }

    }
}
