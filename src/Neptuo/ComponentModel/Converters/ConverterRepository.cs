using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel.Converters
{
    public class ConverterRepository : IConverterRepository
    {
        protected Dictionary<Type, Dictionary<Type, IConverter>> Storage { get; private set; }

        public ConverterRepository()
            : this(new Dictionary<Type, Dictionary<Type, IConverter>>())
        { }

        public ConverterRepository(Dictionary<Type, Dictionary<Type, IConverter>> storage)
        {
            Ensure.NotNull(storage, "storage");
            Storage = storage;
        }

        public IConverterRepository Add(Type sourceType, Type targetType, IConverter converter)
        {
            Ensure.NotNull(sourceType, "sourceType");
            Ensure.NotNull(targetType, "targetType");
            Ensure.NotNull(converter, "converter");

            Dictionary<Type, IConverter> storage;
            if (!Storage.TryGetValue(sourceType, out storage))
                storage = Storage[sourceType] = new Dictionary<Type, IConverter>();

            storage[targetType] = converter;
            return this;
        }

        public event ConverterSearchDelegate OnSearchConverter;

        public bool TryConvert<TSource, TTarget>(TSource sourceValue, out TTarget targetValue)
        {
            Type sourceType = typeof(TSource);
            Type targetType = typeof(TTarget);

            IConverter converter = null;
            Dictionary<Type, IConverter> storage;
            if (!Storage.TryGetValue(sourceType, out storage) || !storage.TryGetValue(targetType, out converter))
            {
                if (OnSearchConverter != null)
                    converter = OnSearchConverter(sourceType, targetType);
            }

            if (converter == null)
            {
                if (sourceValue == null)
                {
                    targetValue = default(TTarget);
                    return true;
                }

                targetValue = default(TTarget);
                return false;
            }

            IConverter<TSource, TTarget> genericConverter = converter as IConverter<TSource, TTarget>;
            if (genericConverter != null)
                return genericConverter.TryConvert(sourceValue, out targetValue);

            object targetObject;
            if (converter.TryConvertGeneral(sourceType, targetType, sourceValue, out targetObject))
            {
                if (targetObject is TTarget)
                {
                    targetValue = (TTarget)targetObject;
                    return true;
                }
            }

            targetValue = default(TTarget);
            return false;
        }

        public bool TryConvert(Type sourceType, Type targetType, object sourceValue, out object targetValue)
        {
            IConverter converter = null;
            Dictionary<Type, IConverter> storage;
            if (!Storage.TryGetValue(sourceType, out storage) || !storage.TryGetValue(targetType, out converter))
            {
                if (OnSearchConverter != null)
                    converter = OnSearchConverter(sourceType, targetType);
            }

            if (converter == null)
            {
                targetValue = null;
                return false;
            }

            return converter.TryConvertGeneral(sourceType, targetType, sourceValue, out targetValue);
        }
    }
}
