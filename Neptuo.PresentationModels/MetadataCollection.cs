using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    public class MetadataCollection : IModelMetadataCollection, IFieldMetadataCollection
    {
        protected Dictionary<string, object> Values { get; private set; }

        public MetadataCollection()
        {
            Values = new Dictionary<string, object>();
        }

        public bool TryGet(string key, out object value)
        {
            return Values.TryGetValue(key, out value);
        }
    }
}
