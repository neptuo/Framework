using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Queries
{
    public class IntQuerySearch : IQuerySearch
    {
        public List<int> Value { get; set; }

        protected IntQuerySearch(int value)
        {
            Value = new List<int>();
            Value.Add(value);
        }

        protected IntQuerySearch(IEnumerable<int> value)
        {
            Value = new List<int>(value);
        }

        public static IntQuerySearch Create(int value)
        {
            return new IntQuerySearch(value);
        }

        public static IntQuerySearch Create(IEnumerable<int> value)
        {
            return new IntQuerySearch(value);
        }
    }
}
