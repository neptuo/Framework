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
                .Add(typeof(bool), new BoolBindingConverter())
                .Add(typeof(int), new IntBindingConverter())
                .Add(typeof(double), new DoubleBindingConverter())
                .Add(typeof(string), new StringBindingConverter())

                .Add(typeof(bool?), new NullBindingConverter<bool>(new BoolBindingConverter()))
                .Add(typeof(int?), new NullBindingConverter<int>(new IntBindingConverter()))
                .Add(typeof(double?), new NullBindingConverter<double>(new DoubleBindingConverter()))

                .Add(typeof(IEnumerable<bool>), new ListBindingConverter<bool>(",", new BoolBindingConverter()))
                .Add(typeof(IEnumerable<int>), new ListBindingConverter<int>(",", new IntBindingConverter()))
                .Add(typeof(IEnumerable<double>), new ListBindingConverter<double>(",", new DoubleBindingConverter()))
                .Add(typeof(IEnumerable<string>), new ListBindingConverter<string>(",", new StringBindingConverter()));
        }
    }
}
