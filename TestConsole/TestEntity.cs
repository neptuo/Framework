using Neptuo;
using Neptuo.Data;
using Neptuo.Data.Commands;
using Neptuo.Data.Entity;
using Neptuo.Data.Entity.Queries;
using Neptuo.Data.Queries;
using Neptuo.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestConsole.Data;
using TestConsole.Data.Commands;

namespace TestConsole
{
    class TestEntity
    {
        public static void Test()
        {
            IDependencyContainer dependencyContainer = new UnityDependencyContainer();
            dependencyContainer
                .RegisterInstance<DataContext>(new DataContext())
                .RegisterType<IProductRepository, ProductRepository>()
                .RegisterType<ICategoryRepository, CategoryRepository>()
                .RegisterType<IProductQuery, ProductEntityQuery>();

            ICommandDispatcher commandDispatcher = new DependencyCommandDispatcher(dependencyContainer);
            IQueryDispatcher queryDispatcher = new DependencyQueryDispatcher(dependencyContainer);

            CreateProducts(dependencyContainer);
            
            //ICategoryRepository categories = dependencyContainer.Resolve<ICategoryRepository>();
            //IProductRepository products = dependencyContainer.Resolve<IProductRepository>();
            //Product product = products.Get(1);
            //product.Category = categories.Get(1);
            //products.Update(product);
            //dependencyContainer.Resolve<DataContext>().SaveChanges();


            //IProductQuery query = queryDispatcher.Get<IProductQuery>();
            //query.Filter.Key = null;
            //Console.WriteLine(String.Join(", ", query.Result().Items));
            //Console.WriteLine(query.Result(p => new { Name = p.Name, Price = p.Price }).Items);//HOW TO SELECT NOT MAPPED PROPERTY?
            //IEnumerable<string> productNames = query.Result(p => p.Name).Items;
            //Console.WriteLine(String.Join(", ", productNames));

            //var q = dependencyContainer.Resolve<IRepository<Product>>().Get().Where(p => p.ID == 3).Select(p => new { ID = p.ID, Name = p.Name });
            //Console.WriteLine(q.ToString());

            //IRepository<Product> repository = dependencyContainer.Resolve<IRepository<Product>>();
            //Product product = repository.Get(1);
            //Console.WriteLine(product.Key.ToLongString());

            //product.Price = 23;

            //DataContext dataContext = new DataContext();
            //Product product2 = dataContext.Products.FirstOrDefault(p => p.ID == 1);
            //product2.Price = 24;

            //repository.Update(product);
            //repository.Update(product2);
            //dependencyContainer.Resolve<DataContext>().SaveChanges();
            
            //dataContext.Set<Product>().Remove(product2);
            //dataContext.SaveChanges();

            Expression<Func<Product, object>> ex = p => new { Name = p.Name, Price = p.Price };


            Console.WriteLine("Done.");
        }

        static void CreateProducts(IDependencyContainer dependencyContainer)
        {
            ICategoryRepository categories = dependencyContainer.Resolve<ICategoryRepository>();

            Category uzenina = new CategoryEntity
            {
                Name = "Uzenina"
            };
            categories.Insert(uzenina);
            //dependencyContainer.Resolve<DataContext>().SaveChanges();

            IProductRepository products = dependencyContainer.Resolve<IProductRepository>();
            products.Insert(new ProductEntity
            {
                Name = "Buřty",
                Category = uzenina,
                Price = 22
            });
            products.Insert(new ProductEntity
            {
                Name = "Šunka",
                Category = uzenina,
                Price = 32
            });

            dependencyContainer.Resolve<DataContext>().SaveChanges();
        }
    }
}
