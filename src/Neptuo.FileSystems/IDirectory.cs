using Neptuo.Models.Features;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems
{
    /// <summary>
    /// Represents directory in virtual file system.
    /// </summary>
    public interface IDirectory : IFeatureModel
    {
        /// <summary>
        /// Directory name.
        /// </summary>
        string Name { get; }
    }
}
