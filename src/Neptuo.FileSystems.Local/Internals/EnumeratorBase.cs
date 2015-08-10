using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Internals
{
    internal abstract class EnumeratorBase<T> : IEnumerator<T>
    {
        public abstract T Current { get; }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public virtual void Dispose()
        { }

        public abstract bool MoveNext();

        public abstract void Reset();
    }
}
