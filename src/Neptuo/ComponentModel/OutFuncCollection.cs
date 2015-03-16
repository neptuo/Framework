using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel
{
    /// <summary>
    /// Represents collection of output functions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    /// <typeparam name="TReturn"></typeparam>
    public class OutFuncCollection<T, TOutput, TReturn> : IEnumerable<OutFunc<T, TOutput, TReturn>>
    {
        private List<OutFunc<T, TOutput, TReturn>> storage = new List<OutFunc<T, TOutput, TReturn>>();

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
            return storage.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
