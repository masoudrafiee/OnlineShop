using MediatR;
using OnlineShop.Domain.AggregatesModel.ProductAggregate;
using OnlineShop.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Commands
{
    public class CreateProductCommand : IRequest<ProductItem>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal Profit { get; set; }
        public ProductType ProductType { get; set; }
        public DiscountType DiscountType { get; set; }
        public string Description { get; set; }
        public CreateProductCommand(string name, decimal price, DiscountType discountType, decimal discountValue, decimal profit, ProductType productType, string description)
        { 
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
