using Neptuo.Data.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Data.Sql
{
    public class ProductTable<TInt>
    {
        public PrimaryKeyColumn<TInt> Id { get; private set; }
        public TableColumn<string> Name { get; private set; }
        public TableColumn<decimal?> Price { get; private set; }
    }

    public class ProductTable : ProductTable<int>
    { }

    public class ProductLeftJoin : ProductTable<int?>
    { }

    public class TermTable<TInt, TDateTime>
    {
        public PrimaryKeyColumn<TInt> Id { get; private set; }
        public TableColumn<TDateTime> DateFrom { get; private set; }
        public TableColumn<TDateTime> DateTo { get; private set; }

        public ProductTable<int?> Product { get; private set; }
    }

    public class TermTable : TermTable<int, DateTime>
    { }

    public class TermLeftJoin : TermTable<int?, DateTime?>
    { }
}
