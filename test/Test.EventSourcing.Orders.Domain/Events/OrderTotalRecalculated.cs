using Neptuo.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Events
{
    public class OrderTotalRecalculated : Event
    {
        public decimal TotalPrice { get; private set; }

        public OrderTotalRecalculated(decimal totalPrice)
        {
            TotalPrice = totalPrice;
        }
    }
}
