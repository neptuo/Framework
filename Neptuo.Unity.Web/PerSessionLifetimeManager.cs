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

        /// <summary>
        /// Sets TTL for request.
        /// </summary>
        public int? HopCount { get; set; }

        protected HttpSessionState Session
        {
            get { return HttpContext.Current.Session; }
        }

        protected IDictionary Items
        {
            get { return HttpContext.Current.Items; }
        }

        public PerSessionLifetimeManager(int? hopCount = null)
        {
            HopCount = hopCount;
        }

        public override object GetValue()
        {
            ModelWrapper model = (ModelWrapper)Session[guid];
            if (model == null)
                return null;

            if (model.HopCount != null && !Items.Contains(guid))
            {
                if(model.HopCount == 0)
                {
                    RemoveValue();
                    return null;
                }

                Items[guid] = DateTime.Now;
                model.HopCount--;
            }

            return model.Value;
        }

        public override void RemoveValue()
        {
            Session.Remove(guid);
        }

        public override void SetValue(object newValue)
        {
            Session[guid] = new ModelWrapper(newValue, HopCount);
        }

        public class ModelWrapper
        {
            public object Value { get; set; }
            public int? HopCount { get; set; }

            public ModelWrapper(object value, int? hopCount)
            {
                Value = value;
                HopCount = hopCount;
            }
        }
    }
}
