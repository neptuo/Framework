using Neptuo.Data;
using Neptuo.Data.Entity.Queries;
using Neptuo.Data.Queries;
using Neptuo.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Data.Queries
{
    public class ProductEntityQuery : MappingEntityQuery<Product, ProductEntity, IProductFilter>, IProductQuery
    {
        public ProductEntityQuery(DataContext dataContext)
            : base(dataContext.Products)
        { }

        protected override Expression BuildWhereExpression(Expression parameter, Dictionary<string, object> whereFilters)
        {
            Expression target = null;
            foreach (var whereFilter in whereFilters)
            {
                if (whereFilter.Key == TypeHelper.PropertyName<IProductFilter, object>(p => p.Key))
                {
                    target = EntityQuerySearch.BuildIntSearch<ProductEntity>(target, parameter, p => p.ID, (IntSearch)whereFilter.Value);
                }
                else if (whereFilter.Key == TypeHelper.PropertyName<IProductFilter, object>(p => p.Name))
                {
                    target = EntityQuerySearch.BuildTextSearch<Product>(target, parameter, p => p.Name, (TextSearch)whereFilter.Value);
                }
            }

            return target;
        }
    }
}
