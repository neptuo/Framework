using System;
using System.Collections.Generic;

using System.Text;


namespace SharpKit.JavaScript.Private
{

    //TODO: struct

    [JsType(Name = "System.DateTime", Filename = "~/Internal/Core.js")]
    internal class JsImplDateTime
    {
        public JsDate ToJsDate()
        {
            return date;
        }
        JsDate date;

        //HACK - metaspec doesn't resolve the DateTime(year, month, day) ctor
        [JsMethod(Code = @"
if(arguments.length==3)
System.DateTime.ctor$$Int32$$Int32$$Int32.apply(this, arguments);
else if(arguments.length==6)
System.DateTime.ctor$$Int32$$Int32$$Int32$$Int32$$Int32$$Int32.apply(this, arguments);
else
this.date = System.DateTime.MinValue.date;
")]
        public JsImplDateTime()
        {
            this.date = MinValue.date;
        }

        public JsImplDateTime(int year, int month, int day)
        {
            this.date = new JsDate(year, month - 1, day, 0, 0, 0, 0);
        }

        public JsImplDateTime(int year, int month, int day, int hour, int minute, int second)
        {
            this.date = new JsDate(year, month - 1, day, hour, minute, second, 0);
        }

        JsImplDateTime(JsDate jsDate)
        {
            if (jsDate != null)
                this.date = jsDate;
            else
                this.date = MinValue.date;
        }

        JsImplDateTime(long jsDate)
        {
            this.date = new JsDate(jsDate.As<int>());
        }

        public static JsImplDateTime MinValue = null;

        [JsMethod(Name = "Parse$$String")]
        public static DateTime Parse(string str)
        {
            return new DateTime(JsDate.parse(str));
        }

        [JsMethod(Code = "return 32 - new Date(year, month-1, 32).getDate();")]
        public static int DaysInMonth(int year, int month)
        {
            return DateTime.DaysInMonth(year, month);
        }

        [JsMethod(Name = "CompareTo$$DateTime")]
        public int CompareTo(JsImplDateTime value)
        {
            return this.date.valueOf() - value.date.valueOf();
        }

        public static int Compare(JsImplDateTime t1, JsImplDateTime t2)
        {
            return t1.date.valueOf() - t2.date.valueOf();
        }

        //public JsImplDateTime Substract(JsImplDateTime dt)
        //{
        //  return new JsImplDateTime(new JsDate(this.date - dt;
        //}
        public int Year
        {
            get
            {
                return this.date.getFullYear();
            }
            set
            {
                this.date.setFullYear(value);
            }

        }
        public int Month
        {
            get
            {
                return this.date.getMonth() + 1;
            }
            set
            {
                this.date.setMonth(value - 1);
            }

        }
        public int Day
        {
            get
            {
                return this.date.getDate();
            }
            set
            {
                this.date.setDate(value);
            }

        }
        public int Hour
        {
            get
            {
                return this.date.getHours();
            }
            set
            {
                this.date.setHours(value);
            }
        }
        public int Minute
        {
            get
            {
                return this.date.getMinutes();
            }
            set
            {
                this.date.setMinutes(value);
            }
        }
        public int Second
        {
            get
            {
                return this.date.getSeconds();
            }
            set
            {
                this.date.setSeconds(value);
            }
        }
        public int Millisecond
        {
            get
            {
                return this.date.getMilliseconds();
            }
            set
            {
                this.date.setMilliseconds(value);
            }
        }

        //TODO: JS Enums problem (DayOfWeek)
        public int DayOfWeek
        {
            get
            {
                return this.date.getDay();
            }
        }
        public override string ToString()
        {
            return this.date.toString();
        }
        public JsImplDateTime AddDays(double days)
        {
            return new JsImplDateTime(this.date.addDays(days.As<int>()));
        }
        public JsImplDateTime AddMonths(int months)
        {
            return new JsImplDateTime(this.date.addMonths(months));
        }

        public JsImplDateTime AddHours(int hours)
        {
            return new JsImplDateTime(this.date.addHours(hours));
        }
        public JsImplDateTime AddMilliseconds(int milliseconds)
        {
            return new JsImplDateTime(this.date.addMilliseconds(milliseconds));
        }
        public JsImplDateTime AddMinutes(int minutes)
        {
            return new JsImplDateTime(this.date.addMinutes(minutes));
        }
        public JsImplDateTime AddSeconds(int seconds)
        {
            return new JsImplDateTime(this.date.addSeconds(seconds));
        }
        public JsImplDateTime AddYears(int years)
        {
            return new JsImplDateTime(this.date.addYears(years));
        }

        public static JsImplDateTime Now
        {
            get
            {
                return new JsImplDateTime(new JsDate());
            }
        }
        public static JsImplDateTime Today
        {
            get
            {
                JsDate now = new JsDate();
                return new JsImplDateTime(now.getFullYear(), now.getMonth() + 1, now.getDate());
            }
        }

        public TimeSpan Subtract(DateTime value)
        {
            var diff = date.valueOf() - value.As<JsImplDateTime>().date.valueOf();
            return new TimeSpan(diff * TimeSpan.TicksPerMillisecond);
        }
        // System.DateTime
        public JsImplDateTime Subtract(TimeSpan value)
        {
            var newDate = new JsDate(date.valueOf());
            newDate.setMilliseconds(date.getMilliseconds() + value.TotalMilliseconds);
            return new JsImplDateTime(newDate);
        }

        [JsMethod(Code = "return this.date.format(format);")]
        public string ToString(string format)
        {
            return null;
        }

        public DateTime get_Value()
        {
            return this.As<DateTime>();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is JsImplDateTime)) return false;
            return this.date.Equals(((JsImplDateTime)obj).date);
        }

        public override int GetHashCode()
        {
            return this.date.GetHashCode();
        }

        public static bool operator ==(JsImplDateTime t1, JsImplDateTime t2)
        {
            if (t1.As<object>() == t2.As<object>())
                return true;

            if (t1.As<object>() != null && t2.As<object>() != null)
                return DateTime.Compare(t1.As<DateTime>(), t2.As<DateTime>()) == 0;

            return false;
        }

        public static bool operator !=(JsImplDateTime t1, JsImplDateTime t2)
        {
            return !(t1 == t2);
        }

        public static TimeSpan operator -(JsImplDateTime t1, JsImplDateTime t2)
        {
            return TimeSpan.FromMilliseconds(t1.date.getTime() - t2.date.getTime());
        }
        public static JsImplDateTime operator -(JsImplDateTime t1, TimeSpan t2)
        {
            return new JsImplDateTime(new JsDate((long)t1.date.getDate() - (long)t2.TotalMilliseconds));
        }

        public static TimeSpan operator +(JsImplDateTime t1, JsImplDateTime t2)
        {
            return TimeSpan.FromMilliseconds(t1.date.getTime() + t2.date.getTime());
        }
        public static JsImplDateTime operator +(JsImplDateTime t1, TimeSpan t2)
        {
            return new JsImplDateTime(new JsDate((long)t1.date.getDate() + (long)t2.TotalMilliseconds));
        }
        public static bool operator <=(JsImplDateTime t1, JsImplDateTime t2)
        {
            if (t1 == null || t2 == null)
                return false;

            return DateTime.Compare(t1.As<DateTime>(), t2.As<DateTime>()) <= 0;
        }
        public static bool operator >=(JsImplDateTime t1, JsImplDateTime t2)
        {
            if (t1 == null || t2 == null)
                return false;

            return DateTime.Compare(t1.As<DateTime>(), t2.As<DateTime>()) >= 0;
        }
        public static bool operator <(JsImplDateTime t1, JsImplDateTime t2)
        {
            if (t1 == null || t2 == null)
                return false;

            return DateTime.Compare(t1.As<DateTime>(), t2.As<DateTime>()) < 0;
        }
        public static bool operator >(JsImplDateTime t1, JsImplDateTime t2)
        {
            if (t1 == null || t2 == null)
                return false;

            return DateTime.Compare(t1.As<DateTime>(), t2.As<DateTime>()) > 0;
        }
    }

    [JsType(Filename = "~/Internal/Core.js")]
    static class Extensions
    {

        public static JsDate addMilliseconds(this JsDate date, int miliseconds)
        {
            var date2 = new JsDate(date.valueOf());
            date2.setMilliseconds(date2.getMilliseconds() + miliseconds);
            return date2;
        }

        public static JsDate addSeconds(this JsDate date, int seconds)
        {
            var date2 = new JsDate(date.valueOf());
            date2.setSeconds(date2.getSeconds() + seconds);
            return date2;
        }

        public static JsDate addMinutes(this JsDate date, int minutes)
        {
            var date2 = new JsDate(date.valueOf());
            date2.setMinutes(date2.getMinutes() + minutes);
            return date2;
        }

        public static JsDate addHours(this JsDate date, int hours)
        {
            var date2 = new JsDate(date.valueOf());
            date2.setHours(date2.getHours() + hours);
            return date2;
        }

        public static JsDate addDays(this JsDate date, JsNumber days)
        {
            var date2 = new JsDate(date.valueOf());
            date2.setDate(date2.getDate() + days);
            return date2;
        }

        public static JsDate addMonths(this JsDate date, JsNumber months)
        {
            var date2 = new JsDate(date.valueOf());
            date2.setMonth(date2.getMonth() + months);
            return date2;
        }

        public static JsDate addYears(this JsDate date, JsNumber years)
        {
            var date2 = new JsDate(date.valueOf());
            date2.setMonth(date2.getFullYear() + years);
            return date2;
        }

        public static JsDate removeTime(this JsDate date)
        {
            var date2 = new JsDate(date.getFullYear(), date.getMonth(), date.getDate());
            return date2;
        }

    }
}
