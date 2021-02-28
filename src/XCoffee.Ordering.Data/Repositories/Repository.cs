using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

using XCoffee.Core.Data;
using XCoffee.Core.Domain;
using XCoffee.Ordering.Data.Context;

namespace XCoffee.Ordering.Data.Repositories
{
    public abstract class Repository<TAggregateRoot> : IReadRepository<TAggregateRoot>, IWriteRepository<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot
    {
        #region Read-Only Fields

        protected readonly ILogger Logger;
        protected readonly OrderingContext Db;
        protected readonly DbSet<TAggregateRoot> DbSet;

        #endregion

        #region Constructors

        public Repository(OrderingContext context, ILogger logger)
        {
            Db = context ?? throw new ArgumentNullException(nameof(context));
            DbSet = Db.Set<TAggregateRoot>();

            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region IRepository Members

        public IUnitOfWork UnitOfWork => Db;

        public void Dispose() => Db?.Dispose();

        #endregion

        #region IReadRepository Members

        public virtual async Task<TAggregateRoot> Get(Guid id)
            => await DbSet.FindAsync(id);

        public virtual async Task<IEnumerable<TAggregateRoot>> GetAll()
            => await DbSet.AsNoTracking().ToListAsync();

        public virtual async Task<IEnumerable<TAggregateRoot>> GetAll(
            Func<IQueryable<TAggregateRoot>, IOrderedQueryable<TAggregateRoot>> orderBy = null,
            Func<IQueryable<TAggregateRoot>, IIncludableQueryable<TAggregateRoot, object>> include = null,
            bool disableTracking = true)
        {
            var query = DbSet.AsQueryable();

            if (disableTracking)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        public async virtual Task<TAggregateRoot> Find(
            Expression<Func<TAggregateRoot, bool>> predicate = null,
            Func<IQueryable<TAggregateRoot>, IIncludableQueryable<TAggregateRoot, object>> include = null,
            Func<IQueryable<TAggregateRoot>, IOrderedQueryable<TAggregateRoot>> orderBy = null)
        {
            var query = DbSet.AsQueryable();

            if (include != null)
                query = include(query);

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).FirstOrDefaultAsync();

            return await query.FirstOrDefaultAsync();
        }

        public async virtual Task<IEnumerable<TAggregateRoot>> FindAll(
            Expression<Func<TAggregateRoot, bool>> predicate,
            Func<IQueryable<TAggregateRoot>, IOrderedQueryable<TAggregateRoot>> orderBy = null,
            Func<IQueryable<TAggregateRoot>, IIncludableQueryable<TAggregateRoot, object>> include = null,
            bool disableTracking = true)
        {
            var query = DbSet.AsQueryable();

            if (disableTracking)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        public virtual async Task<bool> Any(Expression<Func<TAggregateRoot, bool>> predicate)
            => await DbSet.AnyAsync(predicate);

        #endregion

        #region IWriteRepository Members

        public virtual async Task Add(TAggregateRoot aggregateRoot)
            => await DbSet.AddAsync(aggregateRoot);

        public virtual Task Update(TAggregateRoot aggregateRoot)
        {
            DbSet.Update(aggregateRoot);
            return Task.CompletedTask;
        }

        public virtual Task Delete(TAggregateRoot aggregateRoot)
        {
            DbSet.Remove(aggregateRoot);
            return Task.CompletedTask;
        }

        #endregion
    }
}
