using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators.Internals
{
    internal class InstanceStorage
    {
        private readonly Dictionary<string, object> objectStorage;
        private readonly Dictionary<string, IActivator<object>> activatorStorage;

        public InstanceStorage()
            : this(new Dictionary<string, object>(), new Dictionary<string,IActivator<object>>())
        { }

        public InstanceStorage(Dictionary<string, object> storage, Dictionary<string, IActivator<object>> activatorStorage)
        {
            Ensure.NotNull(storage, "storage");
            Ensure.NotNull(activatorStorage, "activatorStorage");
            this.objectStorage = storage;
            this.activatorStorage = activatorStorage;
        }

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

        public Dictionary<string, object> CopyObjects(IEnumerable<string> keysToSkip)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> item in objectStorage)
            {
                if (!keysToSkip.Contains(item.Key))
                    result.Add(item.Key, item.Value);
            }

            return result;
        }

        public Dictionary<string, IActivator<object>> CopyActivators(IEnumerable<string> keysToSkip)
        {
            Dictionary<string, IActivator<object>> result = new Dictionary<string, IActivator<object>>();
            foreach (KeyValuePair<string, IActivator<object>> item in activatorStorage)
            {
                if (!keysToSkip.Contains(item.Key))
                    result.Add(item.Key, item.Value);
            }

            return result;
        }
    }
}
