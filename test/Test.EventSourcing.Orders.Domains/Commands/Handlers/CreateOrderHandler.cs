using Neptuo;
using Neptuo.Commands.Handlers;
using Neptuo.Models.Keys;
using Neptuo.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Domains.Commands.Handlers
{
    public class CreateOrderHandler : ICommandHandler<CreateOrder>
    {
        private readonly IRepository<Order, IKey> repository;

        public CreateOrderHandler(IRepository<Order, IKey> repository)
        {
            Ensure.NotNull(repository, "repository");
            this.repository = repository;
        }

        public Task HandleAsync(CreateOrder command)
        {
            repository.Save(new Order(command.OrderKey));
            return Task.FromResult(true);
        }
    }
}
