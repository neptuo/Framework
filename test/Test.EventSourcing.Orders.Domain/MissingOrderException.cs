using Neptuo;
using Neptuo.Models;
using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders
{
    public class MissingOrderException : AggregateRootException
    {
        public IKey OrderKey { get; private set; }

        public MissingOrderException(IKey orderKey)
        {
            Ensure.Condition.NotEmptyKey(orderKey);
            OrderKey = orderKey;
        }
    }
}
