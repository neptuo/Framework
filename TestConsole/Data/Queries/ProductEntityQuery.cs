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
    public class ProductEntityQuery : EntityQuery<Product, IProductFilter>, IProductQuery
    {
        public ProductEntityQuery(IProductRepository repository)
            : base(repository.Get())
        { }

        protected override IQueryable<Product> AppendWhere(IQueryable<Product> items, Dictionary<string, IQuerySearch> whereFilters)
        {


            foreach (var whereFilter in whereFilters)
            {
                if (whereFilter.Key == TypeHelper.PropertyName<IProductFilter, object>(p => p.Key))
                {
                    IntQuerySearch intSearch = (IntQuerySearch)whereFilter.Value;
                    if (intSearch.Value.Count == 0)
                        continue;

                    //if (intSearch.Value.Count == 1)
                    //    items = items.Where(i => i.Key == intSearch.Value[0]);
                }
                else if (whereFilter.Key == TypeHelper.PropertyName<IProductFilter, object>(p => p.Name))
                {
                    TextQuerySearch textSearch = (TextQuerySearch)whereFilter.Value;
                    if (String.IsNullOrEmpty(textSearch.Text))
                        continue;

                    //items = items.Where(p => p.Name == textSearch.Text);


                    
                    ParameterExpression productParameter = Expression.Parameter(typeof(Product));
                    MemberExpression nameProperty = Expression.Property(productParameter, TypeHelper.PropertyName<Product, string>(p => p.Name));
                    ConstantExpression value = Expression.Constant(textSearch.Text);
                    BinaryExpression equal = Expression.Equal(nameProperty, value);



                    MethodCallExpression whereCallExpression = Expression.Call(
                       typeof(Queryable),
                       TypeHelper.MethodName<IQueryable<Product>, Expression<Func<Product, bool>>, IQueryable<Product>>(q => q.Where),
                       new Type[] { typeof(Product) },
                       items.Expression,
                       Expression.Lambda<Func<Product, bool>>(equal, new ParameterExpression[] { productParameter })
                    );

                    items = items.Provider.CreateQuery<Product>(whereCallExpression);


                    continue;
                }
            }

            return items;
        }
    }
}
