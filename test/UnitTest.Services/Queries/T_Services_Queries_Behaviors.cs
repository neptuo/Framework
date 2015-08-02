using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Activators;
using Neptuo.Behaviors;
using Neptuo.Behaviors.Processing.Reflection;
using Neptuo.Behaviors.Providers;
using Neptuo.Services.Queries.Behaviors;
using Neptuo.Services.Queries.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Queries
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

    public class TestBehavior : IBehavior<object>
    {
        public static int count;

        public Task ExecuteAsync(object handler, IBehaviorContext context)
        {
            count++;
            return context.NextAsync();
        }
    }

    [TestClass]
    public class T_Services_Queries_Behaviors
    {
        [TestMethod]
        public void ReflectionPipeline()
        {
            IBehaviorProvider behaviorProvider = new GlobalBehaviorCollection()
                .Add(typeof(TestBehavior));

            ReflectionPipeline<ProductQueryHandler> pipeline = new ReflectionPipeline<ProductQueryHandler>(behaviorProvider, new DefaultReflectionBehaviorFactory());
            IQueryHandler<ProductQuery, Product> queryHandler = new QueryHandler<ProductQueryHandler, ProductQuery, Product>(pipeline, new DefaultActivator<ProductQueryHandler>());

            Task<Product> task = queryHandler.HandleAsync(new ProductQuery() { Name = "Test" });
            if (!task.IsCompleted)
                task.RunSynchronously();

            Assert.AreEqual(10.1M, task.Result.Price);
            Assert.AreEqual(1, TestBehavior.count);
        }
    }
}
