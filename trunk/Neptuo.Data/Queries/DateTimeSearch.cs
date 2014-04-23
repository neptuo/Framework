using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Queries
{
    public class DateTimeSearch : IQuerySearch
    {
        public DateTime? DateTime { get; protected set; }
        public DateTimeSearchType Type { get; protected set; }
        public DateTimeSearchCompare ComparePart { get; protected set; }

        protected DateTimeSearch(DateTime? dateTime, DateTimeSearchType type, DateTimeSearchCompare comparePart)
        {
            DateTime = dateTime;
            Type = type;
            ComparePart = comparePart;
        }

        public static DateTimeSearch Create(DateTime dateTime, DateTimeSearchType type = DateTimeSearchType.Exactly, DateTimeSearchCompare comparePart = DateTimeSearchCompare.Date)
        {
            return new DateTimeSearch(dateTime, type, comparePart);
        }

        public static DateTimeSearch After(DateTime dateTime, DateTimeSearchCompare comparePart = DateTimeSearchCompare.Date)
        {
            return Create(dateTime, DateTimeSearchType.After, comparePart);
        }

        public static DateTimeSearch Before(DateTime dateTime, DateTimeSearchCompare comparePart = DateTimeSearchCompare.Date)
        {
            return Create(dateTime, DateTimeSearchType.Before, comparePart);
        }

        public static DateTimeSearch AfterOrExactly(DateTime dateTime, DateTimeSearchCompare comparePart = DateTimeSearchCompare.Date)
        {
            return Create(dateTime, DateTimeSearchType.After | DateTimeSearchType.Exactly, comparePart);
        }

        public static DateTimeSearch BeforeOrExactly(DateTime dateTime, DateTimeSearchCompare comparePart = DateTimeSearchCompare.Date)
        {
            return Create(dateTime, DateTimeSearchType.Before | DateTimeSearchType.Exactly, comparePart);
        }

        public static DateTimeSearch BeforeOrAfter(DateTime dateTime, DateTimeSearchCompare comparePart = DateTimeSearchCompare.Date)
        {
            return Create(dateTime, DateTimeSearchType.Before | DateTimeSearchType.After, comparePart);
        }

        public static DateTimeSearch Null()
        {
            return new DateTimeSearch(null, DateTimeSearchType.Exactly, DateTimeSearchCompare.DateTime);
        }

        public static DateTimeSearch NotNull()
        {
            return new DateTimeSearch(null, DateTimeSearchType.Before | DateTimeSearchType.After, DateTimeSearchCompare.DateTime);
        }
    }
}
