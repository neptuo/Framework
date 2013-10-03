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

        public new IProductFilter Filter
        {
            get { throw new NotImplementedException(); }
        }

        public new Neptuo.Data.Queries.IQuery<Product, IProductFilter> OrderBy(System.Linq.Expressions.Expression<Func<Product, object>> sorter)
        {
            throw new NotImplementedException();
        }

        public new Neptuo.Data.Queries.IQuery<Product, IProductFilter> OrderByDescending(System.Linq.Expressions.Expression<Func<Product, object>> sorter)
        {
            throw new NotImplementedException();
        }

        public new Neptuo.Data.Queries.IQuery<Product, IProductFilter> Page(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
