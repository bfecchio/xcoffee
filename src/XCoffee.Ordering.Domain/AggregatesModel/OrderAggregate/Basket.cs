using System.Linq;
using System.Collections.Generic;

using XCoffee.Core.Domain;
using XCoffee.Ordering.Domain.AggregatesModel.ProductAggregate;

namespace XCoffee.Ordering.Domain.AggregatesModel.OrderAggregate
{
    public class Basket : Entity
    {
        #region Read-Only Fields

        private readonly List<BasketItem> _items;
        private readonly List<BasketCoin> _coins;

        #endregion

        #region Properties

        public IReadOnlyCollection<BasketItem> Items => _items.AsReadOnly();
        public IReadOnlyCollection<BasketCoin> Coins => _coins.AsReadOnly();

        #endregion

        #region Constructors

        public Basket()
        {
            _items = new List<BasketItem>();
            _coins = new List<BasketCoin>();
        }

        #endregion

        #region Methods

        internal protected void AddItem(Product product, int quantity)
        {
            Guard.AssertArgumentNotNull(product, "The product is required.");
            Guard.AssertArgumentRange(quantity, 1, 10, "The quantity of the product must be between 1 and 10.");

            var basketItem = _items.FirstOrDefault(x => x.Product == product);
            if (basketItem != null)
                basketItem.AddUnits(quantity);
            else
                _items.Add(new BasketItem(product, quantity));
        }

        internal protected void AddCoin(Coin coin, int quantity)
        {
            Guard.AssertArgumentNotNull(coin, "The coin is required.");
            Guard.AssertArgumentRange(quantity, 1, 10, "The quantity of the product must be between 1 and 10.");

            var depositedCoin = _coins.FirstOrDefault(x => x.Coin == coin);
            if (depositedCoin != null)
                depositedCoin.AddUnits(quantity);
            else
                _coins.Add(new BasketCoin(coin, quantity));
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
