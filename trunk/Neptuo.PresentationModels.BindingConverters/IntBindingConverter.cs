using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.BindingConverters
{
    public class IntBindingConverter : FuncBindingConverter<int>
    {
        public IntBindingConverter()
            : base(Int32.TryParse)
        { }
    }
}
