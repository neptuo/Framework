using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Resources.FileResources
{
    public abstract class WithMetaBase : IWithMeta
    {
        public IDictionary<string, string> Metadata { get; private set; }

        protected WithMetaBase(IDictionary<string, string> metadata)
        {
            Guard.NotNull(metadata, "metadata");
            Metadata = metadata;
        }

        public string Meta(string key, string defaultValue)
        {
            Guard.NotNullOrEmpty(key, "key");
            string result;
            if (Metadata.TryGetValue(key, out result))
                return result;

            return defaultValue;
        }
    }
}
