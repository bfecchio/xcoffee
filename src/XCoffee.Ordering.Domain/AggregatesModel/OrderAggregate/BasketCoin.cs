using System.Collections.Generic;

using XCoffee.Core.Domain;

namespace XCoffee.Ordering.Domain.AggregatesModel.OrderAggregate
{
    public sealed class BasketCoin : Entity
    {
        #region Properties

        public Coin Coin { get; private set; }
        public int Quantity { get; private set; }
        public decimal Amount => Coin.Value * Quantity;

        #endregion

        #region Constructors

        public BasketCoin(Coin coin, int quantity)
        {
            Guard.AssertArgumentNotNull(coin, "The coin is required.");    
            
            Coin = coin;
            Quantity = quantity;
        }

        #endregion

        #region Entity Members

        protected override IEnumerable<object> GetIdentityComponents()
        {
            yield return Coin.Id;
        }

        #endregion

        #region Methods

        public void AddUnits(int units) => Quantity += units;

        #endregion
    }
}
