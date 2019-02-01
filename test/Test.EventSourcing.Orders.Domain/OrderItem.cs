using Neptuo;
using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders
{
    public class OrderItem
    {
        public IKey ProductKey { get; private set; }
        public int Count { get; private set; }

        public OrderItem(IKey productKey, int count)
        {
            Ensure.Condition.NotEmptyKey(productKey);
            Ensure.Positive(count, "count");
            ProductKey = productKey;
            Count = count;
        }

        public void Extend(int count)
        {
            Count += count;
        }
    }
}
