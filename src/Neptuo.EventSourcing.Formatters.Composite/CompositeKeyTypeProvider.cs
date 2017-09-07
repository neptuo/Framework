using Neptuo.Formatters.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Keys
{
    /// <summary>
    /// An implementation of <see cref="IKeyTypeProvider"/> using <see cref="ICompositeTypeProvider"/>.
    /// </summary>
    public class CompositeKeyTypeProvider : IKeyTypeProvider
    {
        private readonly ICompositeTypeProvider provider;

        /// <summary>
        /// Creates a new instance with <paramref name="provider"/>.
        /// </summary>
        /// <param name="provider">A provider of composite type definitions.</param>
        public CompositeKeyTypeProvider(ICompositeTypeProvider provider)
        {
            Ensure.NotNull(provider, "provider");
            this.provider = provider;
        }

        public bool TryGet(string keyType, out Type type)
        {
            Ensure.NotNullOrEmpty(keyType, "keyType");
            if (provider.TryGet(keyType, out CompositeType composite))
            {
                type = composite.Type;
                return true;
            }

            type = null;
            return false;
        }

        public bool TryGet(Type type, out string keyType)
        {
            Ensure.NotNull(type, "type");
            if (provider.TryGet(type, out CompositeType composite))
            {
                keyType = composite.Name;
                return true;
            }

            keyType = null;
            return false;
        }
    }
}
