using Neptuo.FeatureModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel
{
    /// <summary>
    /// Default extensible implementation of <see cref="IErrorModel"/>.
    /// This implementation is not thread safe.
    /// </summary>
    public class DefaultErrorModel : FeatureCollectionModel
    {
        public string Text { get; private set; }

        public DefaultErrorModel(string text)
            : base(true)
        {
            Text = text;
        }
    }
}
