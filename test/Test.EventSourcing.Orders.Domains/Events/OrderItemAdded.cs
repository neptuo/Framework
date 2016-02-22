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
    public class OrderItemAdded : Event
    {
        public IKey ProductKey { get; private set; }
        public int Count { get; private set; }

        public OrderItemAdded(IKey productKey, int count)
        {
            Ensure.Condition.NotEmptyKey(productKey, "productKey");
            Ensure.Positive(count, "count");
            ProductKey = productKey;
            Count = count;
        }
    }
}
