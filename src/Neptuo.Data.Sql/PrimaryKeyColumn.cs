using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Sql
{
    public class PrimaryKeyColumn<T> : IColumn<T>, IReadableColumn<T>
    {
        public IColumnPath Path { get; private set; }

        public PrimaryKeyColumn(IColumnPath path)
        {
            Ensure.NotNull(path, "path");
            Path = path;
        }
    }
}
