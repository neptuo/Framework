using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Entity
{
    /// <summary>
    /// The default implementation of <see cref="IEventContext"/>.
    /// </summary>
    public class EventContext : DbContext, IEventContext
    {
        public IDbSet<EventEntity> Events { get; private set; }
        public IDbSet<UnPublishedEventEntity> UnPublishedEvents { get; private set; }

        static EventContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EventContext>());
        }

        public EventContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Events = Set<EventEntity>();
            UnPublishedEvents = Set<UnPublishedEventEntity>();
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}
