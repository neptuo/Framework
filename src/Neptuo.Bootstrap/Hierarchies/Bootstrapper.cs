using Neptuo.Activators;
using Neptuo.Bootstrap.Hierarchies.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Hierarchies
{
    public class Bootstrapper : IBootstrapper, IBootstrapTaskCollection
    {
        private readonly Dictionary<Type, IFactory<IBootstrapTask>> storage = new Dictionary<Type, IFactory<IBootstrapTask>>();

        public IBootstrapTaskCollection Add<T>(IFactory<T> factory)
            where T : class, IBootstrapTask
        {
            Ensure.NotNull(factory, "factory");
            storage[typeof(T)] = factory;
            return this;
        }

        public bool TryGet<T>(out IFactory<T> factory)
            where T : class, IBootstrapTask
        {
            IFactory<IBootstrapTask> innerFactory;
            if (storage.TryGetValue(typeof(T), out innerFactory))
            {
                factory = (IFactory<T>)innerFactory;
                return true;
            }

            factory = null;
            return true;
        }

        public void Initialize()
        {
            // Sort tasks.
            IEnumerable<Type> sourceTypes = storage.Keys;
            Sorter sorter = new Sorter(null, null);
            IEnumerable<Type> targetTypes = sorter.Sort(sourceTypes, new List<Type>());

            // Create instances (if needed).
            foreach (Type targetType in targetTypes)
            {
                IBootstrapTask task = storage[targetType].Create();
                task.Initialize();
            }
        }
    }
}
