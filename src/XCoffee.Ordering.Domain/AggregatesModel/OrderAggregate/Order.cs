using System.Collections.Generic;

using XCoffee.Core.Domain;

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

        #endregion

        #region Entity Members

        protected override IEnumerable<object> GetIdentityComponents()
        {
            yield return Id;
        }

        #endregion
    }
}
