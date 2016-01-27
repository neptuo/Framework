using Neptuo.Activators;
using Neptuo.Formatters.Metadata;
using Neptuo.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters
{
    /// <summary>
    /// The implementation of <see cref="ISerializer"/> and <see cref="IDeserializer"/> with support for versioned types described as <see cref="CompositeType"/>.
    /// </summary>
    public class CompositeTypeFormatter : ISerializer, IDeserializer
    {
        private readonly ICompositeTypeProvider provider;
        private readonly IFactory<ICompositeStorage> storageFactory;

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="provider">The provider for reading composite type definitions.</param>
        /// <param name="storageFactory">The factory for storage.</param>
        public CompositeTypeFormatter(ICompositeTypeProvider provider, IFactory<ICompositeStorage> storageFactory)
        {
            Ensure.NotNull(provider, "provider");
            Ensure.NotNull(storageFactory, "storageFactory");
            this.provider = provider;
            this.storageFactory = storageFactory;
        }

        public Task<bool> TrySerializeAsync(object input, ISerializerContext context)
        {
            CompositeType type;
            if (!provider.TryGet(context.InputType, out type))
                return Task.FromResult(false);

            int version = (int)type.VersionProperty.Getter(input);
            CompositeVersion typeVersion = GetCompositeVersion(type, version, context.InputType);
            return TrySerializeAsync(input, context, type, typeVersion);
        }

        protected virtual async Task<bool> TrySerializeAsync(object input, ISerializerContext context, CompositeType type, CompositeVersion typeVersion)
        {
            ICompositeStorage storage = storageFactory.Create();
            storage.Add("Name", type.Name);
            storage.Add("Version", type.VersionProperty.Getter(input));

            ICompositeStorage valueStorage = storage.Add("Payload");
            foreach (CompositeProperty property in typeVersion.Properties)
            {
                object propertyValue = property.Getter(input);
                if (!TryStoreValue(valueStorage, property.Name, propertyValue))
                    throw new NotSupportedValueException(property.Type);
            }

            await storage.StoreAsync(context.Output);
            return true;
        }

        protected virtual bool TryStoreValue(ICompositeStorage storage, string key, object value)
        {
            storage.Add(key, value);
            return true;
        }

        private CompositeVersion GetCompositeVersion(CompositeType type, int version, Type inputType)
        {
            CompositeVersion typeVersion = type.Versions.FirstOrDefault(v => v.Version == version);
            if (typeVersion == null)
                throw new MissingVersionException(inputType, version);

            return typeVersion;
        }

        public Task<bool> TryDeserializeAsync(Stream input, IDeserializerContext context)
        {
            CompositeType type;
            if (!provider.TryGet(context.OutputType, out type))
                return Task.FromResult(false);

            return TryDeserializeAsync(input, context, type);
        }

        protected virtual async Task<bool> TryDeserializeAsync(Stream input, IDeserializerContext context, CompositeType type)
        {
            ICompositeStorage storage = storageFactory.Create();
            await storage.LoadAsync(input);

            int version;
            if (!storage.TryGet("Version", out version))
                throw new MissingVersionValueException();

            CompositeVersion typeVersion = GetCompositeVersion(type, version, context.OutputType);
            ICompositeStorage valueStorage;
            if(!storage.TryGet("Payload", out valueStorage))
                throw new MissingPayloadValueException();

            List<object> values = new List<object>();
            foreach (CompositeProperty property in typeVersion.Properties)
            {
                object value;
                if (!TryLoadValue(valueStorage, property.Name, property.Type, out value))
                    throw new NotSupportedValueException(property.Type);

                values.Add(value);
            }

            context.Output = typeVersion.Constructor.Factory(values.ToArray());
            return true;
        }

        private static readonly string tryGetMethodName = "TryGet";
        private readonly Dictionary<Type, MethodInfo> tryGetCache = new Dictionary<Type, MethodInfo>();

        protected virtual bool TryLoadValue(ICompositeStorage storage, string key, Type type, out object value)
        {
            MethodInfo methodInfo;
            if (!tryGetCache.TryGetValue(type, out methodInfo))
            {
                methodInfo = storage.GetType().GetMethods().FirstOrDefault(m => m.Name == tryGetMethodName && m.IsGenericMethod);
                if (methodInfo == null)
                {
                    value = null;
                    return false;
                }

                methodInfo = methodInfo.MakeGenericMethod(type);
                tryGetCache[type] = methodInfo;
            }

            object[] parameters = new object[2] { key, null };
            bool result = (bool)methodInfo.Invoke(storage, parameters);

            value = parameters[1];
            return result;
        }
    }
}
