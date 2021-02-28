using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Query;
using XCoffee.Core.Domain;

namespace XCoffee.Core.Data
{
    public interface IReadRepository<TAggregateRoot> : IRepository<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot
    {
        #region IReadRepository Members

        Task<TAggregateRoot> Get(Guid id);

        Task<IEnumerable<TAggregateRoot>> GetAll();

        Task<IEnumerable<TAggregateRoot>> GetAll(
              Func<IQueryable<TAggregateRoot>, IOrderedQueryable<TAggregateRoot>> orderBy = null
            , Func<IQueryable<TAggregateRoot>, IIncludableQueryable<TAggregateRoot, object>> include = null
            , bool disableTracking = true
        );

        Task<TAggregateRoot> Find(
              Expression<Func<TAggregateRoot, bool>> predicate = null
            , Func<IQueryable<TAggregateRoot>, IIncludableQueryable<TAggregateRoot, object>> include = null
            , Func<IQueryable<TAggregateRoot>, IOrderedQueryable<TAggregateRoot>> orderBy = null
        );

        Task<IEnumerable<TAggregateRoot>> FindAll(
              Expression<Func<TAggregateRoot, bool>> predicate
            , Func<IQueryable<TAggregateRoot>, IOrderedQueryable<TAggregateRoot>> orderBy = null
            , Func<IQueryable<TAggregateRoot>, IIncludableQueryable<TAggregateRoot, object>> include = null
            , bool disableTracking = true
        );

        Task<bool> Any(Expression<Func<TAggregateRoot, bool>> predicate);

        #endregion
    }
}