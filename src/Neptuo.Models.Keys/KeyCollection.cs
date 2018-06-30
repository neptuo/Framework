using Neptuo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Keys
{
    /// <summary>
    /// An implementation of <see cref="ICollection{T}"/> with checks for key types and key classes.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class KeyCollection<TKey> : Collection<TKey>
        where TKey : IKey
    {
        private readonly IEnumerable<Type> supportedClasses;
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
        /// Creates a new empty collection where any added key's type must be contained in <paramref name="supportedTypes"/> and passed in keys must be of one of types in <paramref name="supportedClasses"/>.
        /// </summary>
        /// <param name="supportedTypes">An enumeration of supported <see cref="IKey.Type"/> (<c>null</c> means all).</param>
        /// <param name="supportedClasses">An enumeration of supported <see cref="IKey"/> implementation types (<c>null</c> means all).</param>
        public KeyCollection(IEnumerable<string> supportedTypes, IEnumerable<Type> supportedClasses)
        {
            this.supportedTypes = supportedTypes;
            this.supportedClasses = supportedClasses;
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
        /// Ensures that <paramref name="key"/> is of supported types and classes.
        /// </summary>
        /// <param name="key">A key to check.</param>
        protected virtual void EnsureKey(IKey key)
        {
            if (supportedTypes != null)
            {
                if (!supportedTypes.Contains(key.Type))
                    throw new RequiredKeyOfTypeException(key.Type, supportedTypes);
            }

            if (supportedClasses != null)
            {
                Type keyType = key.GetType();
                if (!supportedClasses.Any(c => c.IsAssignableFrom(keyType)))
                    throw new RequiredKeyOfClassException(keyType, supportedClasses);
            }
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
    }
}
