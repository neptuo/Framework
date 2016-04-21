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
    public class AddOrderItemHandler : ICommandHandler<AddOrderItem>
    {
        private readonly IRepository<Order, IKey> repository;

        public AddOrderItemHandler(IRepository<Order, IKey> repository)
        {
            Ensure.NotNull(repository, "repository");
            this.repository = repository;
        }

        public Task HandleAsync(AddOrderItem command)
        {
            Order order = repository.Find(command.OrderKey);
            if (order == null)
                throw new MissingOrderException(command.OrderKey);

            order.AddItem(command.ProductKey, command.Count);
            repository.Save(order);
            return Task.FromResult(true);
        }
    }
}
