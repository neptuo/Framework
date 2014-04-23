using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Queries
{
    public class DoubleSearch : IMultiValueQuerySearch<double>
    {
        public IReadOnlyList<double> Value { get; protected set; }
        
        protected DoubleSearch(double value)
        {
            Value = new List<double>(1) { value };
        }

        protected DoubleSearch(IEnumerable<double> value)
        {
            Value = new List<double>(value);
        }

        public static DoubleSearch Create(double value)
        {
            return new DoubleSearch(value);
        }

        public static DoubleSearch Create(IEnumerable<double> value)
        {
            return new DoubleSearch(value);
        }
    }
}
