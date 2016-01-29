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
    /// The implementation of <see cref="IFormatter"/> with support for versioned types described as <see cref="CompositeType"/>.
    /// </summary>
    public class CompositeTypeFormatter : IFormatter, ISerializer, IDeserializer
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

        /// <summary>
        /// Tries to store the <paramref name="input"/> to the <paramref name="context"/>. The <paramref name="input"/> is described by the <paramref name="type"/>
        /// and in the <paramref name="version"/>.
        /// </summary>
        /// <param name="input">The object to store.</param>
        /// <param name="context">The serialization context.</param>
        /// <param name="type">The <paramref name="input"/> composite type descriptor.</param>
        /// <param name="typeVersion">The version of the <paramref name="input"/>.</param>
        /// <returns><c>true</c> if the <paramref name="input"/> was serialized to the <paramref name="context"/>; <c>false</c> otherwise.</returns>
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

        /// <summary>
        /// Tries to store hte <paramref name="value"/> to the <paramref name="storage"/> with the <paramref name="key"/>.
        /// </summary>
        /// <param name="storage">The composite storage to store the <paramref name="value"/> to.</param>
        /// <param name="key">The key to associate the <paramref name="value"/> to.</param>
        /// <param name="value">The value to store.</param>
        /// <returns><c>true</c> if the <paramref name="value"/> was stored to the <paramref name="storage"/>; <c>false</c> otherwise.</returns>
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

        /// <summary>
        /// Tries to load a object from the <paramref name="input"/> described by the <paramref name="type"/>.
        /// </summary>
        /// <param name="input">The serialized value to deserialize.</param>
        /// <param name="context">The deserialization context.</param>
        /// <param name="type">The composite type descriptor to load.</param>
        /// <returns><c>true</c> if object was deserialized; <c>false</c> otherwise.</returns>
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

            if (type.VersionProperty.Setter != null)
                type.VersionProperty.Setter(context.Output, version);

            return true;
        }

        private static readonly string tryGetMethodName = "TryGet";
        private readonly Dictionary<Type, MethodInfo> tryGetCache = new Dictionary<Type, MethodInfo>();

        /// <summary>
        /// Tries to load value from <paramref name="storage"/> associated with <paramref name="key"/> of <paramref name="type"/>.
        /// In default implementation uses underlaying <paramref name="storage"/> capability to load value of <paramref name="type"/>.
        /// </summary>
        /// <param name="storage">The composite storage to laod value from.</param>
        /// <param name="key">The key to value of.</param>
        /// <param name="type">The type of value to load.</param>
        /// <param name="value">The loaded value or <c>null</c>.</param>
        /// <returns><c>true</c> if <paramref name="value"/> was provided; <c>false</c> otherwise.</returns>
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
