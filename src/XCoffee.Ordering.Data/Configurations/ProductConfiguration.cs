using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using XCoffee.Core.Data;
using XCoffee.Core.Domain;
using XCoffee.Ordering.Data.Context;
using XCoffee.Ordering.Domain.AggregatesModel.ProductAggregate;

namespace XCoffee.Ordering.Data.Configurations
{
    internal sealed class ProductConfiguration : EntityConfiguration<Product>
    {
        #region EntityConfiguration Members

        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products", OrderingContext.SCHEMA);

            builder.HasKey(p => p.Id);
            
            builder.Property(p => p.Id).ValueGeneratedNever();
            builder.Property(p => p.Name).HasColumnType("VARCHAR(80)").IsRequired();
            builder.Property(p => p.Price).HasColumnType("NUMERIC(8,2)").IsRequired();
            builder.Property(p => p.Thumbnail).HasColumnType("VARCHAR(80)").IsRequired();
            builder.Property(p => p.Description).HasColumnType("VARCHAR(70)").IsRequired();
        }

        #endregion

        #region Overriden Methods

        public override IEnumerable<Product> Seed()
            => Enumeration<Guid>.GetAll<Product>();

        #endregion
    }
}
