using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators.Internals
{
    /// <summary>
    /// Storage for scoped instances/activators.
    /// </summary>
    internal class InstanceStorage
    {
        private readonly Dictionary<string, object> objectStorage = new Dictionary<string, object>();
        private readonly Dictionary<string, IActivator<object>> activatorStorage = new Dictionary<string,IActivator<object>>();

        public InstanceStorage AddObject(string key, object instance)
        {
            Ensure.NotNullOrEmpty(key, "key");
            Ensure.NotNull(instance, "instance");
            objectStorage[key] = instance;
            return this;
        }

        public InstanceStorage AddActivator(string key, IActivator<object> activator)
        {
            Ensure.NotNullOrEmpty(key, "key");
            Ensure.NotNull(activator, "activator");
            activatorStorage[key] = activator;
            return this;
        }

        public object TryGetObject(string key)
        {
            Ensure.NotNullOrEmpty(key, "key");
            return objectStorage[key];
        }

        public IActivator<object> TryGetActivator(string key)
        {
            Ensure.NotNullOrEmpty(key, "key");
            return activatorStorage[key];
        }
    }
}
