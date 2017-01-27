using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo
{
    partial class TimerSchedulingProvider
    {
        /// <summary>
        /// An implementation of <see cref="ISchedulingContext"/> used internally by the <see cref="TimerSchedulingProvider"/>.
        /// It removes inner context from the collection after execution.
        /// </summary>
        private class SchedulingContext : ISchedulingContext
        {
            private readonly TimerSchedulingProvider collection;
            private readonly ISchedulingContext inner;

            public SchedulingContext(TimerSchedulingProvider collection, ISchedulingContext inner)
            {
                this.collection = collection;
                this.inner = inner;
            }

            public Envelope Envelope
            {
                get { return inner.Envelope; }
            }

            public void Execute()
            {
                collection.Remove(inner);
                inner.Execute();
            }

            public override int GetHashCode()
            {
                return inner.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                return inner.Equals(obj);
            }
        }
    }
}
