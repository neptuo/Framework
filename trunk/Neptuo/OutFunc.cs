using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neptuo
{ 
    public delegate TReturn OutFunc<T, TOutput, TReturn>(T input, out TOutput output);
}
