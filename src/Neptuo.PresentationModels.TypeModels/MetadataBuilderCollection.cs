using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels
{
    public class MetadataBuilderCollection : MetadataCollection, IMetadataBuilder
    {
        public void Set(string identifier, object value)
        {
            if (identifier == null)
                throw new ArgumentNullException("identifier");

            Values[identifier] = value;
        }
    }
}
