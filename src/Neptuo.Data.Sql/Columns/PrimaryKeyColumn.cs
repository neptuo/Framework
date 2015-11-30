using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Sql.Columns
{
    public class PrimaryKeyColumn<T> : IColumn<T>, IReadableColumn<T>, IInsertableColumn<T>
    {
        public IColumnPath Path { get; private set; }

        public PrimaryKeyColumn(IColumnPath path)
        {
            Ensure.NotNull(path, "path");
            Path = path;
        }
    }
}
