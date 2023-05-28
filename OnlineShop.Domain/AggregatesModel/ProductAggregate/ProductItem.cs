using OnlineShop.Domain.Exceptions;
using OnlineShop.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineShop.Domain.AggregatesModel.ProductAggregate
{
    public class ProductItem : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal Profit { get; set; }
        public ProductType ProductType { get; set; } 
        public DiscountType DiscountType { get; set; }
        public string Description { get; set; }
        public ProductItem()
        {

        }
        public ProductItem(string name, decimal price, DiscountType discountType, decimal discountValue, decimal profit, ProductType productType, string description)
        {
            if (string.IsNullOrEmpty(name))
                AddError("Name is missing");

            if (price < 1)
                AddError("Price is missing");

            if (productType.Id < 1)
                AddError("ProductTypeId is missing");

            if (discountValue < 1)
                AddError("discount is missing");

            if (discountType.Id < 1)
                AddError("discountTypeId is missing");

            if (profit < 1)
                AddError("profit is missing");

            if (Errors.Any())
                throw new DomainException(Errors.Select(e => e.Error));

            Name = name;
            Price = price;
            DiscountValue = discountValue;
            DiscountType = discountType;
            Profit = profit;
            ProductType = productType;
            Description = description;
        }
    }
}
