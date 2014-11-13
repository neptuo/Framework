using Neptuo.Bootstrap.Constraints;
using Neptuo.Bootstrap.Dependencies;
using Neptuo.Bootstrap.Dependencies.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap
{
    /// <summary>
    /// TODO: Implement Hierarchical bootstrapper.
    /// </summary>
    /// <remarks>
    /// Řezení bootstrap tasků by mělo být takto:
    /// 1) Ti, co X exportují (vytvářejí instanci).
    /// 2) Ti, co X importují i exportují (donaplňují instanci) => co atribut [Building]?
    /// 3) Ti, co jen importují (má se předávat do kontextu/env/container).
    /// </remarks>
    public class HierarchicalBootstrapper : BootstrapperBase, IBootstrapper, IBootstrapTaskRegistry
    {
        private readonly HierarchicalContext context;
        private readonly List<BootstrapTaskDescriptor> descriptors = new List<BootstrapTaskDescriptor>();

        public HierarchicalBootstrapper(HierarchicalContext context)
            : base(context.Activator, context.ConstraintProvider)
        {
            Guard.NotNull(context, "context");
            this.context = context;
        }

        public void Register(IBootstrapTask task)
        {
            Guard.NotNull(task, "task");
            BootstrapTaskDescriptor descriptor = new BootstrapTaskDescriptor(task.GetType());
            descriptor.Imports.AddRange(context.DependencyProvider.GetImports(descriptor.Type));
            descriptor.Exports.AddRange(context.DependencyProvider.GetExports(descriptor.Type));
            descriptor.Instance = task;
            descriptors.Add(descriptor);
        }

        public void Register<T>() 
            where T : IBootstrapTask
        {
            BootstrapTaskDescriptor descriptor = new BootstrapTaskDescriptor(typeof(T));
            descriptor.Imports.AddRange(context.DependencyProvider.GetImports(descriptor.Type));
            descriptor.Exports.AddRange(context.DependencyProvider.GetExports(descriptor.Type));
            descriptor.Instance = CreateInstance<T>();
            descriptors.Add(descriptor);
        }

        public override void Initialize()
        {
            foreach (BootstrapTaskDescriptor descriptor in descriptors)
                ExecuteDescriptor(descriptor);
        }

        private void ExecuteDescriptor(BootstrapTaskDescriptor descriptor)
        {
            if (descriptor.IsExecuted)
                return;

            //TODO: Check cycles! Use stack and check if descriptor is not present on the stack.

            // Import.
            ProcessImports(descriptor);

            // Check constraints.
            if (AreConstraintsSatisfied(descriptor.Instance))
            {
                // Run task.
                descriptor.Instance.Initialize();

                // Export.
                ProcessExports(descriptor, descriptor.Instance);
            }

            // Mark as exported.
            descriptor.IsExecuted = true;
        }

        private void ProcessImports(BootstrapTaskDescriptor descriptor)
        {
            foreach (ITaskImportDescriptor import in descriptor.Imports)
            {
                Tuple<ITaskExportDescriptor, BootstrapTaskDescriptor> export = FindExportDescriptor(import.Target);
                if(export == null)
                    throw Guard.Exception.InvalidOperation("Missing import for type '{0}', implement factory or environment import.", import.Target.TargetType.FullName);

                if (!export.Item2.IsExecuted)
                    ExecuteDescriptor(export.Item2);

                import.SetValue(descriptor.Instance, export.Item1.GetValue(export.Item2.Instance));
            }
        }

        private void ProcessExports(BootstrapTaskDescriptor descriptor, IBootstrapTask task)
        {
            foreach (ITaskExportDescriptor export in descriptor.Exports)
                context.DependencyExporter.Export(export, export.GetValue(task));
        }

        private Tuple<ITaskExportDescriptor, BootstrapTaskDescriptor> FindExportDescriptor(ITaskDependencyTarget target)
        {
            foreach (BootstrapTaskDescriptor descriptor in descriptors)
            {
                foreach (ITaskExportDescriptor export in descriptor.Exports)
                {
                    if (export.Target.Equals(target))
                        return new Tuple<ITaskExportDescriptor,BootstrapTaskDescriptor>(export, descriptor);
                }
            }

            return null;
        }


        //void ITaskExecutor.Execute(IBootstrapTask task)
        //{
        //    Guard.NotNull(task, "task");
        //    BootstrapTaskDescriptor descriptor = descriptors.FirstOrDefault(d => d.Instance == task);
        //    if (descriptor == null)
        //    {
        //        Type taskType = task.GetType();
        //        descriptor = descriptors.FirstOrDefault(d => d.Type == taskType);

        //        if (descriptor == null)
        //            descriptors.Add(descriptor = new BootstrapTaskDescriptor(taskType) { Instance = task });
        //    }

        //    ExecuteDescriptor(descriptor);
        //}





        private class BootstrapTaskDescriptor
        {
            public Type Type { get; private set; }
            public IBootstrapTask Instance { get; set; }

            public List<ITaskImportDescriptor> Imports { get; private set; }
            public List<ITaskExportDescriptor> Exports { get; private set; }

            public bool IsExecuted { get; set; }

            public BootstrapTaskDescriptor(Type type)
            {
                Guard.NotNull(type, "type");
                Type = type;
                Imports = new List<ITaskImportDescriptor>();
                Exports = new List<ITaskExportDescriptor>();
            }
        }
    }
}
