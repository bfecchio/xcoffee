using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using XCoffee.Core.Domain;
using XCoffee.Ordering.Domain.Interfaces;
using XCoffee.Ordering.Domain.Repositories;
using XCoffee.Ordering.Domain.AggregatesModel.OrderAggregate;

namespace XCoffee.Ordering.Domain.Services
{
    public sealed class OrderService : DomainService, IOrderService
    {
        #region Read-Only Fields

        private readonly IOrderRepository _orderRepository;

        #endregion

        #region Constructors

        public OrderService(ILogger<ProductService> logger
            , IOrderRepository orderRepository
        )
            : base(logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        #endregion

        #region IProductService Members

        public async Task<bool> Add(Basket basket)
        {
            var order = new Order(basket.Items.Count, basket.Amount, basket.AmountPaid, basket.AmountPayBack);
            foreach (var item in basket.Items) order.AddItem(item.Product, item.Quantity);

            await _orderRepository.Add(order);
            return await _orderRepository.UnitOfWork.CommitAsync();            
        }

        #endregion
    }
}
