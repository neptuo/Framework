using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Converters
{
    /// <summary>
    /// Default implmentation of <see cref="IConverterRepository"/>.
    /// </summary>
    public class DefaultConverterRepository : IConverterRepository
    {
        private readonly Dictionary<Type, Dictionary<Type, IConverter>> storage;
        private readonly OutFuncCollection<ConverterSearchContext, IConverter, bool> onSearchConverter;

        /// <summary>
        /// Cretes new empty instance.
        /// </summary>
        public DefaultConverterRepository()
            : this(new Dictionary<Type, Dictionary<Type, IConverter>>())
        { }

        /// <summary>
        /// Creates instance with default converter registrations.
        /// </summary>
        /// <param name="storage">'First is the source type, second key is the target type' storage.</param>
        public DefaultConverterRepository(Dictionary<Type, Dictionary<Type, IConverter>> storage)
        {
            Ensure.NotNull(storage, "storage");
            this.storage = storage;
            this.onSearchConverter = new OutFuncCollection<ConverterSearchContext, IConverter, bool>();
        }

        public IConverterRepository Add(Type sourceType, Type targetType, IConverter converter)
        {
            Ensure.NotNull(sourceType, "sourceType");
            Ensure.NotNull(targetType, "targetType");
            Ensure.NotNull(converter, "converter");

            Dictionary<Type, IConverter> sourceStorage;
            if (!storage.TryGetValue(sourceType, out sourceStorage))
                storage[sourceType] = sourceStorage = new Dictionary<Type, IConverter>();

            sourceStorage[targetType] = converter;
            return this;
        }

        public IConverterRepository AddSearchHandler(OutFunc<ConverterSearchContext, IConverter, bool> searchHandler)
        {
            Ensure.NotNull(searchHandler, "searchHandler");
            onSearchConverter.Add(searchHandler);
            return this;
        }

        public bool TryConvert<TSource, TTarget>(TSource sourceValue, out TTarget targetValue)
        {
            Type sourceType = typeof(TSource);
            Type targetType = typeof(TTarget);

            IConverter converter = null;
            Dictionary<Type, IConverter> sourceStorage;
            if (!storage.TryGetValue(sourceType, out sourceStorage) || !sourceStorage.TryGetValue(targetType, out converter))
                onSearchConverter.TryExecute(new ConverterSearchContext(sourceType, targetType), out converter);

            if (converter == null)
            {
                targetValue = default(TTarget);
                return false;
            }

            IConverter<TSource, TTarget> genericConverter = converter as IConverter<TSource, TTarget>;
            if (genericConverter != null)
                return genericConverter.TryConvert(sourceValue, out targetValue);

            object targetObject;
            if (converter.TryConvert(sourceType, targetType, sourceValue, out targetObject))
            {
                targetValue = (TTarget)targetObject;
                return true;
            }

            targetValue = default(TTarget);
            return false;
        }

        public bool TryConvert(Type sourceType, Type targetType, object sourceValue, out object targetValue)
        {
            IConverter converter = null;
            Dictionary<Type, IConverter> sourceStorage;
            if (!storage.TryGetValue(sourceType, out sourceStorage) || !sourceStorage.TryGetValue(targetType, out converter))
                onSearchConverter.TryExecute(new ConverterSearchContext(sourceType, targetType), out converter);

            if (converter == null)
            {
                targetValue = null;
                return false;
            }

            return converter.TryConvert(sourceType, targetType, sourceValue, out targetValue);
        }
    }
}
