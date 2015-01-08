using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Http
{
    /// <summary>
    /// Collections of known Http statuses.
    /// </summary>
    internal class HttpStatusCollection : KeyedCollection<int, HttpStatus>
    {
        protected override int GetKeyForItem(HttpStatus item)
        {
            return item.Code;
        }
    }
}
