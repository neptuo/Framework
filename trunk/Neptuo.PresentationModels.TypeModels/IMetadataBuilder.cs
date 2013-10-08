using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels
{
    public interface IMetadataBuilder
    {
        void Set(string identifier, object value);
    }
}
