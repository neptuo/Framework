using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Binding.Converters
{
    public class DoubleBindingConverter : FuncBindingConverter<double>
    {
        public DoubleBindingConverter()
            : base(Double.TryParse)
        { }
    }
}
