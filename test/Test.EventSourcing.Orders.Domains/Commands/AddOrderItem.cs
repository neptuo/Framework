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
    public class AddOrderItem : Command
    {
        public IKey OrderKey { get; private set; }
        public IKey ProductKey { get; private set; }
        public int Count { get; private set; }

        public AddOrderItem(IKey orderKey, IKey productKey, int count)
        {
            Ensure.Condition.NotEmptyKey(orderKey, "orderKey");
            Ensure.Condition.NotEmptyKey(productKey, "productKey");
            Ensure.Positive(count, "count");
            OrderKey = orderKey;
            ProductKey = productKey;
            Count = count;
        }
    }
}
