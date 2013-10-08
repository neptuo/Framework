using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels
{
    public interface IMetadataReader
    {
        void Apply(Attribute attribute, IMetadataBuilder builder);
    }
}
