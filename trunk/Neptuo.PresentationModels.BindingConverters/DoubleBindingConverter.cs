using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.BindingConverters
{
    public class DoubleBindingConverter : FuncBindingConverter<double>
    {
        public DoubleBindingConverter()
            : base(Double.TryParse)
        { }
    }
}
