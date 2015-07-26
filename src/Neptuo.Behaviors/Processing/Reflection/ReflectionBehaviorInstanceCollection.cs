using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel.Behaviors.Processing.Reflection
{
    /// <summary>
    /// Registry for <see cref="IReflectionBehaviorInstanceProvider"/> by behavior type.
    /// </summary>
    public class ReflectionBehaviorInstanceCollection : IReflectionBehaviorInstanceProvider
    {
        private readonly object storageLock = new object();
        private readonly object searchHandlerLock = new object();
        private readonly Dictionary<Type, IReflectionBehaviorInstanceProvider> storage = new Dictionary<Type, IReflectionBehaviorInstanceProvider>();
        private readonly FuncList<Type, IReflectionBehaviorInstanceProvider> onSearchBuilder = new FuncList<Type, IReflectionBehaviorInstanceProvider>(o => new DefaultReflectionBehaviorInstanceProvider());

        /// <summary>
        /// Maps <paramref name="behaviorType"/> to be processed by <paramref name="provider" />
        /// </summary>
        public ReflectionBehaviorInstanceCollection AddProvider(Type behaviorType, IReflectionBehaviorInstanceProvider provider)
        {
            Ensure.NotNull(behaviorType, "behaviorType");
            Ensure.NotNull(provider, "provider");
         
            lock (storageLock)
                storage[behaviorType] = provider;

            return this;
        }

        /// <summary>
        /// Adds <paramref name="searchHandler"/> to be executed when generator was not found.
        /// (Last registered is executed the first).
        /// </summary>
        /// <param name="searchHandler">Generator provider method.</param>
        public ReflectionBehaviorInstanceCollection AddSearchHandler(Func<Type, IReflectionBehaviorInstanceProvider> searchHandler)
        {
            Ensure.NotNull(searchHandler, "searchHandler");

            lock (searchHandlerLock)
                onSearchBuilder.Add(searchHandler);

            return this;
        }

        public object TryProvide(IReflectionContext context, Type behaviorType)
        {
            IReflectionBehaviorInstanceProvider provider;
            if (!storage.TryGetValue(behaviorType, out provider))
                provider = onSearchBuilder.Execute(behaviorType);

            return provider.TryProvide(context, behaviorType);
        }
    }
}
