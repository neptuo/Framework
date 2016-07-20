using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neptuo.Models.Keys;
using Neptuo.Threading.Tasks;

namespace Neptuo.Data
{
    /// <summary>
    /// The empty implementation of <see cref="ICommandStore"/> and <see cref="ICommandPublishingStore"/>.
    /// </summary>
    public class EmptyCommandStore : ICommandStore, ICommandPublishingStore
    {
        public Task ClearAsync()
        {
            return Async.CompletedTask;
        }

        public Task<IEnumerable<CommandModel>> GetAsync()
        {
            return Task.FromResult(Enumerable.Empty<CommandModel>());
        }

        public Task PublishedAsync(IKey commandKey)
        {
            return Async.CompletedTask;
        }

        public void Save(IEnumerable<CommandModel> commands)
        { }

        public void Save(CommandModel command)
        { }
    }
}
