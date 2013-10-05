using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Queries
{
    public class DoubleSearch : IQuerySearch
    {
        public List<double> Value { get; set; }
        
        protected DoubleSearch(double value)
        {
            Value = new List<double>();
            Value.Add(value);
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
