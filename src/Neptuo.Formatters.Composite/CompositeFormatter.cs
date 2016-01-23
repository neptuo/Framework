using Neptuo.Activators;
using Neptuo.Formatters.Metadata;
using Neptuo.Formatters.Storages;
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
        /// Gets the collection of value type formatters.
        /// </summary>
        public CompositeStorageFormatterCollection StorageFormatters { get; private set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="provider">The provider for reading composite type definitions.</param>
        /// <param name="storageFactory">The factory for storage.</param>
        public CompositeFormatter(ICompositeTypeProvider provider, IFactory<ICompositeStorage> storageFactory)
        {
            Ensure.NotNull(provider, "provider");
            Ensure.NotNull(storageFactory, "storageFactory");
            this.provider = provider;
            this.storageFactory = storageFactory;
            StorageFormatters = new CompositeStorageFormatterCollection();
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

        protected virtual Task<bool> TrySerializeAsync(object input, ISerializerContext context, CompositeType type, CompositeVersion typeVersion)
        {
            ICompositeStorage storage = storageFactory.Create();
            storage.Add("Name", type.Name);
            storage.Add("Version", type.VersionProperty.Getter(input));

            ICompositeStorage valueStorage = storage.Add("Payload");
            foreach (CompositeProperty property in typeVersion.Properties)
            {
                object propertyValue = property.Getter(input);
                if (StorageFormatters.TrySerialize(valueStorage, property.Name, propertyValue))
                    throw new NotSupportedValueException(property.Type);
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

        protected virtual Task<bool> TryDeserializeAsync(Stream input, IDeserializerContext context, CompositeType type)
        {
            ICompositeStorage storage = storageFactory.Create();
            storage.Load(input);

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
                if (!StorageFormatters.TryDeserialize(valueStorage, property.Name, property.Type, out value))
                    throw new NotSupportedValueException(property.Type);

                values.Add(value);
            }

            context.Output = typeVersion.Constructor.Factory(values.ToArray());
            return Task.FromResult(true);
        }
    }
}
