using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using XCoffee.Core.Domain;

namespace XCoffee.Core.Data
{
    public abstract class EntityConfiguration<TEntity> : IEntityConfiguration
        where TEntity : class, IDomainEntity
    {
        #region EntityConfiguration Members

        public abstract void Configure(EntityTypeBuilder<TEntity> builder);

        public virtual IEnumerable<TEntity> Seed() => Array.Empty<TEntity>();

        #endregion
    }
}
