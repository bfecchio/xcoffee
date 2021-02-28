using System.Linq;
using System.Collections.Generic;

using XCoffee.Core.Domain;
using XCoffee.Ordering.Domain.AggregatesModel.ProductAggregate;
using System;

namespace XCoffee.Ordering.Domain.AggregatesModel.OrderAggregate
{
    public class Basket : Entity
    {
        #region Read-Only Fields

        private readonly List<BasketItem> _items;
        private readonly List<BasketCoin> _coins;
        private readonly List<BasketCoin> _payBackCoins;

        #endregion

        #region Properties

        public decimal Amount { get; private set; }
        public decimal AmountPaid { get; private set; }
        public decimal AmountPayBack { get; private set; }
        public IReadOnlyCollection<BasketItem> Items => _items.AsReadOnly();
        public IReadOnlyCollection<BasketCoin> Coins => _coins.AsReadOnly();
        public IReadOnlyCollection<BasketCoin> PayBackCoins => _payBackCoins.AsReadOnly();

        #endregion

        #region Constructors

        public Basket()
        {
            _items = new List<BasketItem>();
            _coins = new List<BasketCoin>();
            _payBackCoins = new List<BasketCoin>();
        }

        #endregion

        #region Methods

        internal protected void AddItem(Product product, int quantity)
        {
            Guard.AssertArgumentNotNull(product, "The product is required.");            

            var basketItem = _items.FirstOrDefault(x => x.Product == product);
            if (basketItem != null)
                basketItem.AddUnits(quantity);
            else
                _items.Add(new BasketItem(product, quantity));

            CalculateTotal();
        }

        internal protected void AddCoin(Coin coin, int quantity)
        {
            Guard.AssertArgumentNotNull(coin, "The coin is required.");            

            var depositedCoin = _coins.FirstOrDefault(x => x.Coin == coin);
            if (depositedCoin != null)
                depositedCoin.AddUnits(quantity);
            else
                _coins.Add(new BasketCoin(coin, quantity));

            CalculatePayment();
            CalculatePayBack();
        }

        internal protected void ClearCoins()
        {
            _coins.Clear();
            _payBackCoins.Clear();

            CalculatePayment();
            CalculatePayBack();
        }

        private void CalculateTotal()
            => Amount = _items?.Sum(x => x.Amount) ?? 0;

        private void CalculatePayment()
            => AmountPaid = _coins?.Sum(x => x.Amount) ?? 0;

        private void CalculatePayBack()
        {
            var payback = (AmountPaid > 0 ? AmountPaid - Amount : 0);
            AmountPayBack = (payback < 0 ? 0 : payback);

            if (payback <= 0) return;
            ComputePayBackCoins(payback);
        }

        private void ComputePayBackCoins(decimal payBack)
        {
            var coinOptions = Enumeration<int>.GetAll<Coin>();

            _payBackCoins.Clear();
            foreach (var coin in coinOptions.OrderByDescending(x => x.Value))
            {
                var units = (int)(payBack / coin.Value);
                if (units > 0)
                {
                    var payBackCoin = new BasketCoin(coin, units);
                    
                    _payBackCoins.Add(payBackCoin);
                    payBack -= payBackCoin.Amount;
                }
            }
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
