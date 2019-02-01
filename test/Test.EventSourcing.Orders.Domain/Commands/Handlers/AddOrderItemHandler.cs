using Neptuo;
using Neptuo.Commands.Handlers;
using Neptuo.Models.Keys;
using Neptuo.Models.Repositories;
using Neptuo.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Commands.Handlers
{
    public class AddOrderItemHandler : ICommandHandler<AddOrderItem>
    {
        private readonly IRepository<Order, IKey> repository;

        public AddOrderItemHandler(IRepository<Order, IKey> repository)
        {
            Ensure.NotNull(repository, "repository");
            this.repository = repository;
        }

        public async Task HandleAsync(AddOrderItem command)
        {
            Order order = await repository.FindAsync(command.OrderKey);
            if (order == null)
                throw new MissingOrderException(command.OrderKey);

            order.AddItem(command.ProductKey, command.Count);
            await repository.SaveAsync(order);
        }
    }
}
