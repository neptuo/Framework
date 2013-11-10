using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    public interface IModelValueGetter
    {
        bool TryGetValue(string identifier, out object value);
    }

    public static class ModelValueGetterExtensions
    {
        public static object GetValueOrDefault(this IModelValueGetter getter, string identifier, object defaultValue)
        {
            object value;
            if (getter.TryGetValue(identifier, out value))
                return value;

            return defaultValue;
        }

        public static object GetValueOrDefault(this IModelValueGetter getter, string identifier, Func<object> defaultValueGetter)
        {
            object value;
            if (getter.TryGetValue(identifier, out value))
                return value;

            return defaultValueGetter();
        }

        public static T GetValueOrDefault<T>(this IModelValueGetter getter, string identifier, T defaultValue)
        {
            object value;
            if (getter.TryGetValue(identifier, out value))
                return (T)value;

            return defaultValue;
        }

        public static T GetValueOrDefault<T>(this IModelValueGetter getter, string identifier, Func<T> defaultValueGetter)
        {
            object value;
            if (getter.TryGetValue(identifier, out value))
                return (T)value;

            return defaultValueGetter();
        }
    }
}
