using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Dependencies.Handlers
{
    public class CompositeDependencyImporter : IDependencyImporter
    {
        private readonly List<IDependencyImporter> importers = new List<IDependencyImporter>();

        public CompositeDependencyImporter()
        {
            this.importers = new List<IDependencyImporter>();
        }

        public CompositeDependencyImporter(IEnumerable<IDependencyImporter> importers)
        {
            Ensure.NotNull(importers, "importers");
            this.importers = new List<IDependencyImporter>(importers);
        }

        public CompositeDependencyImporter Add(IDependencyImporter importer)
        {
            Ensure.NotNull(importer, "importer");
            importers.Add(importer);
            return this;
        }

        public object Import(Type depedencyType)
        {
            foreach (IDependencyImporter importer in importers)
            {
                object result = importer.Import(depedencyType);
                if (result != null)
                    return result;
            }

            return null;
        }
    }
}
