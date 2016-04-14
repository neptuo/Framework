using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data
{
    /// <summary>
    /// Describes serialized command with metadata.
    /// </summary>
    public class CommandModel
    {
        /// <summary>
        /// The key of the command.
        /// </summary>
        public IKey CommandKey { get; set; }

        /// <summary>
        /// Serialized event payload.
        /// </summary>
        public string Payload { get; set; }
        
        /// <summary>
        /// The date and time when this event has raised.
        /// </summary>
        public DateTime RaisedAt { get; set; }

        /// <summary>
        /// Creates new empty instance.
        /// </summary>
        public CommandModel()
        {
            RaisedAt = DateTime.Now;
        }

        /// <summary>
        /// Creates new instance and fills values.
        /// </summary>
        /// <param name="commandKey">The key of the command.</param>
        /// <param name="payload">Serialized event payload.</param>
        public CommandModel(IKey commandKey, string payload)
            : this()
        {
            Ensure.NotNull(commandKey, "commandKey");
            CommandKey = commandKey;
            Payload = payload;
        }
    }
}
