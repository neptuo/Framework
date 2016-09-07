using Neptuo;
using Neptuo.Activators;
using Neptuo.Collections.Specialized;
using Neptuo.Commands;
using Neptuo.Commands.Handlers;
using Neptuo.Data;
using Neptuo.Events;
using Neptuo.Events.Handlers;
using Neptuo.Formatters;
using Neptuo.Formatters.Metadata;
using Neptuo.Models.Domains;
using Neptuo.Models.Keys;
using Neptuo.Models.Repositories;
using Neptuo.Threading.Tasks;
using Orders.Domains.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.EventSourcing
{
    public class MockEventStore : IEventStore, IEventRebuilderStore
    {
        private readonly Dictionary<IKey, List<EventModel>> storage = new Dictionary<IKey, List<EventModel>>();

        public IEnumerable<EventModel> Get(IKey aggregateKey)
        {
            List<EventModel> events;
            if (storage.TryGetValue(aggregateKey, out events))
                return events;

            return Enumerable.Empty<EventModel>();
        }

        public Task<IEnumerable<EventModel>> GetAsync(IEnumerable<string> eventTypes)
        {
            return Task.FromResult(storage.Values.SelectMany(e => e).Where(e => eventTypes.Contains(e.EventKey.Type)));
        }

        public void Save(IEnumerable<EventModel> events)
        {
            EventModel payload = events.FirstOrDefault();
            if (payload != null)
            {
                List<EventModel> entities;
                if (!storage.TryGetValue(payload.AggregateKey, out entities))
                    storage[payload.AggregateKey] = entities = new List<EventModel>();

                entities.AddRange(events);
            }
        }
    }

    public class OrderPlacedHandler : IEventHandler<OrderPlaced>
    {
        public Task HandleAsync(OrderPlaced payload)
        {
            Console.WriteLine(payload.AggregateKey);
            return Task.FromResult(true);
        }
    }

    public interface IHelloService
    {
        string SayHello(string name);
    }

    public class HiHelloService : IHelloService
    {
        public string SayHello(string name)
        {
            return String.Format("Hi, {0}!");
        }
    }

    public class AggregateWithParameters : AggregateRoot
    {
        public IHelloService Service { get; private set; }

        public AggregateWithParameters(IHelloService service)
        {
            Ensure.NotNull(service, "service");
            Service = service;
        }

        public AggregateWithParameters(IKey key, IEnumerable<IEvent> events)
            : base(key, events)
        { }

        public AggregateWithParameters(IKey key, IEnumerable<IEvent> events, IHelloService service)
            : base(key, events)
        {
            Ensure.NotNull(service, "service");
            Service = service;
        }
    }

    public class ReadModelHandler : IEventHandler<OrderPlaced>, IEventHandler<OrderTotalRecalculated>
    {
        public Dictionary<IKey, decimal> Totals { get; private set; }

        public ReadModelHandler()
        {
            Totals = new Dictionary<IKey, decimal>();
        }

        public Task HandleAsync(OrderPlaced payload)
        {
            Totals[payload.AggregateKey] = 0;
            return Async.CompletedTask;
        }

        public Task HandleAsync(OrderTotalRecalculated payload)
        {
            Totals[payload.AggregateKey] = payload.TotalPrice;
            return Async.CompletedTask;
        }
    }


    public class SlowCommand : Command
    { }

    public class FastCommand : Command
    { }

    public class CommandHandlerService
    {
        private readonly object logLock = new object();

        public List<CommandType> Log { get; private set; }

        public CommandHandlerService()
        {
            Log = new List<CommandType>();
        }

        public void AddLog(CommandType log)
        {
            lock (logLock)
            {
                Log.Add(log);
            }
        }
    }

    public enum CommandType
    {
        Slow,
        Fast
    }

    public class SlowCommandHandler : ICommandHandler<SlowCommand>
    {
        private readonly CommandHandlerService service;

        public SlowCommandHandler(CommandHandlerService service)
        {
            Ensure.NotNull(service, "service");
            this.service = service;
        }

        public async Task HandleAsync(SlowCommand command)
        {
            await Task.Delay(2000);
            service.AddLog(CommandType.Slow);
        }
    }

    public class FastCommandHandler : ICommandHandler<FastCommand>
    {
        private readonly CommandHandlerService service;

        public FastCommandHandler(CommandHandlerService service)
        {
            Ensure.NotNull(service, "service");
            this.service = service;
        }

        public async Task HandleAsync(FastCommand command)
        {
            await Task.Delay(100);
            service.AddLog(CommandType.Fast);
        }
    }
}
