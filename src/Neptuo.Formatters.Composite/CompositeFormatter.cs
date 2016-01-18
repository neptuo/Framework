using Neptuo.Activators;
using Neptuo.Formatters.Metadata;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters
{
    /// <summary>
    /// Base class for implementing <see cref="ISerializer"/> and <see cref="IDeserializer"/> as composite formatter.
    /// </summary>
    public class CompositeFormatter : ISerializer, IDeserializer
    {
        private readonly ICompositeTypeProvider provider;
        private readonly IFactory<ICompositeStorage> storageFactory;

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="provider">The provider for reading composite type definitions.</param>
        /// <param name="storageFactory">The factory for storage.</param>
        public CompositeFormatter(ICompositeTypeProvider provider, IFactory<ICompositeStorage> storageFactory)
        {
            Ensure.NotNull(provider, "provider");
            this.provider = provider;
        }

        public void AddValue(ICompositeStorage storage, string key, object value)
        {
            storage.Add(key, value.ToString());
        }

        public bool TryGetValue<T>(ICompositeStorage storage, string key, out T value)
        {
            object objectValue;
            if (!storage.TryGet(key, out objectValue))
            {
                value = default(T);
                return false;
            }

            value = (T)(object)objectValue;
            return true;
        }

        public bool TryGetValue(ICompositeStorage storage, string key, Type valueType, out object value)
        {
            object objectValue;
            if (!storage.TryGet(key, out objectValue))
            {
                value = null;
                return false;
            }

            value = (object)objectValue;
            return true;
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

        protected Task<bool> TrySerializeAsync(object input, ISerializerContext context, CompositeType type, CompositeVersion typeVersion)
        {
            ICompositeStorage storage = storageFactory.Create();
            AddValue(storage, "Name", type.Name);
            AddValue(storage, "Version", type.VersionProperty.Getter(input));

            ICompositeStorage valueStorage = storage.Add("Payload");
            foreach (CompositeProperty property in typeVersion.Properties)
            {
                object propertyValue = property.Getter(input);
                valueStorage.Add(property.Name, propertyValue.ToString());
            }

            storage.Store(context.Output);
            return Task.FromResult(true);
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

        protected Task<bool> TryDeserializeAsync(Stream input, IDeserializerContext context, CompositeType type)
        {
            ICompositeStorage storage = storageFactory.Create();
            storage.Load(input);

            int version;
            if (!TryGetValue(storage, "Version", out version))
                throw Ensure.Exception.NotImplemented();

            CompositeVersion typeVersion = GetCompositeVersion(type, version, context.OutputType);
            ICompositeStorage valueStorage;
            if(!storage.TryGet("Payload", out valueStorage))
                throw Ensure.Exception.NotImplemented();

            List<object> values = new List<object>();
            foreach (CompositeProperty property in typeVersion.Properties)
            {
                object value;
                if(TryGetValue(valueStorage, property.Name, property.Type, out value))
                    throw Ensure.Exception.NotImplemented();

                values.Add(value);
            }

            context.Output = typeVersion.Constructor.Factory(values.ToArray());
            return Task.FromResult(true);
        }
    }
}
