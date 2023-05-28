using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using OnlineShop.Domain.AggregatesModel.BasketAggregate;
using OnlineShop.Domain.AggregatesModel.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Infrastructure.EntityConfigurations
{
    public class BasketItemEntityTypeConfiguration : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> basketItemConfiguration)
        {

            basketItemConfiguration.Property(x => x.ProductType)
                .HasColumnType("int")
                       .HasConversion(
                            //x => JsonConvert.SerializeObject(x),
                            //x => JsonConvert.DeserializeObject<DiscountType>(x)
                            x => x.Id,
                            x => DiscountType.FromValue<ProductType>(x));

            basketItemConfiguration.Property(x => x.DiscountType)
                .HasColumnType("int")
                       .HasConversion(
                            //x => JsonConvert.SerializeObject(x),
                            //x => JsonConvert.DeserializeObject<DiscountType>(x)
                            x => x.Id,
                            x => DiscountType.FromValue<DiscountType>(x));
        }
    }
}
