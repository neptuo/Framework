using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Lifetimes
{
    public class SingletonLifetime
    {
        public object Instance { get; private set; }

        public SingletonLifetime(object instance)
        {
            Instance = instance;
        }
    }
}
