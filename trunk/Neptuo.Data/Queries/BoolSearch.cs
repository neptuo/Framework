using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Queries
{
    public class BoolSearch : IQuerySearch
    {
        public bool? Value { get; protected set; }
        public bool IsNotNull { get; protected set; }

        protected BoolSearch(bool? value, bool isNotNull)
        {
            Value = value;
            IsNotNull = isNotNull;
        }

        public static BoolSearch Create(bool value)
        {
            return new BoolSearch(value, false);
        }

        public static BoolSearch True()
        {
            return Create(true);
        }

        public static BoolSearch False()
        {
            return Create(false);
        }

        public static BoolSearch Null()
        {
            return new BoolSearch(null, false);
        }

        public static BoolSearch NotNull()
        {
            return new BoolSearch(null, true);
        }
    }
}
