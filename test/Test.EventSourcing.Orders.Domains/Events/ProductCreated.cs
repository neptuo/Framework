using Neptuo;
using Neptuo.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Domains.Events
{
    public class ProductCreated : Event
    {
        public string Name { get; private set; }

        internal ProductCreated(string name)
        {
            Name = name;
        }
    }
}
