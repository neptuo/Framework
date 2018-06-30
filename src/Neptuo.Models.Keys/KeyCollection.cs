using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Keys
{
    /// <summary>
    /// An implementation of <see cref="ICollection{T}"/> with checks for key types.
    /// All empty keys are ignored.
    /// </summary>
    /// <typeparam name="TKey">A type of the key.</typeparam>
    public class KeyCollection<TKey> : Collection<TKey>
        where TKey : IKey
    {
        private readonly IEnumerable<string> supportedTypes;

        /// <summary>
        /// Creates a new empty collection.
        /// </summary>
        public KeyCollection()
        { }

        /// <summary>
        /// Creates a new empty collection where any added key's type must be contained in <paramref name="supportedTypes"/>.
        /// </summary>
        /// <param name="supportedTypes">An enumeration of supported <see cref="IKey.Type"/>.</param>
        public KeyCollection(IEnumerable<string> supportedTypes)
        {
            this.supportedTypes = supportedTypes;
        }

        /// <summary>
        /// Creates a new collection as a wrapper for <paramref name="list"/>.
        /// </summary>
        /// <param name="list">An inner list of keys.</param>
        public KeyCollection(IList<TKey> list)
            : base(list)
        {
        }

        /// <summary>
        /// Creates a new collection as a wrapper for <paramref name="list"/> where any added key's type must be contained in <paramref name="supportedTypes"/>.
        /// </summary>
        /// <param name="list">An inner list of keys.</param>
        /// <param name="supportedTypes">An enumeration of supported <see cref="IKey.Type"/>.</param>
        public KeyCollection(IList<TKey> list, IEnumerable<string> supportedTypes)
            : base(list)
        {
            this.supportedTypes = supportedTypes;
        }

        /// <summary>
        /// Ensures that <paramref name="key"/> is of supported types and classes.
        /// </summary>
        /// <param name="key">A key to check.</param>
        protected virtual void EnsureKey(IKey key)
        {
            if (!IsSupportedType(key.Type))
                throw new RequiredKeyOfTypeException(key.Type, supportedTypes);
        }

        protected override void InsertItem(int index, TKey key)
        {
            if (key.IsEmpty)
                return;

            EnsureKey(key);
            base.InsertItem(index, key);
        }

        protected override void SetItem(int index, TKey key)
        {
            if (key.IsEmpty)
                return;

            EnsureKey(key);
            base.SetItem(index, key);
        }

        /// <summary>
        /// Returns <c>true</c> if supported key types contains <paramref name="keyType"/>.
        /// </summary>
        /// <param name="keyType">An key type.</param>
        /// <returns><c>true</c> if supported key types contains <paramref name="keyType"/>; <c>false</c> otherwise.</returns>
        public bool IsSupportedType(string keyType)
        {
            if (supportedTypes == null)
                return true;

            return supportedTypes.Contains(keyType);
        }
    }

    /// <summary>
    /// An implementation of <see cref="ICollection{T}"/> with checks for key types and key classes.
    /// All empty keys are ignored.
    /// </summary>
    public class KeyCollection : KeyCollection<IKey>
    {
        private readonly IEnumerable<Type> supportedClasses;

        /// <summary>
        /// Creates a new empty collection where any added key's type must be contained in <paramref name="supportedTypes"/> and passed in keys must be of one of types in <paramref name="supportedClasses"/>.
        /// </summary>
        /// <param name="supportedTypes">An enumeration of supported <see cref="IKey.Type"/> (<c>null</c> means all).</param>
        /// <param name="supportedClasses">An enumeration of supported <see cref="IKey"/> implementation types (<c>null</c> means all).</param>
        public KeyCollection(IEnumerable<string> supportedTypes, IEnumerable<Type> supportedClasses)
            : base(supportedTypes)
        {
            this.supportedClasses = supportedClasses;
        }

        protected override void EnsureKey(IKey key)
        {
            base.EnsureKey(key);

            if (supportedClasses != null)
            {
                Type keyType = key.GetType();
                if (!IsSupportedClass(keyType))
                    throw new RequiredKeyOfClassException(keyType, supportedClasses);
            }
        }

        /// <summary>
        /// Returns <c>true</c> if supported key classes contains <paramref name="keyType"/>.
        /// </summary>
        /// <param name="keyType">An key type.</param>
        /// <returns><c>true</c> if supported key classes contains <paramref name="keyType"/>; <c>false</c> otherwise.</returns>
        public bool IsSupportedClass(Type keyType)
        {
            if (supportedClasses == null)
                return true;

            return supportedClasses.Any(c => c.IsAssignableFrom(keyType));
        }
    }
}
