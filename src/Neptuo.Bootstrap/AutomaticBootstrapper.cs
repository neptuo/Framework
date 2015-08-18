using Neptuo.Bootstrap.Handlers;
using Neptuo.Reflection;
using Neptuo.Reflection.Enumerators.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap
{
    /// <summary>
    /// Simple automatic bootstrapper.
    /// Tasks are run synchronously and registered automatically from current app domain.
    /// </summary>
    public class AutomaticBootstrapper : IBootstrapper
    {
        public Task Initialize()
        {
            // Get tasks.
            List<Type> sourceTypes = new List<Type>();
            using (ITypeExecutorService typeExecutors = ReflectionFactory.FromCurrentAppDomain().PrepareTypeExecutors())
            {
                typeExecutors
                    .AddFiltered(false)
                    .AddFilterNotAbstract()
                    .AddFilterNotInterface()
                    .AddFilterHasDefaultConstructor()
                    .AddFilter(t => typeof(IBootstrapHandler).IsAssignableFrom(t))
                    .AddHandler(sourceTypes.Add);
            }

            // Create instances (if needed).
            foreach (Type targetType in sourceTypes)
            {
                IBootstrapHandler handler = (IBootstrapHandler)Activator.CreateInstance(targetType);
                Task task = handler.HandleAsync();
                if (!task.IsCompleted && !task.IsCanceled)
                    task.RunSynchronously();
            }

            return Task.FromResult(true);
        }
    }
}
