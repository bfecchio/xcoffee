using System.Collections.Generic;

using XCoffee.Core.Domain;
using XCoffee.Ordering.Domain.AggregatesModel.ProductAggregate;

namespace XCoffee.Ordering.Domain.AggregatesModel.OrderAggregate
{
    public sealed class BasketItem : Entity
    {        
        #region Properties

        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        public decimal Amount => Product.Price * Quantity;

        #endregion

        #region Constructors

        public BasketItem(Product product, int quantity)
        {
            Guard.AssertArgumentNotNull(product, "The product is required.");
            Guard.AssertArgumentRange(quantity, 1, 10, "The quantity of the product must be between 1 and 10.");

            Product = product;
            Quantity = quantity;
        }

        #endregion

        #region Entity Members

        protected override IEnumerable<object> GetIdentityComponents()
        {
            yield return Product.Id;
        }

        #endregion

        #region Methods

        public void AddUnits(int units) => Quantity += units;

        #endregion
    }
}
