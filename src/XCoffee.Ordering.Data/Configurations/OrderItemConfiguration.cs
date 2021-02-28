using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using XCoffee.Core.Data;
using XCoffee.Ordering.Data.Context;
using XCoffee.Ordering.Domain.AggregatesModel.OrderAggregate;

namespace XCoffee.Ordering.Data.Configurations
{
    internal sealed class OrderItemConfiguration : EntityConfiguration<OrderItem>
    {
        #region EntityConfiguration Members

        public override void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems", OrderingContext.SCHEMA);

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedNever();
            builder.Property(p => p.OrderId).IsRequired();
            builder.Property(p => p.ProductId).IsRequired();
            builder.Property(p => p.Quantity).IsRequired();
            builder.Property(p => p.Price).HasColumnType("NUMERIC(8,2)").IsRequired();
            builder.Property(p => p.Amount).HasColumnType("NUMERIC(8,2)").IsRequired();

            builder
                .HasOne(p => p.Order).WithMany(p => p.Items)
                .HasForeignKey(p => p.OrderId).OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(p => p.Product).WithMany()
                .HasForeignKey(p => p.ProductId).OnDelete(DeleteBehavior.NoAction);
        }

        #endregion
    }
}
