using Neptuo.Data.Sql;
using Neptuo.Data.Sql.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Data.Sql
{
    public class ProductTable<TIdColumn>
    {
        public TIdColumn Id { get; private set; }
        public TableColumn<string> Name { get; private set; }
        public TableColumn<decimal?> Price { get; private set; }
    }

    public class ProductTable : ProductTable<PrimaryKeyColumn<int>>
    { }

    public class ProductLeftJoin : ProductTable<ForeignKeyColumn<int?>>
    { }

    public class TermTable<TInt, TDateTime>
    {
        public PrimaryKeyColumn<TInt> Id { get; private set; }
        public TableColumn<TDateTime> DateFrom { get; private set; }
        public TableColumn<TDateTime> DateTo { get; private set; }

        public ProductLeftJoin Product { get; private set; }
    }

    public class TermTable : TermTable<int, DateTime>
    { }

    public class TermLeftJoin : TermTable<int?, DateTime?>
    { }
}
