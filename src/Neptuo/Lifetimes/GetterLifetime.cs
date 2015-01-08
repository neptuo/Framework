using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Lifetimes
{
    public class GetterLifetime
    {
        public Func<object> Factory { get; private set; }

        public GetterLifetime(Func<object> factory)
        {
            Factory = factory;
        }
    }
}
