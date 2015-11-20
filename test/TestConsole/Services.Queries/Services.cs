using Neptuo.Queries;
using Neptuo.Queries.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Services.Queries
{
    public class ProductQuery : IQuery<Product>
    {
        public string Name { get; set; }
    }

    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    [Log]
    public class ProductQueryHandler : IQueryHandler<ProductQuery, Product>
    {
        public Task<Product> HandleAsync(ProductQuery query)
        {
            return Task.FromResult(new Product()
            {
                Name = query.Name,
                Price = 10.1M
            });
        }
    }
}
