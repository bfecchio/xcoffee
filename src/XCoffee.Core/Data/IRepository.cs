using System;
using XCoffee.Core.Domain;

namespace XCoffee.Core.Data
{
    public interface IRepository<TAggregateRoot> : IDisposable
        where TAggregateRoot : IAggregateRoot
    {
        #region IRepository Members

        IUnitOfWork UnitOfWork { get; }

        #endregion
    }
}
