using Microsoft.Extensions.Logging;

using XCoffee.Ordering.Data.Context;
using XCoffee.Ordering.Domain.Repositories;
using XCoffee.Ordering.Domain.AggregatesModel.ProductAggregate;

namespace XCoffee.Ordering.Data.Repositories
{
    public sealed class ProductRepository : Repository<Product>, IProductRepository
    {
        #region Constructors

        public ProductRepository(OrderingContext context, ILogger<ProductRepository> logger)
            : base(context, logger)
        { }

        #endregion
    }
}
