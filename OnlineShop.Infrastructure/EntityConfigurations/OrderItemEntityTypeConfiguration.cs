using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Domain.AggregatesModel.OrderAggregate;
using OnlineShop.Domain.AggregatesModel.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Infrastructure.EntityConfigurations
{
    public class OrderItemEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> orderItemConfiguration)
        {
            orderItemConfiguration.Property(x => x.ProductType)
             .HasColumnType("int")
                    .HasConversion(
                         x => x.Id,
                         x => DiscountType.FromValue<ProductType>(x));

            orderItemConfiguration.Property(x => x.DiscountType)
                .HasColumnType("int")
                       .HasConversion(
                            x => x.Id,
                            x => DiscountType.FromValue<DiscountType>(x));
        }
    }
}
