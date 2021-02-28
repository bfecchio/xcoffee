using System;
using System.Collections.Generic;

using XCoffee.Core.Domain;
using XCoffee.Ordering.Domain.AggregatesModel.ProductAggregate;

namespace XCoffee.Ordering.Domain.AggregatesModel.OrderAggregate
{
    public class OrderItem : Entity
    {
        #region Properties

        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
        public decimal Amount { get; private set; }

        #endregion

        #region EF Properties

        public virtual Order Order { get; private set; }
        public virtual Product Product { get; private set; }

        #endregion

        #region Constructors

        protected OrderItem() { }

        public OrderItem(Guid productId, int quantity, decimal price)
        {
            ProductId = productId;
            Quantity = quantity;
            Price = price;            
        }

        #endregion

        #region Entity Members

        protected override IEnumerable<object> GetIdentityComponents()
        {
            yield return Id;
        }

        #endregion
    }
}
