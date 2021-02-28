using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

using XCoffee.Core.Domain;
using XCoffee.Ordering.Domain.Interfaces;
using XCoffee.Ordering.Domain.AggregatesModel.OrderAggregate;
using XCoffee.Ordering.Domain.AggregatesModel.ProductAggregate;

namespace XCoffee.Ordering.Domain.Services
{
    public sealed class BasketService : DomainService, IBasketService
    {
        #region Properties

        public Basket Basket { get; private set; }        

        #endregion

        #region Constructors

        public BasketService(ILogger<BasketService> logger)
            : base(logger)
        { }

        #endregion

        #region IBasketService Members

        public void New()
            => Basket = new Basket();

        public void Cancel()
            => Basket = null;

        public void AddItem(Product product, int quantity)
            => Basket.AddItem(product, quantity);

        public IEnumerable<BasketItem> ListItems()
            => Basket.Items.OrderBy(x => x.Quantity);

        public void AddCoin(Coin coin, int quantity)
            => Basket.AddCoin(coin, quantity);

        public IEnumerable<BasketCoin> ListCoins()
            => Basket.Coins.OrderBy(x => x.Coin.Value);

        #endregion
    }
}
