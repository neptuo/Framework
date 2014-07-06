using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    [Obsolete]
    public interface IModelView
    {
        void SetValue(string identifier, object value);
        object GetValue(string identifier);
    }
}
