using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpKit.UnobtrusiveFeatures
{
    public abstract class BaseAstExtension : BaseExtension
    {
        /// <summary>
        /// Js clr helper.
        /// </summary>
        protected JsClrHelper Helper { get; private set; }

        public BaseAstExtension(string extensionName, bool debug)
            : base(extensionName, debug)
        {
            Helper = new JsClrHelper();
        }

    }
}
