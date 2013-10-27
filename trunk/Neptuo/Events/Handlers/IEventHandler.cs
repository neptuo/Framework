using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Events.Handlers
{
    public interface IEventHandler<TEvent>
    {
        void Handle(TEvent eventData);
    }
}
