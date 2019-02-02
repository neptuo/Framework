using Neptuo.Models;
using Neptuo.Models.Snapshots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Snapshots
{
    public class OrderSnapshotProvider : ISnapshotProvider
    {
        public bool TryCreate(IAggregateRoot aggregate, out ISnapshot snapshot)
        {
            Order order = aggregate as Order;
            if(order == null)
            {
                snapshot = null;
                return false;
            }

            snapshot = new OrderSnapshot()
            {
                AggregateKey = order.Key,
                Version = order.Version,
                Items = order.Items.ToList(),
                TotalPrice = order.TotalPrice
            };
            return true;
        }
    }
}
