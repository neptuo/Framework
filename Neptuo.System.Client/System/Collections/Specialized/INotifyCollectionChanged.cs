using SharpKit.JavaScript;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpKit.Javascript.Private
{
    [JsType(Name = "System.Collections.Specialized.INotifyCollectionChanged", Filename = "~/res/System.Collections.js")]
    public interface JsImplINotifyCollectionChanged
    {
        event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}
