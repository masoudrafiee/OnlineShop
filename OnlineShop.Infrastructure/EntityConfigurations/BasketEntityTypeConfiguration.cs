using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using OnlineShop.Domain.AggregatesModel.BasketAggregate;
using OnlineShop.Domain.AggregatesModel.BuyerAggregate;
using OnlineShop.Domain.AggregatesModel.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Infrastructure.EntityConfigurations
{
    public class BasketEntityTypeConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> basketConfiguration)
        {
            basketConfiguration.ToTable("Basket", "dbo");

            basketConfiguration.HasKey(b => b.Id);

            basketConfiguration.HasOne<Buyer>()
            .WithMany()
            .IsRequired(true)
            .HasForeignKey("BuyerId");

            
        }
    }
}
