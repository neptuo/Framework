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
    }
}
