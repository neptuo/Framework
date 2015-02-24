using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.AppServices.Behaviors
{
    public class ReprocessAttribute : Attribute
    {
        public int Count { get; private set; }

        public ReprocessAttribute()
            : this(3)
        { }

        public ReprocessAttribute(int count)
        {
            Guard.PositiveOrZero(count, "count");
            Count = count;
        }
    }
}
