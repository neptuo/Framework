using Neptuo.Data.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Queries
{
    public interface IProductFilter
    {
        IntSearch Key { get; set; }
        TextSearch Name { get; set; }
        TextSearch Code { get; set; }
        DoubleSearch Price { get; set; }
    }

    public class ProductModel
    {
        public int Key { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public double Price { get; set; }
    }
}
