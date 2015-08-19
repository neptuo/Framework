using Neptuo.Activators;
using Neptuo.Behaviors.Processing.Reflection;
using Neptuo.Behaviors.Providers;
using Neptuo.Bootstrap;
using Neptuo.Bootstrap.Handlers;
using Neptuo.Bootstrap.Processing.Metadata;
using Neptuo.Bootstrap.Processing.Metadata.Sorting;
using Neptuo.Bootstrap.Sequences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.BootstrapTasks
{
    public class BootstrapperBuilder
    {
        public HierarchicalBuilder ToHierarchical(IValueInputProvider inputProvider, IValueOutputProvider outputProvider)
        {
            return new HierarchicalBuilder();
        }

        public SimpleBuilder ToSimple()
        {
            return new SimpleBuilder();
        }
    }

    public class SimpleBuilder
    {
        public SimpleManualBuilder ToManual()
        {
            return new SimpleManualBuilder();
        }

        public SimpleAutomaticBuilder ToAutomatic()
        {
            return new SimpleAutomaticBuilder();
        }
    }

    public class SimpleManualBuilder : IBootstrapHandlerCollection
    {
        public IBootstrapHandlerCollection Add<T>(IFactory<T> factory) where T : class, IBootstrapHandler
        {
            throw new NotImplementedException();
        }

        public bool TryGet<T>(out IFactory<T> factory) where T : class, IBootstrapHandler
        {
            throw new NotImplementedException();
        }

        public IBootstrapper ToBootstrapper()
        {
            return new ManualBootstrapper();
        }
    }

    public class SimpleAutomaticBuilder
    {
        public IBootstrapper ToBootstrapper()
        {
            return new AutomaticBootstrapper();
        }
    }

    // Ví, jak zpracovat tasky
    public class HierarchicalBuilder 
    {
        public HierarchicalBuilder AddSortInputProvider(ISortInputProvider inputProvider)
        {
            return this;
        }

        public HierarchicalBuilder AddSortOutputProvider(ISortOutputProvider outputProvider)
        {
            return this;
        }

        public HierarchicalBuilder AddBehaviorProvider(IBehaviorProvider behaviorProvider)
        {
            return this;
        }

        public HierarchicalBuilder AddReflectionBehaviorFactory(IReflectionBehaviorFactory reflectionBehaviorFactory)
        {
            return this;
        }

        public HierarchicalBuilder AddDefaultDependency(Type type)
        {
            return this;
        }

        public HierarchicalBuilder AddDefaultDependency<T>()
        {
            return AddDefaultDependency(typeof(T));
        }


        public HierarchicalManualBuilder ToManual()
        {
            return new HierarchicalManualBuilder();
        }

        public HierarchicalAutomaticBuilder ToAutomatic()
        {
            return new HierarchicalAutomaticBuilder();
        }
    }

    // Ví jak získat tasky.
    public class HierarchicalManualBuilder : IBootstrapHandlerCollection
    {
        public IBootstrapHandlerCollection Add<T>(IFactory<T> factory) 
            where T : class, IBootstrapHandler
        {
            throw new NotImplementedException();
        }

        public bool TryGet<T>(out IFactory<T> factory) 
            where T : class, IBootstrapHandler
        {
            throw new NotImplementedException();
        }

        public IBootstrapper ToBootstrapper()
        {
            return new Neptuo.Bootstrap.Sequences.ManualBootstrapper();
        }
    }

    public class HierarchicalAutomaticBuilder
    {
        public HierarchicalAutomaticBuilder AddFilter(Func<Type, bool> filter)
        {
            return this;
        }

        public IBootstrapper ToBootstrapper()
        {
            return new AutomaticBootstrapper();
        }
    }

    class TestBuilder
    {
        public static void Test()
        {
            new BootstrapperBuilder()
                .ToHierarchical(new DependencyValueProvider(null), new DependencyValueProvider(null))
                .AddBehaviorProvider(new GlobalBehaviorCollection())
                .AddDefaultDependency<IDependencyContainer>()
                .AddDefaultDependency<IDependencyProvider>()
                .AddReflectionBehaviorFactory(new DefaultReflectionBehaviorFactory())
                .ToAutomatic()
                .AddFilter(t => t.Namespace.StartsWith("Neptuo."))
                .ToBootstrapper()
                .Initialize();

            SimpleManualBuilder manualBuilder = new BootstrapperBuilder()
                .ToSimple()
                .ToManual();

            manualBuilder
                .Add(new TestConsole.BootstrapTasks.Sequence.HelloBootstrapTask())
                .Add(new TestConsole.BootstrapTasks.Sequence.WriterBootstrapTask());

            manualBuilder
                .ToBootstrapper()
                .Initialize();
        }
    }
}
