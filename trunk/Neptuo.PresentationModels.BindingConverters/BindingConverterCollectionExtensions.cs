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
                .Add(new TypeFieldType(typeof(string)), new StringBindingConverter());
        }
    }
}
