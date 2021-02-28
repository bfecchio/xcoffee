using XCoffee.Core.Data;
using XCoffee.Ordering.Domain.AggregatesModel.OrderAggregate;

namespace XCoffee.Ordering.Domain.Repositories
{
    public interface IOrderRepository : IReadRepository<Order>, IWriteRepository<Order>
    {
        #region IOrderRepository Members

        #endregion
    }
}
