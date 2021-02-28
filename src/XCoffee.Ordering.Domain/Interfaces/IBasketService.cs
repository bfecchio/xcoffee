using System.Collections.Generic;

using XCoffee.Core.Domain;
using XCoffee.Ordering.Domain.AggregatesModel.OrderAggregate;
using XCoffee.Ordering.Domain.AggregatesModel.ProductAggregate;

namespace XCoffee.Ordering.Domain.Interfaces
{
    public interface IBasketService : IDomainService
    {
        #region IBasketService Members

        Basket Basket { get; }

        void New();
        void Cancel();
        void ClearCoins();


        IEnumerable<BasketItem> ListItems();
        void AddCoin(Coin coin, int quantity);
        
        IEnumerable<BasketCoin> ListCoins();
        void AddItem(Product product, int quantity);

        #endregion
    }
}
