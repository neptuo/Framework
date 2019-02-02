using Neptuo.Activators;
using Neptuo.Behaviors.Processing.Reflection;
using Neptuo.Behaviors.Providers;
using Neptuo.Queries.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Services.Queries
{
    static class TestQueries
    {
        public static void Test()
        {
            IBehaviorProvider behaviorProvider = new AttributeBehaviorCollection()
                .Add(typeof(LogAttribute), typeof(LogBehavior));

            ReflectionPipeline<ProductQueryHandler> pipeline = new ReflectionPipeline<ProductQueryHandler>(behaviorProvider, new DefaultReflectionBehaviorFactory());
            IQueryHandler<ProductQuery, Product> queryHandler = new BehaviorQueryHandler<ProductQueryHandler, ProductQuery, Product>(pipeline, new DefaultFactory<ProductQueryHandler>());

            Task<Product> task = queryHandler.HandleAsync(new ProductQuery() { Name = "Test" }, default);
            if (!task.IsCompleted)
                task.RunSynchronously();

            Console.WriteLine(task.Result.Price);
        }
    }
}
