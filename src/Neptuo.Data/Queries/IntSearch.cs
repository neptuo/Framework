using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Queries
{
    public class IntSearch : IMultiValueQuerySearch<int>
    {
        public IReadOnlyList<int> Value { get; protected set; }

        protected IntSearch(int value)
        {
            Value = new List<int>(1) { value };
        }

        protected IntSearch(IEnumerable<int> value)
        {
            Value = new List<int>(value);
        }

        public static IntSearch Create(params int[] values)
        {
            return new IntSearch(values);
        }

        public static IntSearch Create(IEnumerable<int> values)
        {
            return new IntSearch(values);
        }
    }
}
