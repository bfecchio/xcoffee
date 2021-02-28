using Microsoft.Extensions.Logging;

using XCoffee.Ordering.Data.Context;
using XCoffee.Ordering.Domain.Repositories;
using XCoffee.Ordering.Domain.AggregatesModel.OrderAggregate;

namespace XCoffee.Ordering.Data.Repositories
{
    public sealed class OrderRepository : Repository<Order>, IOrderRepository
    {
        #region Constructors

        public OrderRepository(OrderingContext context, ILogger<OrderRepository> logger)
            : base(context, logger)
        { }

        #endregion
    }
}
