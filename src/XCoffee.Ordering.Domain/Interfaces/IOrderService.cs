using System;
using System.Threading.Tasks;

using XCoffee.Core.Domain;
using XCoffee.Ordering.Domain.AggregatesModel.OrderAggregate;

namespace XCoffee.Ordering.Domain.Interfaces
{
    public interface IOrderService : IDomainService
    {
        #region IOrderService Members

        Task<bool> Add(Basket basket);

        #endregion
    }
}
