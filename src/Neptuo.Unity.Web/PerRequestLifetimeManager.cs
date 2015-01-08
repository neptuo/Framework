using Microsoft.Practices.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Neptuo.Unity.Web
{
    public class PerRequestLifetimeManager : ContainerLifetimeManagerBase
    {
        protected override IDictionary Container
        {
            get { return HttpContext.Current.Items; }
        }
    }
}
