using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.DomainModels
{
    /// <summary>
    /// Base implementation of <see cref="IKey"/>.
    /// Solves equality, hash codes, comparing to other keys, atc.
    /// </summary>
    public abstract class KeyBase : IKey
    {
        /// <summary>
        /// Constant for hash code computing of the type.
        /// </summary>
        private const int hashPrimeNumber = 216613626;

        /// <summary>
        /// Constant for hash code computing of the hash code value provided by derivered class.
        /// </summary>
        private const int hashPrimeNumberField = 16777619;

        public string Type { get; private set; }

        public bool IsEmpty { get; private set; }

        /// <summary>
        /// Creates key instance with flag whether is empty or not..
        /// </summary>
        /// <param name="type">Identifier of the domain model type.</param>
        /// <param name="isEmpty">Whether this key is empty.</param>
        protected KeyBase(string type, bool isEmpty)
        {
            Guard.NotNullOrEmpty(type, "type");
            Type = type;
            IsEmpty = isEmpty;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as IKey);
        }

        public bool Equals(IKey other)
        {
            KeyBase key = other as KeyBase;
            if (key == null)
                return false;

            if (IsEmpty != key.IsEmpty)
                return false;

            if (Type != key.Type)
                return false;

            return Equals(key);
        }

        /// <summary>
        /// Should compare this key value to value of <paramref name="other"/> and returns its values are equal.
        /// </summary>
        /// <param name="other">The other key to compare its value.</param>
        /// <returns></returns>
        protected abstract bool Equals(KeyBase other);

        public int CompareTo(object obj)
        {
            KeyBase key = obj as KeyBase;
            if (key == null)
                return 1;

            int typeCompare = Type.CompareTo(key.Type);
            if (typeCompare == 0)
                return CompareValueTo(key);

            return typeCompare;
        }

        /// <summary>
        /// Should compare value of the <paramref name="other"/>.
        /// </summary>
        /// <param name="other">The other key to compare its value to.</param>
        /// <rereturns><see cref="IComparable.CompareTo"/>.</rereturns>
        protected abstract int CompareValueTo(KeyBase other);


        public override int GetHashCode()
        {
            //TODO: Do NOT export this method?
            //unchecked // Overflow is fine, just wrap ...
            //{
                int hash = hashPrimeNumber;

                hash = hash * hashPrimeNumberField ^ Type.GetHashCode();
                hash = !IsEmpty
                    ? hash * hashPrimeNumberField ^ GetValueHashCode()
                    : hash * hashPrimeNumberField ^ -1;

                return hash;
            //}
        }

        /// <summary>
        /// Should returns hash code for this key value.
        /// </summary>
        /// <returns>Hash code for this key value.</returns>
        protected abstract int GetValueHashCode();
    }
}
