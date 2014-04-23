using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Queries
{
    public class DecimalSearch : IMultiValueQuerySearch<decimal>
    {
        public IReadOnlyList<decimal> Value { get; protected set; }
        
        protected DecimalSearch(decimal value)
        {
            Value = new List<decimal>(1) { value };
        }

        protected DecimalSearch(IEnumerable<decimal> value)
        {
            Value = new List<decimal>(value);
        }

        public static DecimalSearch Create(decimal value)
        {
            return new DecimalSearch(value);
        }

        public static DecimalSearch Create(IEnumerable<decimal> value)
        {
            return new DecimalSearch(value);
        }
    }
}
