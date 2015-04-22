using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Binding.Converters
{
    public class IntBindingConverter : FuncBindingConverter<int>
    {
        public IntBindingConverter()
            : base(Int32.TryParse)
        { }
    }
}
