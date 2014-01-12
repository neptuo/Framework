using Neptuo.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntSearch = Neptuo.Data.Queries.IntSearch;
using TextSearch = Neptuo.Data.Queries.TextSearch;
using DoubleSearch = Neptuo.Data.Queries.DoubleSearch;

namespace TestConsole.Queries
{
    class TestQueries
    {
        public static void Test()
        {
            IQuery<ProductModel, IProductFilter> query = null;

            query
                .Where(f => f.Key, IntSearch.Create(5))
                .Or(
                    query
                        .Where(f => f.Name, TextSearch.Create("Spa"))
                );
        }
    }
}
