using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Converters
{
    /// <summary>
    /// Default implmentation of <see cref="IConverterRepository"/>.
    /// </summary>
    public class DefaultConverterRepository : IConverterRepository
    {
        private static readonly Action<Exception> defaultExceptionHandler = e =>
        {
            ExceptionDispatchInfo info = ExceptionDispatchInfo.Capture(e);
            info.Throw();
        };

        private readonly object storageLock = new object();
        private readonly Dictionary<Type, Dictionary<Type, IConverter>> storage;
        private readonly OutFuncCollection<ConverterSearchContext, IConverter, bool> onSearchConverter;
        private readonly IConverterRepository inner;
        private readonly Action<Exception> exceptionHandler;

        /// <summary>
        /// Creates a new empty instance.
        /// </summary>
        public DefaultConverterRepository()
            : this(defaultExceptionHandler)
        { }

        /// <summary>
        /// Creates a new empty instance with exception filtering handler.
        /// </summary>
        /// <param name="exceptionHandler">A handler for expcetions raised by converters.</param>
        public DefaultConverterRepository(Action<Exception> exceptionHandler)
            : this(new Dictionary<Type, Dictionary<Type, IConverter>>(), exceptionHandler)
        { }

        /// <summary>
        /// Creates a new instance that uses <paramref name="inner"/> if converter is not found.
        /// </summary>
        /// <param name="inner"></param>
        public DefaultConverterRepository(IConverterRepository inner)
            : this(inner, defaultExceptionHandler)
        { }

        /// <summary>
        /// Creates a new instance that uses <paramref name="inner"/> if converter is not found.
        /// </summary>
        /// <param name="inner"></param>
        /// <param name="exceptionHandler">A handler for expcetions raised by converters.</param>
        public DefaultConverterRepository(IConverterRepository inner, Action<Exception> exceptionHandler)
        {
            Ensure.NotNull(inner, "inner");
            Ensure.NotNull(exceptionHandler, "exceptionHandler");
            this.inner = inner;
            this.exceptionHandler = exceptionHandler;
            this.onSearchConverter = new OutFuncCollection<ConverterSearchContext, IConverter, bool>();
        }

        /// <summary>
        /// Creates instance with default converter registrations.
        /// </summary>
        /// <param name="storage">'First is the source type, second key is the target type' storage.</param>
        public DefaultConverterRepository(Dictionary<Type, Dictionary<Type, IConverter>> storage)
            : this(storage, defaultExceptionHandler)
        { }

        /// <summary>
        /// Creates instance with default converter registrations.
        /// </summary>
        /// <param name="storage">'First is the source type, second key is the target type' storage.</param>
        /// <param name="exceptionHandler">A handler for expcetions raised by converters.</param>
        public DefaultConverterRepository(Dictionary<Type, Dictionary<Type, IConverter>> storage, Action<Exception> exceptionHandler)
        {
            Ensure.NotNull(storage, "storage");
            Ensure.NotNull(exceptionHandler, "exceptionHandler");
            this.storage = storage;
            this.exceptionHandler = exceptionHandler;
            this.onSearchConverter = new OutFuncCollection<ConverterSearchContext, IConverter, bool>();
        }

        public IConverterRepository Add(Type sourceType, Type targetType, IConverter converter)
        {
            Ensure.NotNull(sourceType, "sourceType");
            Ensure.NotNull(targetType, "targetType");
            Ensure.NotNull(converter, "converter");

            Dictionary<Type, IConverter> sourceStorage;
            if (!storage.TryGetValue(sourceType, out sourceStorage))
            {
                lock (storageLock)
                {
                    if (!storage.TryGetValue(sourceType, out sourceStorage))
                        storage[sourceType] = sourceStorage = new Dictionary<Type, IConverter>();
                }
            }

            lock (storageLock)
                sourceStorage[targetType] = converter;

            return this;
        }

        public IConverterRepository AddSearchHandler(OutFunc<ConverterSearchContext, IConverter, bool> searchHandler)
        {
            Ensure.NotNull(searchHandler, "searchHandler");
            lock (storageLock)
                onSearchConverter.Add(searchHandler);

            return this;
        }

        private bool IsConverterContextType(Type type)
        {
            return type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(IConverterContext<>);
        }

        public bool TryConvert<TSource, TTarget>(TSource sourceValue, out TTarget targetValue)
        {
            Type sourceType = typeof(TSource);
            Type targetType = typeof(TTarget);

            // If target value is assignable from source, no conversion is needed.
            if (targetType.IsAssignableFrom(sourceType))
            {
                targetValue = (TTarget)(object)sourceValue;
                return true;
            }

            // If source value is null, return default value.
            if (sourceValue == null)
            {
                targetValue = default(TTarget);
                return true;
            }

            // Find converter, look in storage or find using search handler.
            IConverter converter = null;
            Dictionary<Type, IConverter> sourceStorage;
            if (!storage.TryGetValue(sourceType, out sourceStorage) || !sourceStorage.TryGetValue(targetType, out converter))
                onSearchConverter.TryExecute(new ConverterSearchContext(sourceType, targetType), out converter);

            // If no converter was found, try context converters.
            if (converter == null && !IsConverterContextType(sourceType))
                return TryConvert<IConverterContext<TSource>, TTarget>(new DefaultConverterContext<TSource>(sourceValue, this), out targetValue);

            // If no converter was found, conversion is not possible.
            if (converter == null)
            {
                if (inner != null)
                    return inner.TryConvert(sourceValue, out targetValue);

                targetValue = default(TTarget);
                return false;
            }

            // Try cast to generic converter.
            IConverter<TSource, TTarget> genericConverter = converter as IConverter<TSource, TTarget>;
            if (genericConverter != null)
            {
                try
                {
                    return genericConverter.TryConvert(sourceValue, out targetValue);
                }
                catch (Exception e)
                {
                    exceptionHandler(e);
                }
            }

            try
            {
                // Convert using general converter.
                object targetObject;
                if (converter.TryConvert(sourceType, targetType, sourceValue, out targetObject))
                {
                    targetValue = (TTarget)targetObject;
                    return true;
                }
            }
            catch (Exception e)
            {
                exceptionHandler(e);
            }

            // No other options for conversion.
            targetValue = default(TTarget);
            return false;
        }

        public bool TryConvert(Type sourceType, Type targetType, object sourceValue, out object targetValue)
        {
            Ensure.NotNull(sourceType, "sourceType");
            Ensure.NotNull(targetType, "targetType");

            // If target value is assignable from source, no conversion is needed.
            if (targetType.IsAssignableFrom(sourceType))
            {
                targetValue = sourceValue;
                return true;
            }

            // If source value is null, return default value.
            if (sourceValue == null)
            {
                if (targetType.GetTypeInfo().IsValueType)
                    targetValue = Activator.CreateInstance(targetType);
                else
                    targetValue = null;

                return true;
            }

            // Find converter, look in storage or find using search handler.
            IConverter converter = null;
            Dictionary<Type, IConverter> sourceStorage;
            if (!storage.TryGetValue(sourceType, out sourceStorage) || !sourceStorage.TryGetValue(targetType, out converter))
                onSearchConverter.TryExecute(new ConverterSearchContext(sourceType, targetType), out converter);

            // If no converter was found, try context converters.
            if (converter == null && !IsConverterContextType(sourceType))
            {
                Type sourceContextType = typeof(IConverterContext<>).MakeGenericType(sourceType);

                if (!storage.TryGetValue(sourceContextType, out sourceStorage) || !sourceStorage.TryGetValue(targetType, out converter))
                    onSearchConverter.TryExecute(new ConverterSearchContext(sourceContextType, targetType), out converter);

                // If context converter was found, create instance of context.
                if (converter != null)
                {
                    Type concreteContextType = typeof(DefaultConverterContext<>).MakeGenericType(sourceType);
                    ConstructorInfo concreteContextConstructor = concreteContextType.GetConstructor(new Type[] { sourceType, typeof(IConverterRepository) });
                    if (concreteContextConstructor != null)
                    {
                        sourceValue = concreteContextConstructor.Invoke(new object[] { sourceValue, this });
                        sourceType = sourceContextType;
                    }
                    else
                    {
                        converter = null;
                    }
                }
            }

            // If no converter was found, conversion is not possible.
            if (converter == null)
            {
                if (inner != null)
                    return inner.TryConvert(sourceType, targetType, sourceValue, out targetValue);

                targetValue = null;
                return false;
            }

            try
            {
                // Convert using general converter.
                return converter.TryConvert(sourceType, targetType, sourceValue, out targetValue);
            }
            catch (Exception e)
            {
                exceptionHandler(e);
            }

            targetValue = null;
            return false;
        }

        public Func<TSource, TTarget> GetConverter<TSource, TTarget>()
        {
            Type sourceType = typeof(TSource);
            Type targetType = typeof(TTarget);

            // If target value is assignable from source, no conversion is needed.
            if (targetType.IsAssignableFrom(sourceType))
                return sourceValue => (TTarget)(object)sourceValue;

            // Find converter, look in storage or find using search handler.
            IConverter converter = null;
            Dictionary<Type, IConverter> sourceStorage;
            if (!storage.TryGetValue(sourceType, out sourceStorage) || !sourceStorage.TryGetValue(targetType, out converter))
                onSearchConverter.TryExecute(new ConverterSearchContext(sourceType, targetType), out converter);

            // If no converter was found, try context converters.
            if (converter == null && !IsConverterContextType(sourceType))
            {
                Func<IConverterContext<TSource>, TTarget> result = GetConverter<IConverterContext<TSource>, TTarget>();
                return sourceValue => result(new DefaultConverterContext<TSource>(sourceValue, this));
            }

            // If no converter was found, conversion is not possible.
            if (converter == null)
            {
                if (inner != null)
                    return inner.GetConverter<TSource, TTarget>();

                return sourceValue =>
                {
                    // If source value is null, return default value.
                    if (sourceValue == null)
                        return default(TTarget);

                    return default(TTarget);
                };
            }

            // Try cast to generic converter.
            IConverter<TSource, TTarget> genericConverter = converter as IConverter<TSource, TTarget>;
            if (genericConverter != null)
            {
                return sourceValue =>
                {
                    try
                    {
                        TTarget targetValue;
                        if (genericConverter.TryConvert(sourceValue, out targetValue))
                            return targetValue;
                    }
                    catch (Exception e)
                    {
                        exceptionHandler(e);
                        return default(TTarget);
                    }

                    // No other options for conversion.
                    throw Ensure.Exception.NotSupportedConversion(targetType, sourceValue);
                };
            }

            return sourceValue =>
            {
                try
                {
                    // Convert using general converter.
                    object targetObject;
                    if (converter.TryConvert(sourceType, targetType, sourceValue, out targetObject))
                        return (TTarget)targetObject;
                }
                catch (Exception e)
                {
                    exceptionHandler(e);
                    return default(TTarget);
                }

                // No other options for conversion.
                throw Ensure.Exception.NotSupportedConversion(targetType, sourceValue);
            };
        }

        public OutFunc<TSource, TTarget, bool> GetTryConverter<TSource, TTarget>()
        {
            Type sourceType = typeof(TSource);
            Type targetType = typeof(TTarget);

            // If target value is assignable from source, no conversion is needed.
            if (targetType.IsAssignableFrom(sourceType))
            {
                return (TSource sourceValue, out TTarget targetValue) =>
                {
                    targetValue = (TTarget)(object)sourceValue;
                    return true;
                };
            }

            // Find converter, look in storage or find using search handler.
            IConverter converter = null;
            Dictionary<Type, IConverter> sourceStorage;
            if (!storage.TryGetValue(sourceType, out sourceStorage) || !sourceStorage.TryGetValue(targetType, out converter))
                onSearchConverter.TryExecute(new ConverterSearchContext(sourceType, targetType), out converter);

            // If no converter was found, try context converters.
            if (converter == null && !IsConverterContextType(sourceType))
            {
                OutFunc<IConverterContext<TSource>, TTarget, bool> result = GetTryConverter<IConverterContext<TSource>, TTarget>();
                return (TSource sourceValue, out TTarget targetValue) => result(new DefaultConverterContext<TSource>(sourceValue, this), out targetValue);
            }

            // If no converter was found, conversion is not possible.
            if (converter == null)
            {
                if (inner != null)
                    return inner.GetTryConverter<TSource, TTarget>();

                return (TSource sourceValue, out TTarget targetValue) =>
                {
                    // If source value is null, return default value.
                    if (sourceValue == null)
                    {
                        targetValue = default(TTarget);
                        return true;
                    }

                    targetValue = default(TTarget);
                    return false;
                };
            }

            // Try cast to generic converter.
            IConverter<TSource, TTarget> genericConverter = converter as IConverter<TSource, TTarget>;
            if (genericConverter != null)
                return new ExceptionHandlingConverter<TSource, TTarget>(genericConverter, exceptionHandler).TryConvert;

            // Convert using general converter.
            return (TSource sourceValue, out TTarget targetValue) =>
            {
                try
                {
                    object targetObject;
                    if (converter.TryConvert(sourceType, targetType, sourceValue, out targetObject))
                    {
                        targetValue = (TTarget)targetObject;
                        return true;
                    }
                }
                catch (Exception e)
                {
                    exceptionHandler(e);
                }

                // No other options for conversion.
                targetValue = default(TTarget);
                return false;
            };
        }


        public bool HasConverter<TSource, TTarget>()
        {
            Type sourceType = typeof(TSource);
            Type targetType = typeof(TTarget);

            // If target value is assignable from source, no conversion is needed.
            if (targetType.IsAssignableFrom(sourceType))
                return true;

            // Find converter, look in storage or find using search handler.
            IConverter converter = null;
            Dictionary<Type, IConverter> sourceStorage;
            if (!storage.TryGetValue(sourceType, out sourceStorage) || !sourceStorage.TryGetValue(targetType, out converter))
                onSearchConverter.TryExecute(new ConverterSearchContext(sourceType, targetType), out converter);

            // If no converter was found, try context converters.
            if (converter == null && !IsConverterContextType(sourceType))
                return HasConverter<IConverterContext<TSource>, TTarget>();

            // If no converter was found, conversion is not possible.
            if (converter == null)
            {
                if (inner != null)
                    return inner.HasConverter<TSource, TTarget>();

                return false;
            }

            return true;
        }

        public bool HasConverter(Type sourceType, Type targetType)
        {
            Ensure.NotNull(sourceType, "sourceType");
            Ensure.NotNull(targetType, "targetType");

            // If target value is assignable from source, no conversion is needed.
            if (targetType.IsAssignableFrom(sourceType))
                return true;

            // Find converter, look in storage or find using search handler.
            IConverter converter = null;
            Dictionary<Type, IConverter> sourceStorage;
            if (!storage.TryGetValue(sourceType, out sourceStorage) || !sourceStorage.TryGetValue(targetType, out converter))
                onSearchConverter.TryExecute(new ConverterSearchContext(sourceType, targetType), out converter);

            // If no converter was found, try context converters.
            if (converter == null && !IsConverterContextType(sourceType))
            {
                Type sourceContextType = typeof(IConverterContext<>).MakeGenericType(sourceType);
                return HasConverter(sourceContextType, targetType);
            }

            // If no converter was found, conversion is not possible.
            if (converter == null)
            {
                if (inner != null)
                    return inner.HasConverter(sourceType, targetType);

                return false;
            }

            return true;
        }
    }
}