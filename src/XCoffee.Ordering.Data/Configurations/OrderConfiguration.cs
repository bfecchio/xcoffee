using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using XCoffee.Core.Data;
using XCoffee.Ordering.Data.Context;
using XCoffee.Ordering.Domain.AggregatesModel.OrderAggregate;

namespace XCoffee.Ordering.Data.Configurations
{
    internal sealed class OrderConfiguration : EntityConfiguration<Order>
    {
        #region EntityConfiguration Members

        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders", OrderingContext.SCHEMA);

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedNever();
            builder.Property(p => p.TotalItems).IsRequired();
            builder.Property(p => p.Amount).HasColumnType("NUMERIC(8,2)").IsRequired();
            builder.Property(p => p.AmountPaid).HasColumnType("NUMERIC(8,2)").IsRequired();
            builder.Property(p => p.AmountPayBack).HasColumnType("NUMERIC(8,2)").IsRequired();
        }

        #endregion
    }
}
