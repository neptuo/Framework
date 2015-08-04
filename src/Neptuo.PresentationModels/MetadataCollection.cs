using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    /// <summary>
    /// Default implementation of <see cref="IMetadataBuilder"/> based on <see cref="KeyValueCollection"/>.
    /// </summary>
    public class MetadataCollection : KeyValueCollection, IMetadataBuilder
    {
        public IMetadataBuilder Add(string identifier, object value)
        {
            base.Add(identifier, value);
            return this;
        }
    }
}