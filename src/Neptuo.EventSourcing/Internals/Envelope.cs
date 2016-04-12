using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo
{
    internal class Envelope
    {
        public static Envelope Create(object body)
        {
            return new Envelope() { Body = body };
        }

        public object Body { get; set; }
    }

    internal class Envelope<T> : Envelope
    {

    }
}
