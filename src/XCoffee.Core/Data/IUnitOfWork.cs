using System.Threading;
using System.Threading.Tasks;

namespace XCoffee.Core.Data
{
    public interface IUnitOfWork
    {
        #region IUnitOfWork Members

        Task<bool> CommitAsync(CancellationToken cancellationToken = default);

        #endregion
    }
}
