//using Neptuo;
//using Neptuo.Data;
//using Neptuo.Pipelines.Commands;
//using Neptuo.Pipelines.Commands.Handlers;
//using Neptuo.Data.Entity;
//using Neptuo.Data.Entity.Queries;
//using Neptuo.Data.Queries;
//using Neptuo.Linq.Expressions;
//using Neptuo.Activators;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Runtime.CompilerServices;
//using System.Text;
//using System.Threading.Tasks;
//using TestConsole.Data;
//using TestConsole.Data.Commands;
//using TestConsole.Data.Commands.Handlers;
//using TestConsole.Data.Commands.Validators.Handlers;
//using TestConsole.Data.Queries;
//using Neptuo.Pipelines.Validators;
//using Neptuo.Pipelines.Events;

//namespace TestConsole.Data
//{
//    class TestEntity
//    {
//        public static void Test()
//        {
//            IDependencyContainer dependencyContainer = new UnityDependencyContainer();
//            dependencyContainer
//                .RegisterInstance<DataContext>(new DataContext())
//                .RegisterType<IUnitOfWorkFactory, DbContextUnitOfWorkFactory<DataContext>>()
//                .RegisterType<IProductRepository, ProductRepository>()
//                .RegisterType<ICategoryRepository, CategoryRepository>()
//                .RegisterType<ICommandHandler<CreateProductCommand>, CreateProductCommandHandler>()
//                .RegisterType<IValidationHandler<CreateProductCommand>, CreateProductValidator>()
//                .RegisterType<IProductQuery, ProductEntityQuery>();

//            //.RegisterType<ICommandValidator<CreateProductCommand, IValidationResult>, CreateProductValidator>();

//            ICommandDispatcher commandDispatcher = new DependencyCommandDispatcher(dependencyContainer, new DefaultEventManager());

//            //CreateProducts(dependencyContainer);

//            ICategoryRepository categories = dependencyContainer.Resolve<ICategoryRepository>();
//            Category uzenina = categories.Create();
//            uzenina.Name = "Software";
//            categories.Insert(uzenina);

//            //ICategoryRepository categories = dependencyContainer.Resolve<ICategoryRepository>();
//            //CreateProductCommand createProduct = new CreateProductCommand();
//            //createProduct.Name = "Uzený salám";
//            //createProduct.Price = 12;
//            //createProduct.Category = categories.Get(1);

//            //if (commandDispatcher.Validate<CreateProductCommand, IValidationResult>(createProduct).IsValid)
//            //{
//            //    commandDispatcher.Handle(createProduct);
//            //    dependencyContainer.Resolve<DataContext>().SaveChanges();
//            //}
//            //commandDispatcher.Handle<CreateProductCommand>(new CreateProductCommand());


//            //ICategoryRepository categories = dependencyContainer.Resolve<ICategoryRepository>();
//            //IProductRepository products = dependencyContainer.Resolve<IProductRepository>();
//            //Product product = products.Get(1);
//            //product.Category = categories.Get(1);
//            //products.Update(product);
//            //dependencyContainer.Resolve<DataContext>().SaveChanges();


//            //ProductEntity e = new ProductEntity();


//            IProductQuery query = dependencyContainer.Resolve<IProductQuery>();
//            //query.Filter.Name = TextSearch.Create("u", TextSearchType.Contains);
//            //query.Filter.CategoryKey = IntSearch.Create(1);
//            //query.Filter.AvailableFrom = DateTimeSearch.CreateBeforeOrExactly(DateTime.Now);
//            //query.Filter.StopSale = DateTimeSearch.NotNull();//Create(DateTime.Now, DateTimeSearchType.Before);
//            query.Filter.Category = new CategoryFilter();
//            query.Filter.Category.Name = TextSearch.Match("Uzenina");
//            query.Filter.IsDiscount = BoolSearch.True();


//            var result = query.Result(p => p.Key);
//            Console.WriteLine(String.Format("{0} => {1}", result.TotalCount, String.Join(", ", result.EnumerateItems())));



//            //PerfTest(dependencyContainer, queryDispatcher);

//            //query.Filter.Key = null;
//            //Console.WriteLine(String.Join(", ", query.Result().Items));
//            //Console.WriteLine(query.Result(p => new { Name = p.Name, Price = p.Price }).Items);//HOW TO SELECT NOT MAPPED PROPERTY?
//            //IEnumerable<string> productNames = query.Result(p => p.Name).Items;
//            //Console.WriteLine(String.Join(", ", productNames));

//            //var q = dependencyContainer.Resolve<IRepository<Product>>().Get().Where(p => p.ID == 3).Select(p => new { ID = p.ID, Name = p.Name });
//            //Console.WriteLine(q.ToString());

//            //IRepository<Product> repository = dependencyContainer.Resolve<IRepository<Product>>();
//            //Product product = repository.Get(1);
//            //Console.WriteLine(product.Key.ToLongString());

//            //product.Price = 23;

//            //DataContext dataContext = new DataContext();
//            //Product product2 = dataContext.Products.FirstOrDefault(p => p.ID == 1);
//            //product2.Price = 24;

//            //repository.Update(product);
//            //repository.Update(product2);
//            //dependencyContainer.Resolve<DataContext>().SaveChanges();
            
//            //dataContext.Set<Product>().Remove(product2);
//            //dataContext.SaveChanges();

//            //Expression<Func<Product, object>> ex = p => new { Name = p.Name, Price = p.Price };

//        }

//        static void PerfTest(IDependencyContainer dependencyContainer)
//        {
//            Stopwatch sw = new Stopwatch();
//            sw.Start();

//            for (int i = 0; i < 1000; i++)
//            {
//                dependencyContainer.Resolve<IProductQuery>()
//                    .Result().EnumerateItems()
//                    .Select(p => p.Key)
//                    .ToArray();
//            }

//            sw.Stop();
//            Console.WriteLine("Ellapsed: {0}ms", sw.ElapsedMilliseconds);
//            sw.Reset();

//            DataContext dataContext = dependencyContainer.Resolve<DataContext>();
//            sw.Start();

//            for (int i = 0; i < 1000; i++)
//            {
//                List<int> ids = new List<int>();
//                dataContext.Products
//                    .Where(p => ids.Contains(p.Key))
//                    .Select(p => new { ID = p.Key, Version = p.Version })
//                    .ToArray();
//            }

//            sw.Stop();
//            Console.WriteLine("Ellapsed: {0}ms", sw.ElapsedMilliseconds);
//        }

//        static void CreateProducts(IDependencyContainer dependencyContainer)
//        {
//            using (var transaction = dependencyContainer.Resolve<IUnitOfWorkFactory>().Create())
//            {
//                ICategoryRepository categories = dependencyContainer.Resolve<ICategoryRepository>();
//                Category uzenina = categories.Create();
//                uzenina.Name = "Uzenina";
//                categories.Insert(uzenina);

//                IProductRepository products = dependencyContainer.Resolve<IProductRepository>();
//                Product burty = products.Create();
//                burty.Name = "Buřty";
//                burty.Category = uzenina;
//                burty.Price = 22;
//                burty.AvailableFrom = DateTime.Now.AddDays(1);
//                burty.StopSale = DateTime.Now.AddDays(3);
//                burty.IsDiscount = true;
//                products.Insert(burty);

//                Product sunka = products.Create();
//                sunka.Name = "Šunka";
//                sunka.Category = uzenina;
//                sunka.Price = 32;
//                sunka.AvailableFrom = DateTime.Now.AddDays(-1);
//                sunka.StopSale = DateTime.Now.AddDays(3);
//                products.Insert(sunka);

//                Product hoveziKyta = products.Create();
//                hoveziKyta.Name = "Hovězí kýta";
//                hoveziKyta.Category = uzenina;
//                hoveziKyta.Price = 44;
//                hoveziKyta.AvailableFrom = DateTime.Now;
//                hoveziKyta.IsDiscount = true;
//                products.Insert(hoveziKyta);

//                transaction.SaveChanges();
//            }
//        }
//    }
//}
