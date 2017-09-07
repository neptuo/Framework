using Neptuo;
using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo
{
    /// <summary>
    /// A factory used for creating instances of <see cref="IKey"/>.
    /// By default it uses <see cref="KeyTypeProvider.Default"/> and <see cref="GuidKey"/>.
    /// You can 
    /// </summary>
    public static class KeyFactory
    {
        private static Func<Type, IKey> keyFactory = null;
        private static Func<Type, IKey> emptyFactory = null;
        private static IKeyTypeProvider keyTypeProvider;

        /// <summary>
        /// Sets <paramref name="keyFactory"/> to be used for generating new keys and <paramref name="emptyFactory"/> for generating empty keys.
        /// </summary>
        /// <param name="keyFactory">The key generator function.</param>
        /// <param name="emptyFactory">The empty key generator function.</param>
        public static void Set(Func<Type, IKey> keyFactory, Func<Type, IKey> emptyFactory)
        {
            Ensure.NotNull(keyFactory, "keyFactory");
            Ensure.NotNull(emptyFactory, "emptyFactory");
            KeyFactory.keyFactory = keyFactory;
            KeyFactory.emptyFactory = emptyFactory;
        }

        /// <summary>
        /// Sets key factory to use <see cref="GuidKey"/> and type fullname with assembly name (without version and public key).
        /// </summary>
        public static void SetGuidKeyWithTypeFullNameAndAssembly()
        {
            keyTypeProvider = new TypeFullNameWithAssemblyKeyTypeProvider();
            keyFactory = null;
            emptyFactory = null;
        }

        /// <summary>
        /// Sets <paramref name="keyTypeProvider"/> as provider for mapping from <see cref="Type"/> to <see cref="IKey.Type"/>.
        /// </summary>
        /// <param name="keyTypeProvider">A mapping provider.</param>
        public static void SetKeyTypeProvider(IKeyTypeProvider keyTypeProvider)
        {
            KeyFactory.keyTypeProvider = keyTypeProvider;
        }

        /// <summary>
        /// Creates a new instance of a key implementing <see cref="IKey"/> for the <paramref name="targetType"/>.
        /// </summary>
        /// <param name="targetType">A type for which key is generated.</param>
        /// <returns>A newly generated key for the <paramref name="targetType"/>.</returns>
        public static IKey Create(Type targetType)
        {
            Ensure.NotNull(targetType, "targetType");

            if (keyFactory != null)
                return keyFactory(targetType);

            string keyType = (keyTypeProvider ?? KeyTypeProvider.Default).Get(targetType);
            return GuidKey.Create(Guid.NewGuid(), keyType);
        }

        /// <summary>
        /// Creates a new empty key instance.
        /// </summary>
        /// <param name="targetType">A type for which key is generated.</param>
        /// <returns>A new empty key instance.</returns>
        public static IKey Empty(Type targetType)
        {
            Ensure.NotNull(targetType, "targetType");

            if (emptyFactory != null)
                return emptyFactory(targetType);

            string keyType = (keyTypeProvider ?? KeyTypeProvider.Default).Get(targetType);
            return GuidKey.Empty(keyType);
        }
    }
}
