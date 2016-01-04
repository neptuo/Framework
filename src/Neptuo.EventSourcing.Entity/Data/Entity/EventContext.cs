using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Entity
{
    /// <summary>
    /// Default implementation of <see cref="IEventContext"/>.
    /// </summary>
    public class EventContext : DbContext, IEventContext
    {
        public IDbSet<EventEntity> Events { get; private set; }

        public new void SaveChanges()
        {
            SaveChanges();
        }
    }
}
