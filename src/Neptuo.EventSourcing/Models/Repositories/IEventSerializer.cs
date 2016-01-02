using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Repositories
{
    /// <summary>
    /// Serializes and deserializes event payloads.
    /// </summary>
    public interface IEventSerializer
    {
        string Serialize(object payload);
        object Deserialize(string payload);
    }
}
