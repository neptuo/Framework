using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Processing.Metadata
{
    public class ValueInputCollection : IValueInputProvider
    {
        private readonly List<IValueInputProvider> importers = new List<IValueInputProvider>();

        public ValueInputCollection()
        {
            this.importers = new List<IValueInputProvider>();
        }

        public ValueInputCollection(IEnumerable<IValueInputProvider> importers)
        {
            Ensure.NotNull(importers, "importers");
            this.importers = new List<IValueInputProvider>(importers);
        }

        public ValueInputCollection Add(IValueInputProvider importer)
        {
            Ensure.NotNull(importer, "importer");
            importers.Add(importer);
            return this;
        }

        public object Import(Type depedencyType)
        {
            foreach (IValueInputProvider importer in importers)
            {
                object result = importer.Import(depedencyType);
                if (result != null)
                    return result;
            }

            return null;
        }
    }
}
