using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors.Processing.Reflection
{
    /// <summary>
    /// Collection of <see cref="IReflectionBehaviorFactory"/> by behavior type.
    /// </summary>
    public class ReflectionBehaviorFactoryCollection : IReflectionBehaviorFactory
    {
        private readonly object storageLock = new object();
        private readonly object searchFactoryLock = new object();
        private readonly Dictionary<Type, IReflectionBehaviorFactory> storage = new Dictionary<Type, IReflectionBehaviorFactory>();
        private readonly OutFuncCollection<Type, IReflectionBehaviorFactory, bool> onSearchFactory = new OutFuncCollection<Type, IReflectionBehaviorFactory, bool>(TryGetDefaultFactory);

        /// <summary>
        /// Maps <paramref name="behaviorType"/> to be processed by <paramref name="factory" />
        /// </summary>
        public ReflectionBehaviorFactoryCollection Add(Type behaviorType, IReflectionBehaviorFactory factory)
        {
            Ensure.NotNull(behaviorType, "behaviorType");
            Ensure.NotNull(factory, "provider");
         
            lock (storageLock)
                storage[behaviorType] = factory;

            return this;
        }

        /// <summary>
        /// Adds <paramref name="searchHandler"/> to be executed when generator was not found.
        /// (Last registered is executed the first).
        /// </summary>
        /// <param name="searchHandler">Generator provider method.</param>
        public ReflectionBehaviorFactoryCollection AddSearchHandler(OutFunc<Type, IReflectionBehaviorFactory, bool> searchHandler)
        {
            Ensure.NotNull(searchHandler, "searchHandler");

            lock (searchFactoryLock)
                onSearchFactory.Add(searchHandler);

            return this;
        }

        public object TryCreate(IReflectionBehaviorFactoryContext context, Type behaviorType)
        {
            IReflectionBehaviorFactory provider;
            if (!storage.TryGetValue(behaviorType, out provider))
                onSearchFactory.TryExecute(behaviorType, out provider);

            return provider.TryCreate(context, behaviorType);
        }

        private static bool TryGetDefaultFactory(Type behaviorType, out IReflectionBehaviorFactory factory)
        {
            factory = new DefaultReflectionBehaviorFactory();
            return true;
        }
    }
}
