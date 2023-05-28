using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Domain.AggregatesModel.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Infrastructure.EntityConfigurations
{
    public class ProductItemEntityTypeConfiguration : IEntityTypeConfiguration<ProductItem>
    {
        public void Configure(EntityTypeBuilder<ProductItem> productItemConfiguration)
        {

            productItemConfiguration.HasKey(x => x.Id);
            productItemConfiguration.Property(x => x.Id).ValueGeneratedOnAdd();

            productItemConfiguration.Property(x => x.ProductType)
                .HasColumnType("int")
                       .HasConversion( 
                            x => x.Id,
                            x => DiscountType.FromValue<ProductType>(x));

            productItemConfiguration.Property(x => x.DiscountType)
                .HasColumnType("int")
                       .HasConversion( 
                            x => x.Id,
                            x => DiscountType.FromValue<DiscountType>(x));
        }
    }
}
