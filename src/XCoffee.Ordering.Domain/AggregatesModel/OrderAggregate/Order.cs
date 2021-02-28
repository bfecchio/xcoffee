using System.Collections.Generic;

using XCoffee.Core.Domain;
using XCoffee.Ordering.Domain.AggregatesModel.ProductAggregate;

namespace XCoffee.Ordering.Domain.AggregatesModel.OrderAggregate
{
    public class Order : Entity, IAggregateRoot
    {
        #region Read-Only Fields

        private readonly List<OrderItem> _orderItems = new List<OrderItem>();

        #endregion

        #region Properties

        public int TotalItems { get; private set; }
        public decimal Amount { get; private set; }
        public decimal AmountPaid { get; private set; }
        public decimal AmountPayBack { get; private set; }
        public virtual IReadOnlyCollection<OrderItem> Items => _orderItems.AsReadOnly();

        #endregion

        #region Constructors

        protected Order() { }

        public Order(int totalItems, decimal amount, decimal amountPaid, decimal amountPayBack)
        {
            TotalItems = totalItems;
            Amount = amount;
            AmountPaid = amountPaid;
            AmountPayBack = amountPayBack;
        }

        #endregion

        #region Methods

        public void AddItem(Product product, int quantity)
        {
            var newItem = new OrderItem(product.Id, quantity, product.Price);
            _orderItems.Add(newItem);
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
