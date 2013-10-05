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
        public ProductEntityQuery(IProductRepository repository)
            : base((IQueryable<ProductEntity>)repository.Get())
        { }

        protected override Expression BuildWhereExpression(Expression parameter, Dictionary<string, IQuerySearch> whereFilters)
        {
            Expression target = null;
            foreach (var whereFilter in whereFilters)
            {
                if (whereFilter.Key == TypeHelper.PropertyName<IProductFilter, object>(p => p.Key))
                {
                    target = EntityQuerySearch.BuildIntSearch<ProductEntity>(target, parameter, p => p.ID, (IntSearch)whereFilter.Value);
                    //IntSearch intSearch = (IntSearch)whereFilter.Value;
                    //if (intSearch.Value.Count == 0)
                    //    continue;

                    //if (intSearch.Value.Count == 1)
                    //    items = items.Where(i => i.Key == intSearch.Value[0]);
                }
                else if (whereFilter.Key == TypeHelper.PropertyName<IProductFilter, object>(p => p.Name))
                {
                    target = EntityQuerySearch.BuildTextSearch<Product>(target, parameter, p => p.Name, (TextSearch)whereFilter.Value);

                    //TextSearch textSearch = (TextSearch)whereFilter.Value;
                    //if (String.IsNullOrEmpty(textSearch.Text))
                    //    continue;

                    //MemberExpression nameProperty = Expression.Property(parameter, TypeHelper.PropertyName<Product, string>(p => p.Name));
                    //ConstantExpression value = Expression.Constant(textSearch.Text);
                    //BinaryExpression equal = Expression.Equal(nameProperty, value);

                    //if (target == null)
                    //    target = equal;
                }
            }

            return target;
        }
    }
}
