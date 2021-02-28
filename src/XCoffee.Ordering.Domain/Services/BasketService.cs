using System;
using Microsoft.Extensions.Logging;

using XCoffee.Core.Domain;
using XCoffee.Ordering.Domain.Interfaces;
using XCoffee.Ordering.Domain.Repositories;

namespace XCoffee.Ordering.Domain.Services
{
    public sealed class BasketService : DomainService, IBasketService
    {
        #region Read-Only Fields

        private readonly IOrderRepository _orderRepository;

        #endregion

        #region Constructors

        public BasketService(ILogger<BasketService> logger
            , IOrderRepository orderRepository
        )
            : base(logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        #endregion
    }
}
