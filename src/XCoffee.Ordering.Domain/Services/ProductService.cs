using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

using XCoffee.Core.Domain;
using XCoffee.Ordering.Domain.Interfaces;
using XCoffee.Ordering.Domain.Repositories;
using XCoffee.Ordering.Domain.AggregatesModel.ProductAggregate;

namespace XCoffee.Ordering.Domain.Services
{
    public sealed class ProductService : DomainService, IProductService
    {
        #region Read-Only Fields

        private readonly IProductRepository _productRepository;

        #endregion

        #region Constructors

        public ProductService(ILogger<ProductService> logger
            , IProductRepository productRepository
        )
            : base(logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        #endregion

        #region IProductService Members

        public async Task<Product> Get(Guid productId)
        {
            return await _productRepository.Get(productId);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var collection = await _productRepository.GetAll();
            return collection.OrderBy(x => x.Price);
        }

        #endregion
    }
}
