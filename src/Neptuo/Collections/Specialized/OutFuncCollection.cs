using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Collections.Specialized
{
    /// <summary>
    /// Represents collection of output functions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    /// <typeparam name="TReturn"></typeparam>
    public class OutFuncCollection<T, TOutput, TReturn> : IEnumerable<OutFunc<T, TOutput, TReturn>>
    {
        private readonly OutFunc<T, TOutput, TReturn> defaultFunc;
        private readonly List<OutFunc<T, TOutput, TReturn>> storage = new List<OutFunc<T, TOutput, TReturn>>();

        /// <summary>
        /// Creates a new empty instance.
        /// </summary>
        public OutFuncCollection()
        { }

        /// <summary>
        /// Create a new instance with first item <paramref name="defaultFunc"/>.
        /// </summary>
        /// <param name="defaultFunc">A first item in the collection.</param>
        public OutFuncCollection(OutFunc<T, TOutput, TReturn> defaultFunc)
        {
            Ensure.NotNull(defaultFunc, "defaultFunc");
            this.defaultFunc = defaultFunc;
        }

        public OutFuncCollection<T, TOutput, TReturn> Add(OutFunc<T, TOutput, TReturn> func)
        {
            Ensure.NotNull(func, "func");
            storage.Add(func);
            return this;
        }

        public OutFuncCollection<T, TOutput, TReturn> Remove(OutFunc<T, TOutput, TReturn> func)
        {
            Ensure.NotNull(func, "func");
            storage.Remove(func);
            return this;
        }

        #region IEnumerable

        public IEnumerator<OutFunc<T, TOutput, TReturn>> GetEnumerator()
        {
            if (defaultFunc == null)
                return storage.GetEnumerator();

            return Enumerable.Concat(
                storage,
                new OutFunc<T, TOutput, TReturn>[1] { defaultFunc }
            ).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
