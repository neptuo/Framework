using Neptuo.Data;
using Neptuo.Data.Entity.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Data.Commands
{
    public class ProductEntityQuery : EntityQuery<Product>, IProductQuery
    {
        public ProductEntityQuery(IProductRepository repository)
            : base(repository.Get())
        { }
    }
}
