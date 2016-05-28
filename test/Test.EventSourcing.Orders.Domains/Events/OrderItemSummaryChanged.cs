using Neptuo;
using Neptuo.Events;
using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Domains.Events
{
    public class OrderItemSummaryChanged : Event
    {
        public IEnumerable<IKey> ProductKeys { get; private set; }

        public OrderItemSummaryChanged(IEnumerable<IKey> productKeys)
        {
            Ensure.NotNull(productKeys, "productKeys");
            ProductKeys = productKeys.ToList();
        }
    }
}
