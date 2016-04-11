using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Internals
{
    internal class HandlerDescriptorProvider
    {


        public HandlerDescriptor Create(object handler)
        {
            return new HandlerDescriptor(
                handler,

            );
        }
    }
}
