using Neptuo.FeatureModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel
{
    /// <summary>
    /// Describes error.
    /// </summary>
    public interface IErrorModel : IFeatureModel
    {
        /// <summary>
        /// Text description of error.
        /// </summary>
        string Text { get; }
    }
}
