using Neptuo.PresentationModels.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Binding.Converters
{
    public static class _BindingConverterCollectionExtensions
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

                .Add(typeof(IEnumerable<bool>), new EnumerableBindingConverter<bool>(",", new BoolBindingConverter()))
                .Add(typeof(IEnumerable<int>), new EnumerableBindingConverter<int>(",", new IntBindingConverter()))
                .Add(typeof(IEnumerable<double>), new EnumerableBindingConverter<double>(",", new DoubleBindingConverter()))
                .Add(typeof(IEnumerable<string>), new EnumerableBindingConverter<string>(",", new StringBindingConverter()))

                .Add(typeof(ICollection<bool>), new CollectionBindingConverter<bool>(",", new BoolBindingConverter()))
                .Add(typeof(ICollection<int>), new CollectionBindingConverter<int>(",", new IntBindingConverter()))
                .Add(typeof(ICollection<double>), new CollectionBindingConverter<double>(",", new DoubleBindingConverter()))
                .Add(typeof(ICollection<string>), new CollectionBindingConverter<string>(",", new StringBindingConverter()))

                .Add(typeof(List<bool>), new ListBindingConverter<bool>(",", new BoolBindingConverter()))
                .Add(typeof(List<int>), new ListBindingConverter<int>(",", new IntBindingConverter()))
                .Add(typeof(List<double>), new ListBindingConverter<double>(",", new DoubleBindingConverter()))
                .Add(typeof(List<string>), new ListBindingConverter<string>(",", new StringBindingConverter()));
        }
    }
}
