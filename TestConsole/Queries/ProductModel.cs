using Neptuo.Data.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Queries
{
    public class ProductFilter
    {
        public IntSearch Key { get; set; }
        public TextSearch Name { get; set; }
        public TextSearch Code { get; set; }
        public DoubleSearch Price { get; set; }
    }

    public class ProductModel
    {
        public int Key { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public double Price { get; set; }
    }
}
