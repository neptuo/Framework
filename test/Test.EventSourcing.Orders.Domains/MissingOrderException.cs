using Neptuo;
using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Domains
{
    public class MissingOrderException : Exception
    {
        public IKey OrderKey { get; private set; }

        public MissingOrderException(IKey orderKey)
        {
            Ensure.Condition.NotEmptyKey(orderKey, "orderKey");
            OrderKey = orderKey;
        }
    }
}
