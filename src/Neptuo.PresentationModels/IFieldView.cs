using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    [Obsolete]
    public interface IFieldView
    {
        void SetValue(object value);
        object GetValue();
    }
}
