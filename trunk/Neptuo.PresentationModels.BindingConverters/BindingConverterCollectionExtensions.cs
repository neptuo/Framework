using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.BindingConverters
{
    public static class BindingConverterCollectionExtensions
    {
        public static BindingConverterCollection AddStandart(this BindingConverterCollection bindingConverters)
        {
            return bindingConverters
                .Add(new TypeFieldType(typeof(bool)), new BoolBindingConverter())
                .Add(new TypeFieldType(typeof(int)), new IntBindingConverter())
                .Add(new TypeFieldType(typeof(double)), new DoubleBindingConverter())
                .Add(new TypeFieldType(typeof(string)), new StringBindingConverter())

                .Add(new TypeFieldType(typeof(bool?)), new NullBindingConverter<bool>(new BoolBindingConverter()))
                .Add(new TypeFieldType(typeof(int?)), new NullBindingConverter<int>(new IntBindingConverter()))
                .Add(new TypeFieldType(typeof(double?)), new NullBindingConverter<double>(new DoubleBindingConverter()))

                .Add(new TypeFieldType(typeof(IEnumerable<bool>)), new ListBindingConverter<bool>(",", new BoolBindingConverter()))
                .Add(new TypeFieldType(typeof(IEnumerable<int>)), new ListBindingConverter<int>(",", new IntBindingConverter()))
                .Add(new TypeFieldType(typeof(IEnumerable<double>)), new ListBindingConverter<double>(",", new DoubleBindingConverter()))
                .Add(new TypeFieldType(typeof(IEnumerable<string>)), new ListBindingConverter<string>(",", new StringBindingConverter()));
        }
    }
}
