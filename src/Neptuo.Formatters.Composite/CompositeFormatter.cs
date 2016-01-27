using Neptuo.Activators;
using Neptuo.Formatters.Converters;
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
                bool isSuccess;
                if (!Converts.Try(new CompositeSerializerContext(valueStorage, property.Name, propertyValue), out isSuccess))
                    throw new NotSupportedValueException(property.Type);
            }

            await storage.StoreAsync(context.Output);
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
                if (!Converts.Try(new CompositeDeserializerContext(valueStorage, property.Name, property.Type), out value))
                    throw new NotSupportedValueException(property.Type);

                values.Add(value);
            }

            context.Output = typeVersion.Constructor.Factory(values.ToArray());
            return true;
        }
    }
}
