using Neptuo.Data.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Data.Queries
{
    public class ProductFilter : IProductFilter
    {
        public IntSearch Key { get; set; }
        public TextSearch Name { get; set; }
        public DoubleSearch Price { get; set; }

        public ICategoryFilter Category { get; set; }

        public DateTimeSearch AvailableFrom { get; set; }
        public DateTimeSearch StopSale { get; set; }

        public BoolSearch IsDiscount { get; set; }

    }
}
