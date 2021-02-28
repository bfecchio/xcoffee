using System;
using Microsoft.Extensions.Logging;

using XCoffee.Core.Domain;
using XCoffee.Ordering.Domain.Interfaces;
using XCoffee.Ordering.Domain.Repositories;

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
    }
}
