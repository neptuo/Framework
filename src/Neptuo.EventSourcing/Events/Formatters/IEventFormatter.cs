using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Events.Formatters
{
    /// <summary>
    /// Serializes and deserializes event payloads.
    /// </summary>
    public interface IEventFormatter
    {
        string Serialize(IEvent payload);
        IEvent Deserialize(string payload);
    }
}
