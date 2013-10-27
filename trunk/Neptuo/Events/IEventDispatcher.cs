using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Events
{
    public interface IEventDispatcher
    {
        void Publish<TEvent>(TEvent eventData);
    }
}
