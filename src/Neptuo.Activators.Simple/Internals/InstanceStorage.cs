using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators.Internals
{
    /// <summary>
    /// Storage for scoped instances/factories.
    /// </summary>
    internal class InstanceStorage
    {
        private readonly Dictionary<string, object> objectStorage = new Dictionary<string, object>();
        private readonly Dictionary<string, IFactory<object>> factoryStorage = new Dictionary<string, IFactory<object>>();

        public InstanceStorage AddObject(string key, object instance)
        {
            Ensure.NotNullOrEmpty(key, "key");
            Ensure.NotNull(instance, "instance");
            objectStorage[key] = instance;
            return this;
        }

        public InstanceStorage AddFactory(string key, IFactory<object> factory)
        {
            Ensure.NotNullOrEmpty(key, "key");
            Ensure.NotNull(factory, "factory");
            factoryStorage[key] = factory;
            return this;
        }

        public object TryGetObject(string key)
        {
            Ensure.NotNullOrEmpty(key, "key");

            object result;
            if (objectStorage.TryGetValue(key, out result))
                return result;

            return null;
        }

        public IFactory<object> TryGetFactory(string key)
        {
            Ensure.NotNullOrEmpty(key, "key");

            IFactory<object> result;
            if (factoryStorage.TryGetValue(key, out result))
                return result;

            return null;
        }
    }
}
