using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;

namespace Neptuo.Reflection
{
    public static class ReflectionHelper
    {
        public static T GetAttribute<T>(MemberInfo member) where T : Attribute
        {
            T[] attrs = (T[])member.GetCustomAttributes(typeof(T), true);
            if (attrs.Length == 1)
                return attrs[0];
            else
                return null;
        }

        public static List<Type> GetTypesInNamespace(string newNamespace)
        {
            Assembly targetAssembly = Assembly.GetExecutingAssembly();

            string[] parts = newNamespace.Split(',');
            if (parts.Length == 2)
                targetAssembly = Assembly.Load(parts[1]);

            return targetAssembly.GetTypes().Where(t => t.Namespace == parts[0]).ToList();
        }

        public static List<PropertyInfo> GetAnnotatedProperties<T>(Type type) where T : Attribute
        {
            List<PropertyInfo> result = new List<PropertyInfo>();
            foreach (PropertyInfo prop in type.GetProperties())
            {
                if (prop.GetCustomAttributes(typeof(T), true).Length == 1)
                    result.Add(prop);
            }

            return result;
        }

        public static bool IsGenericType<TAssignable, TArgument>(Type testedType)
        {
            return IsGenericType(testedType, typeof(TAssignable), typeof(TArgument));
        }

        public static bool IsGenericType<TAssignable>(Type testedType)
        {
            return IsGenericType(testedType, typeof(TAssignable));
        }

        public static bool IsGenericType(Type testedType, Type assignableType, Type argumentType)
        {
            if (argumentType == null)
                return false;

            return assignableType.IsAssignableFrom(testedType)
                && testedType.IsGenericType
                && (testedType.GetGenericArguments()[0].IsAssignableFrom(argumentType) || argumentType.IsAssignableFrom(testedType.GetGenericArguments()[0]));
        }

        public static bool IsGenericType(Type testedType, Type assignableType)
        {
            return assignableType.IsAssignableFrom(testedType)
                && testedType.IsGenericType;
        }

        public static Type GetGenericArgument(Type type, int index = 0)
        {
            if (!type.IsGenericType)
                return null;

            return type.GetGenericArguments()[index];
        }

        public static bool CanBeUsedInMarkup(Type type, bool requireDefaultCtor = true)
        {
            if (type.IsInterface)
                return false;

            if (type.IsAbstract)
                return false;

            if (requireDefaultCtor)
            {
                if (type.GetConstructor(new Type[] { }) == null)
                    return false;
            }

            return true;
        }

        #region Expression Helper

        public static string PropertyName(Expression<Func<object>> propertyGetter)
        {
            return (propertyGetter.Body as MemberExpression ?? ((UnaryExpression)propertyGetter.Body).Operand as MemberExpression).Member.Name;
        }

        public static string PropertyName<T>(Expression<Func<T, object>> propertyGetter)
        {
            return (propertyGetter.Body as MemberExpression ?? ((UnaryExpression)propertyGetter.Body).Operand as MemberExpression).Member.Name;
        }

        public static string PropertyName<T, TResult>(Expression<Func<T, TResult>> propertyGetter)
        {
            return (propertyGetter.Body as MemberExpression ?? ((UnaryExpression)propertyGetter.Body).Operand as MemberExpression).Member.Name;
        }

        public static string MethodName<T, TResult>(Expression<Func<T, Func<TResult>>> propertyGetter)
        {
            return (((((UnaryExpression)propertyGetter.Body).Operand as MethodCallExpression).Arguments[2] as ConstantExpression).Value as MethodInfo).Name;
        }

        public static string MethodName<T, TParam1, TResult>(Expression<Func<T, Func<TParam1, TResult>>> propertyGetter)
        {
            return (((((UnaryExpression)propertyGetter.Body).Operand as MethodCallExpression).Arguments[2] as ConstantExpression).Value as MethodInfo).Name;
        }

        public static string MethodName<T, TParam1, TParam2, TResult>(Expression<Func<T, System.Func<TParam1, TParam2, TResult>>> propertyGetter)
        {
            return (((((UnaryExpression)propertyGetter.Body).Operand as MethodCallExpression).Arguments[2] as ConstantExpression).Value as MethodInfo).Name;
        }

        public static string MethodName<T>(Expression<Func<T, Action>> propertyGetter)
        {
            return (((((UnaryExpression)propertyGetter.Body).Operand as MethodCallExpression).Arguments[2] as ConstantExpression).Value as MethodInfo).Name;
        }

        public static string MethodName<T, TParam1>(Expression<Func<T, Action<TParam1>>> propertyGetter)
        {
            return (((((UnaryExpression)propertyGetter.Body).Operand as MethodCallExpression).Arguments[2] as ConstantExpression).Value as MethodInfo).Name;
        }

        public static string MethodName<T, TParam1, TParam2>(Expression<Func<T, Action<TParam1, TParam2>>> propertyGetter)
        {
            return (((((UnaryExpression)propertyGetter.Body).Operand as MethodCallExpression).Arguments[2] as ConstantExpression).Value as MethodInfo).Name;
        }

        public static string MethodName<T, TParam1, TParam2, TParam3>(Expression<Func<T, Action<TParam1, TParam2, TParam3>>> propertyGetter)
        {
            return (((((UnaryExpression)propertyGetter.Body).Operand as MethodCallExpression).Arguments[2] as ConstantExpression).Value as MethodInfo).Name;
        }

        public static string MethodName<T, TParam1, TParam2, TParam3, TParam4>(Expression<Func<T, Action<TParam1, TParam2, TParam3, TParam4>>> propertyGetter)
        {
            return (((((UnaryExpression)propertyGetter.Body).Operand as MethodCallExpression).Arguments[2] as ConstantExpression).Value as MethodInfo).Name;
        }

        #endregion
    }
}
