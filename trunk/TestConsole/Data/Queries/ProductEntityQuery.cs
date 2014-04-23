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

        protected override Expression BuildWhereExpression(ParameterExpression parameter)
        {
            Expression target = null;

            if (Filter.Key != null)
                target = EntityQuerySearch.BuildIntSearch<ProductEntity>(target, parameter, p => p.Key, Filter.Key);

            if (Filter.Name != null)
                target = EntityQuerySearch.BuildTextSearch<ProductEntity>(target, parameter, p => p.Name, Filter.Name);

            //if (Filter.CategoryKey != null)
            //    target = EntityQuerySearch.BuildIntSearch<ProductEntity>(target, parameter, p => p.CategoryEntity.Key, Filter.CategoryKey);

            if (Filter.AvailableFrom != null)
                target = EntityQuerySearch.BuildDateTimeSearch<ProductEntity>(target, parameter, p => p.AvailableFrom, Filter.AvailableFrom);

            if (Filter.StopSale != null)
                target = EntityQuerySearch.BuildDateTimeSearch<ProductEntity>(target, parameter, p => p.StopSale, Filter.StopSale);

            if (Filter.IsDiscount != null)
                target = EntityQuerySearch.BuildBoolSearch<ProductEntity>(target, parameter, p => p.IsDiscount, Filter.IsDiscount);

            return target;
        }

        protected override IProductFilter CreateFilter()
        {
            return new ProductFilter();
        }
    }
}
