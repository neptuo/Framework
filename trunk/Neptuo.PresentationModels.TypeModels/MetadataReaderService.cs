using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.TypeModels
{
    public class MetadataReaderService
    {
        protected Dictionary<Type, IMetadataReader> Values { get; private set; }

        public MetadataReaderService()
        {
            Values = new Dictionary<Type, IMetadataReader>();
        }

        public void Register(Type attributeType, IMetadataReader reader)
        {
            if (attributeType == null)
                throw new ArgumentNullException("attributeType");

            if (reader == null)
                throw new ArgumentNullException("reader");

            Values[attributeType] = reader;
        }

        public bool TryGetReader(Type attributeType, out IMetadataReader reader)
        {
            return Values.TryGetValue(attributeType, out reader);
        }
    }
}
