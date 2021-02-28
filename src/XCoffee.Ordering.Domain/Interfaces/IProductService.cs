using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using XCoffee.Core.Domain;
using XCoffee.Ordering.Domain.AggregatesModel.ProductAggregate;

namespace XCoffee.Ordering.Domain.Interfaces
{
    public interface IProductService : IDomainService
    {
        #region IProductService Members

        Task<Product> Get(Guid productId);
        Task<IEnumerable<Product>> GetAll();

        #endregion
    }
}
