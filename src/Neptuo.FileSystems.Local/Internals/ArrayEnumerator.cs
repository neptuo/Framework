using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Internals
{
    internal class ArrayEnumerator<T> : EnumeratorBase<T>
    {
        private readonly string[] items;
        private readonly Func<string, T> itemGetter;
        private int index;
        private T current;

        public ArrayEnumerator(string[] items, Func<string, T> itemGetter)
        {
            this.items = items;
            this.itemGetter = itemGetter;
        }

        public override T Current
        {
            get
            {
                if (current == null)
                {
                    if (index < items.Length)
                        current = itemGetter(items[index]);
                    else
                        throw Ensure.Exception.NotSupported("Enumeration is out of bounds.");
                }

                return current;
            }
        }

        public override bool MoveNext()
        {
            index++;
            return index < items.Length;
        }

        public override void Reset()
        {
            index = 0;
        }
    }
}
