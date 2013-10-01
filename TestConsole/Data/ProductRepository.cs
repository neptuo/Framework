using Neptuo.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Data
{
    public class ProductRepository : MappingEntityRepository<Product, ProductEntity, DataContext>, IProductRepository
    {
        public ProductRepository(DataContext dbContext)
            : base(dbContext)
        { }
    }
}
