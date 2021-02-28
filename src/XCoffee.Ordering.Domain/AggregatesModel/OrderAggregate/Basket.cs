using System.Collections.Generic;

using XCoffee.Core.Domain;

namespace XCoffee.Ordering.Domain.AggregatesModel.OrderAggregate
{
    public sealed class Basket : Entity
    {
        #region Properties

        #endregion

        #region Entity Members

        protected override IEnumerable<object> GetIdentityComponents()
        {
            yield return Id;
        }

        #endregion
    }
}
