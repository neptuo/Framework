using Microsoft.Practices.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace Neptuo.Unity.Web
{
    public class PerSessionLifetimeManager : LifetimeManager
    {
        private readonly string guid = Guid.NewGuid().ToString();

        protected HttpSessionState Session
        {
            get { return HttpContext.Current.Session; }
        }

        public override object GetValue()
        {
            return Session[guid];
        }

        public override void RemoveValue()
        {
            Session.Remove(guid);
        }

        public override void SetValue(object newValue)
        {
            Session[guid] = newValue;
        }
    }
}
