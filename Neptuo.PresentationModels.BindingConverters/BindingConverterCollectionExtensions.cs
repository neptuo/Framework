using Neptuo.PresentationModels.Binding;
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
                .Add(new FieldType(typeof(bool)), new BoolBindingConverter())
                .Add(new FieldType(typeof(int)), new IntBindingConverter())
                .Add(new FieldType(typeof(double)), new DoubleBindingConverter())
                .Add(new FieldType(typeof(string)), new StringBindingConverter())

                .Add(new FieldType(typeof(bool?)), new NullBindingConverter<bool>(new BoolBindingConverter()))
                .Add(new FieldType(typeof(int?)), new NullBindingConverter<int>(new IntBindingConverter()))
                .Add(new FieldType(typeof(double?)), new NullBindingConverter<double>(new DoubleBindingConverter()))

                .Add(new FieldType(typeof(IEnumerable<bool>)), new ListBindingConverter<bool>(",", new BoolBindingConverter()))
                .Add(new FieldType(typeof(IEnumerable<int>)), new ListBindingConverter<int>(",", new IntBindingConverter()))
                .Add(new FieldType(typeof(IEnumerable<double>)), new ListBindingConverter<double>(",", new DoubleBindingConverter()))
                .Add(new FieldType(typeof(IEnumerable<string>)), new ListBindingConverter<string>(",", new StringBindingConverter()));
        }
    }
}
