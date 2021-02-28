using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using XCoffee.Core.Data;
using XCoffee.Ordering.Data.Extensions;

namespace XCoffee.Ordering.Data.Context
{
    public sealed class OrderingContext : DbContext, IUnitOfWork
    {
        #region Constants

        public const string SCHEMA = "dbo";

        #endregion

        #region Constructors

        public OrderingContext(DbContextOptions<OrderingContext> options)
            : base(options)
        {
            ChangeTracker.AutoDetectChangesEnabled = false;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        #endregion

        #region IUnitOfWork Members

        public async Task<bool> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await SaveChangesAsync(cancellationToken) > 0;
        }

        #endregion

        #region Overriden Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.LoadAllConfigurations();

            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
