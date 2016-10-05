using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Neptuo.PresentationModels.UI
{
    /// <summary>
    /// The context of rendering view.
    /// </summary>
    public class RenderContext
    {
        /// <summary>
        /// Gets the parent control to place controls into.
        /// </summary>
        public Control Parent { get; private set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="parent">The parent control to place controls into.</param>
        public RenderContext(Control parent)
        {
            Ensure.NotNull(parent, "parent");
            Parent = parent;
        }
    }
}
