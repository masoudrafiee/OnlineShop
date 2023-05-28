using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Domain.AggregatesModel.BuyerAggregate;
using OnlineShop.Domain.AggregatesModel.OrderAggregate;
using OnlineShop.Domain.AggregatesModel.ShippingAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Infrastructure.EntityConfigurations
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> orderConfiguration)
        {
            orderConfiguration.ToTable("Order", "dbo");
            orderConfiguration.HasKey(o => o.Id); 

            orderConfiguration
                .OwnsOne(o => o.Address, a =>
                {
                    a.WithOwner();
                });

            orderConfiguration
                .Property<int>("_buyerId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("BuyerId");

            orderConfiguration
                .Property<DateTime>("OrderDate")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("OrderDate")
                .IsRequired();

            orderConfiguration.Property<string>("Description").IsRequired(false);

            orderConfiguration.HasOne<Buyer>()
                .WithMany()
                .IsRequired(false)
                .HasForeignKey("_buyerId");

            orderConfiguration.Property(x => x.ShippingType)
                .HasColumnType("int")
                       .HasConversion(
                            x => x.Id,
                            x => ShippingType.FromValue<ShippingType>(x));
        }
    }
}
