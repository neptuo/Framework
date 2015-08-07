using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features.Attributes
{
    /// <summary>
    /// Defines contract for modified attributes.
    /// </summary>
    public interface IAttributeUpdater : IAttributeReader, IKeyValueCollection
    { }
}
