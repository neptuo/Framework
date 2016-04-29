using Neptuo;
using Neptuo.Commands;
using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Domains.Commands
{
    public class CreateOrder : Command
    {
        public IKey OrderKey { get; private set; }

        public CreateOrder()
        {
            OrderKey = KeyFactory.Create(typeof(Order));
        }

        internal CreateOrder(IKey orderKey)
        {
            OrderKey = orderKey;
        }
    }
}
