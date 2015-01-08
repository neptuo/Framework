using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    /// <summary>
    /// Implementation of <see cref="IModelMetadataCollection"/> and <see cref="IFieldMetadataCollection"/>
    /// using dictionary.
    /// </summary>
    public class MetadataCollection : IModelMetadataCollection, IFieldMetadataCollection
    {
        /// <summary>
        /// Value storage.
        /// </summary>
        protected Dictionary<string, object> Values { get; private set; }
        
        public IEnumerable<string> Keys
        {
            get { return Values.Keys; }
        }

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
