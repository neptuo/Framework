using Neptuo.Bootstrap.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap
{
    public class AutomaticBootstrapper : BootstrapperBase, IBootstrapper
    {
        private IEnumerable<Type> types;

        public AutomaticBootstrapper(Func<Type, IBootstrapHandler> factory)
            : base(factory)
        { }

        public AutomaticBootstrapper(Func<Type, IBootstrapHandler> factory, IEnumerable<Type> types)
            : base(factory)
        {
            this.types = AddSupportedTypes(new List<Type>(), types);
        }

        public async override Task Initialize()
        {
            if (types == null)
                types = FindTypes();

            foreach (Type type in types)
            {
                IBootstrapHandler instance = CreateInstance(type);
                Tasks.Add(instance);
            }

            foreach (IBootstrapHandler task in Tasks)
                await task.HandleAsync();
        }

        protected virtual IEnumerable<Type> FindTypes()
        {
            List<Type> types = new List<Type>();
            SearchAssemblies(types);
            return types;
        }

        protected virtual IEnumerable<Type> SearchAssemblies(List<Type> types)
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    AddSupportedTypes(types, assembly.GetTypes());
                }
                catch (Exception) { }
            }
            return types;
        }

        protected virtual List<Type> AddSupportedTypes(List<Type> target, IEnumerable<Type> sourceTypes)
        {
            if (target == null)
                target = new List<Type>();

            Type bootstrapInterfaceType = typeof(IBootstrapHandler);
            foreach (Type type in sourceTypes)
            {
                if (bootstrapInterfaceType.IsAssignableFrom(type) && bootstrapInterfaceType != type && !type.IsAbstract && !type.IsInterface)
                    target.Add(type);
            }
            return target;
        }
    }
}
