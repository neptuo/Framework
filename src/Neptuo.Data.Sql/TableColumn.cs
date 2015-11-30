using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Sql
{
    public class TableColumn<T> : IColumn<T>
    {
        public IColumnPath Path { get; private set; }

        public TableColumn(IColumnPath path)
        {
            Ensure.NotNull(path, "path");
            Path = path;
        }
    }
}
