using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Domain.AggregatesModel.BuyerAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Infrastructure.EntityConfigurations
{
    public class BuyerEntityTypeConfiguration : IEntityTypeConfiguration<Buyer>
    {
        public void Configure(EntityTypeBuilder<Buyer> buyerConfiguration)
        {
            buyerConfiguration.ToTable("Buyer", "dbo");
            buyerConfiguration.HasKey(b => b.Id); 
            buyerConfiguration.Property(b => b.IdentityGuid)
                .HasMaxLength(200)
                .IsRequired();
            buyerConfiguration.HasIndex("IdentityGuid")
              .IsUnique(true);
            buyerConfiguration.Property(b => b.Name);
              
        }
    }
}
