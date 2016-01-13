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
    public abstract class CompositeFormatter : ISerializer, IDeserializer
    {
        private readonly ICompositeTypeProvider provider;

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="provider">The provider for reading composite type definitions.</param>
        public CompositeFormatter(ICompositeTypeProvider provider)
        {
            Ensure.NotNull(provider, "provider");
            this.provider = provider;
        }

        public Task<bool> TrySerializeAsync(object input, ISerializerContext context)
        {
            CompositeType type;
            if (!provider.TryGet(context.InputType, out type))
                return Task.FromResult(false);

            int version = (int)type.VersionProperty.Getter(input);
            CompositeVersion typeVersion = type.Versions.FirstOrDefault(v => v.Version == version);
            if (typeVersion == null)
                throw new MissingVersionException(context.InputType, version);

            return TrySerializeAsync(input, context, typeVersion);
        }

        protected abstract Task<bool> TrySerializeAsync(object input, ISerializerContext context, CompositeVersion typeVersion);

        public Task<bool> TryDeserializeAsync(Stream input, IDeserializerContext context)
        {
            CompositeType type;
            if (!provider.TryGet(context.OutputType, out type))
                return Task.FromResult(false);

            return TryDeserializeAsync(input, context, type);
        }

        protected abstract Task<bool> TryDeserializeAsync(Stream input, IDeserializerContext context, CompositeType type);
    }
}
