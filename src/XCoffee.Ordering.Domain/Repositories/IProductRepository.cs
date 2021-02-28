using XCoffee.Core.Data;
using XCoffee.Ordering.Domain.AggregatesModel.ProductAggregate;

namespace XCoffee.Ordering.Domain.Repositories
{
    public interface IProductRepository : IReadRepository<Product>, IWriteRepository<Product>
    {
        #region IProductRepository Members

        #endregion
    }
}
