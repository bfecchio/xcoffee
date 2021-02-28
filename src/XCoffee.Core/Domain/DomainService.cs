using System;
using Microsoft.Extensions.Logging;

namespace XCoffee.Core.Domain
{
    public abstract class DomainService : IDomainService
    {
        #region Read-Only Fields

        protected readonly ILogger _logger;

        #endregion

        #region Constructors

        protected DomainService(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region IDomainService Members

        public void Dispose()
            => GC.SuppressFinalize(this);

        #endregion
    }
}
