using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Converters
{
    /// <summary>
    /// The implementation of <see cref="ICompositeConverter"/> that enables to register converters for concrete types
    /// and register search handler.
    /// </summary>
    public class CompositeConverterCollection : CompositeConverterBase
    {
        private readonly Dictionary<Type, ICompositeConverter> converters = new Dictionary<Type, ICompositeConverter>();
        private readonly OutFuncCollection<Type, ICompositeConverter, bool> onSearchConverter = new OutFuncCollection<Type, ICompositeConverter, bool>();

        public CompositeConverterCollection Add(Type valueType, ICompositeConverter formatter)
        {
            Ensure.NotNull(valueType, "valueType");
            Ensure.NotNull(formatter, "formatter");
            converters[valueType] = formatter;
            return this;
        }

        public CompositeConverterCollection AddSearchHandler(OutFunc<Type, ICompositeConverter, bool> handler)
        {
            Ensure.NotNull(handler, "handler");
            onSearchConverter.Add(handler);
            return this;
        }

        private ICompositeConverter FindFormatter(Type valueType)
        {
            ICompositeConverter formatter;
            if (!converters.TryGetValue(valueType, out formatter))
                onSearchConverter.TryExecute(valueType, out formatter);

            return formatter;
        }

        protected override bool TryDeserialize(CompositeDeserializerContext context, out object value)
        {
            ICompositeConverter converter = FindFormatter(context.ValueType);
            if (converter == null)
            {
                value = null;
                return false;
            }

            return converter.TryConvert(context, out value);
        }

        protected override bool TrySerialize(CompositeSerializerContext context)
        {
            if (context.Value == null)
            {
                context.Storage.Add(context.Key, null);
                return true;
            }

            Type valueType = context.Value.GetType();

            ICompositeConverter converter = FindFormatter(valueType);
            if (converter == null)
                return false;

            bool resultValue;
            return converter.TryConvert(context, out resultValue);
        }
    }
}
