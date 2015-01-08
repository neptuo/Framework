using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo
{
    [Obsolete]
    public class StringConvertBase
    {
        public static TType Convert<TType>(string value, OutFunc<string, TType, bool> func, TType defaultValue)
        {
            TType converted = defaultValue;
            if (func(value, out converted))
                return converted;

            return defaultValue;
        }

        public static TType? ConvertNull<TType>(string value, OutFunc<string, TType, bool> func)
            where TType : struct
        {
            TType converted = default(TType);
            if (func(value, out converted))
                return converted;

            return null;
        }
    }

    [Obsolete]
    public class StringConvert<T> : StringConvertBase
    {
        private IStringValueProvider<T> provider;

        public StringConvert(IStringValueProvider<T> provider)
        {
            this.provider = provider;
        }

        public string Value(T model, string name, string defaultValue = null)
        {
            return provider.GetValue(model, name) ?? defaultValue;
        }

        public bool Bool(T model, string name, bool defaultValue = false)
        {
            return BoolNull(model, name) ?? defaultValue;
        }

        public bool? BoolNull(T model, string name)
        {
            return ConvertNull<bool>(provider.GetValue(model, name), Boolean.TryParse);
        }

        public int Int(T model, string name, int defaultValue)
        {
            return IntNull(model, name) ?? defaultValue;
        }

        public int? IntNull(T model, string name)
        {
            return ConvertNull<int>(provider.GetValue(model, name), Int32.TryParse);
        }

        public long Long(T model, string name, long defaultValue)
        {
            return LongNull(model, name) ?? defaultValue;
        }

        public long? LongNull(T model, string name)
        {
            return ConvertNull<long>(provider.GetValue(model, name), Int64.TryParse);
        }

        public decimal Decimal(T model, string name, decimal defaultValue)
        {
            return DecimalNull(model, name) ?? defaultValue;
        }

        public decimal? DecimalNull(T model, string name)
        {
            return ConvertNull<decimal>(provider.GetValue(model, name), System.Decimal.TryParse);
        }

        public double Double(T model, string name, double defaultValue)
        {
            return DoubleNull(model, name) ?? defaultValue;
        }

        public double? DoubleNull(T model, string name)
        {
            return ConvertNull<double>(provider.GetValue(model, name), System.Double.TryParse);
        }

        public DateTime DateTime(T model, string key, DateTime? defaultValue = null)
        {
            return DateTimeNull(model, key) ?? defaultValue ?? System.DateTime.Now;
        }

        public DateTime? DateTimeNull(T model, string key)
        {
            string value = provider.GetValue(model, key);
            if (value == null)
                return null;

            DateTime dateTime;
            if (System.DateTime.TryParse(value, out dateTime))
                return dateTime;

            long ticks;
            if (Int64.TryParse(value, out ticks))
                return new DateTime(ticks);

            return null;
        }

        public CultureInfo Culture(T model, string key, CultureInfo defaultValue)
        {
            return CultureNull(model, key) ?? defaultValue;
        }

        public CultureInfo CultureNull(T model, string key)
        {
            string value = provider.GetValue(model, key);
            if (value == null)
                return null;

            return CultureInfo.GetCultureInfo(value);
        }

        public TEnum Enum<TEnum>(T model, string name, TEnum defaultValue)
            where TEnum : struct
        {
            return EnumNull<TEnum>(model, name) ?? defaultValue;
        }

        public TEnum? EnumNull<TEnum>(T model, string name)
            where TEnum : struct
        {
            string attr = provider.GetValue(model, name);
            if (attr != null)
            {
                TEnum val;
                if (System.Enum.TryParse<TEnum>(attr, out val))
                    return val;
            }

            return null;
        }
    }

    [Obsolete]
    public interface IStringConvert
    {
        bool Bool(string name, bool defaultValue = false);
        bool? BoolNull(string name);
        CultureInfo Culture(string key, CultureInfo defaultValue);
        CultureInfo CultureNull(string key);
        DateTime DateTime(string key, DateTime? defaultValue = null);
        DateTime? DateTimeNull(string key);
        decimal Decimal(string name, decimal defaultValue);
        decimal? DecimalNull(string name);
        double Double(string name, double defaultValue);
        double? DoubleNull(string name);
        TEnum Enum<TEnum>(string name, TEnum defaultValue) where TEnum : struct;
        TEnum? EnumNull<TEnum>(string name) where TEnum : struct;
        int Int(string name, int defaultValue);
        int? IntNull(string name);
        long Long(string name, long defaultValue);
        long? LongNull(string name);
        string Value(string name, string defaultValue = null);
    }

    [Obsolete]
    public class StringStringConvert<T> : IStringConvert
    {
        private StringConvert<T> convert;
        private T model;

        public StringStringConvert(IStringValueProvider<T> provider, T model)
        {
            this.convert = new StringConvert<T>(provider);
            this.model = model;
        }

        public bool Bool(string name, bool defaultValue = false)
        {
            return convert.Bool(model, name, defaultValue);
        }

        public bool? BoolNull(string name)
        {
            return convert.BoolNull(model, name);
        }

        public CultureInfo Culture(string key, CultureInfo defaultValue)
        {
            return convert.Culture(model, key, defaultValue);
        }

        public CultureInfo CultureNull(string key)
        {
            return convert.CultureNull(model, key);
        }

        public DateTime DateTime(string key, DateTime? defaultValue = null)
        {
            return convert.DateTime(model, key, defaultValue);
        }

        public DateTime? DateTimeNull(string key)
        {
            return convert.DateTimeNull(model, key);
        }

        public decimal Decimal(string name, decimal defaultValue)
        {
            return convert.Decimal(model, name, defaultValue);
        }

        public decimal? DecimalNull(string name)
        {
            return convert.DecimalNull(model, name);
        }

        public double Double(string name, double defaultValue)
        {
            return convert.Double(model, name, defaultValue);
        }

        public double? DoubleNull(string name)
        {
            return convert.DoubleNull(model, name);
        }

        public TEnum Enum<TEnum>(string name, TEnum defaultValue) 
            where TEnum : struct
        {
            return convert.Enum<TEnum>(model, name, defaultValue);
        }

        public TEnum? EnumNull<TEnum>(string name) where TEnum : struct
        {
            return convert.EnumNull<TEnum>(model, name);
        }

        public int Int(string key, int defaultValue)
        {
            return convert.Int(model, key, defaultValue);
        }

        public int? IntNull(string name)
        {
            return convert.IntNull(model, name);
        }

        public long Long(string name, long defaultValue)
        {
            return convert.Long(model, name, defaultValue);
        }

        public long? LongNull(string name)
        {
            return convert.LongNull(model, name);
        }

        public string Value(string name, string defaultValue = null)
        {
            return convert.Value(model, name, defaultValue);
        }
    }

    [Obsolete]
    public interface IStringValueProvider<T>
    {
        string GetValue(T model, string key);
    }
}
